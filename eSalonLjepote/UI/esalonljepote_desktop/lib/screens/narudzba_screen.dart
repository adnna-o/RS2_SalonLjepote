import 'dart:async';
import 'package:esalonljepote_desktop/models/klijenti.dart';
import 'package:esalonljepote_desktop/models/korisnik.dart';
import 'package:esalonljepote_desktop/models/narudzba.dart';
import 'package:esalonljepote_desktop/models/placanje.dart';
import 'package:esalonljepote_desktop/models/proizvod.dart';
import 'package:esalonljepote_desktop/models/search_result.dart';
import 'package:esalonljepote_desktop/models/status.dart';
import 'package:esalonljepote_desktop/models/termini.dart';
import 'package:esalonljepote_desktop/models/usluga.dart';
import 'package:esalonljepote_desktop/models/zaposleni.dart';
import 'package:esalonljepote_desktop/providers/klijenti_provider.dart';
import 'package:esalonljepote_desktop/providers/korisnik_provider.dart';
import 'package:esalonljepote_desktop/providers/narudzba_provider.dart';
import 'package:esalonljepote_desktop/providers/placanje_provider.dart';
import 'package:esalonljepote_desktop/providers/proizvod_provider.dart';
import 'package:esalonljepote_desktop/providers/status_provider.dart';
import 'package:esalonljepote_desktop/providers/termini_provider.dart';
import 'package:esalonljepote_desktop/providers/usluga_provider.dart';
import 'package:esalonljepote_desktop/providers/zaposleni_provider.dart';
import 'package:esalonljepote_desktop/screens/narudzba_details_screen.dart';
import 'package:esalonljepote_desktop/screens/status_narudzbe_screen.dart';
import 'package:esalonljepote_desktop/screens/termin_details_screen.dart';
import 'package:esalonljepote_desktop/widget/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';
import 'package:data_table_2/data_table_2.dart';

class NarudzbaScreen extends StatefulWidget {
  Narudzba? narudzba;
  NarudzbaScreen({Key? key, this.narudzba}) : super(key: key);

  @override
  State<NarudzbaScreen> createState() => _NarudzbaScreen();
}

class _NarudzbaScreen extends State<NarudzbaScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  late KorisnikProvider _korisnikProvider;
  late NarudzbaProvider _narudzbaProvider;
  late ProizvodProvider _proizvodProvider;
  late PlacanjeProvider _placanjeProvider;
  late StatusProvider _statusProvider;

  SearchResult<Korisnik>? korisnikResult;
  SearchResult<Narudzba>? narudzbaResult;
  SearchResult<Proizvod>? proizvodResult;
  SearchResult<Placanje>? placanjeResult;
  SearchResult<Status>? statusResult;

  TextEditingController _imeKlijentaController = TextEditingController();
  TextEditingController _prezimeKlijentaController = TextEditingController();
  TextEditingController _sadrzajNarudzbeController = TextEditingController();
  TextEditingController _iznosController = TextEditingController();
  TextEditingController _kolicinaController = TextEditingController();
  TextEditingController _statusNarudzbeController = TextEditingController();

  DateTime? _datumOd;
  DateTime? _datumDo;

  bool searchExecuted = false;
  Timer? _debounce;
  String? _selectedStatusId;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _korisnikProvider = context.read<KorisnikProvider>();
    _narudzbaProvider = context.read<NarudzbaProvider>();
    _proizvodProvider = context.read<ProizvodProvider>();
    _placanjeProvider = context.read<PlacanjeProvider>();
    _statusProvider = context.read<StatusProvider>();

    _fetchNarudzba();
  }

  Future<void> _fetchNarudzba() async {
    var korisnikData = await _korisnikProvider.get();
    var proizvodData = await _proizvodProvider.get();
    var narudzbaData = await _narudzbaProvider.get();
    var placanjeData = await _placanjeProvider.get();
    var statusData = await _statusProvider.get();

    setState(() {
      korisnikResult = korisnikData;
      proizvodResult = proizvodData;
      narudzbaResult = narudzbaData;
      placanjeResult = placanjeData;
      statusResult = statusData;
    });
  }

  void _onSearchChanged() {
    if (_debounce?.isActive ?? false) _debounce?.cancel();
    _debounce = Timer(const Duration(milliseconds: 300), () async {
      var data = await _narudzbaProvider.get(filter: {
        'imeKlijenta': _imeKlijentaController.text.trim(),
        'prezimeKlijenta': _prezimeKlijentaController.text.trim(),
        'sadrzajNarudzbe': _sadrzajNarudzbeController.text.trim(),
        'kolicinaProizvoda': _kolicinaController.text.trim(),
        'iznosNarudzbe': _iznosController.text.trim(),
        'nazivStatusa': _statusNarudzbeController.text.trim(),
      });
      setState(() {
        narudzbaResult = data;
      });
    });
  }

  Future<void> _searchData() async {
    var filter = {
      'imeKlijenta': _imeKlijentaController.text.trim(),
      'prezimeKlijenta': _prezimeKlijentaController.text.trim(),
      'sadrzajNarudzbe': _sadrzajNarudzbeController.text.trim(),
      'kolicinaProizvoda': _kolicinaController.text.isNotEmpty
          ? int.parse(_kolicinaController.text)
          : null,
      'iznosNarudzbe': _iznosController.text.isNotEmpty
          ? double.parse(_iznosController.text)
          : null,
      'datumOd': _datumOd?.toIso8601String(),
      'datumDo': _datumDo?.toIso8601String(),
      'nazivStatusa': _statusNarudzbeController.text.trim(),
    };
    if (_selectedStatusId != null && _selectedStatusId!.isNotEmpty) {
      filter['statusNarudzbeId'] = _selectedStatusId!;
    }

    var data = await _narudzbaProvider.get(filter: filter);

    setState(() {
      narudzbaResult = data;
      searchExecuted = true;
    });
  }

  Map<String, Color> statusBoje = {
    "reirano": Colors.blue,
    "poslano": Colors.green,
    "otkazano": Colors.red,
  };

  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.all(12.0),
      child: Card(
        elevation: 4,
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            children: [
              Row(
                children: [
                  Expanded(
                    child: TextField(
                      controller: _imeKlijentaController,
                      onChanged: (value) => _onSearchChanged(),
                      decoration: InputDecoration(
                        prefixIcon: Icon(Icons.person,
                            color: Color.fromARGB(255, 34, 78, 57)),
                        labelText: "Ime klijenta",
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(12),
                          borderSide: BorderSide.none,
                        ),
                        filled: true,
                        fillColor: Colors.white,
                      ),
                    ),
                  ),
                  SizedBox(width: 12),
                  Expanded(
                    child: TextField(
                      controller: _prezimeKlijentaController,
                      onChanged: (value) => _onSearchChanged(),
                      decoration: InputDecoration(
                        prefixIcon: Icon(Icons.person_outline,
                            color: Color.fromARGB(255, 173, 160, 117)),
                        labelText: "Prezime klijenta",
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(12),
                          borderSide: BorderSide.none,
                        ),
                        filled: true,
                        fillColor: Colors.white,
                      ),
                    ),
                  ),
                ],
              ),
              SizedBox(height: 12),
              Row(
                children: [
                  Expanded(
                    child: TextField(
                      controller: _kolicinaController,
                      decoration:
                          InputDecoration(labelText: "Količina proizvoda"),
                      keyboardType: TextInputType.number,
                    ),
                  ),
                  SizedBox(width: 12),
                  Expanded(
                    child: TextField(
                      controller: _iznosController,
                      decoration: InputDecoration(labelText: "Iznos narudžbe"),
                      keyboardType: TextInputType.number,
                    ),
                  ),
                  SizedBox(width: 12),
                  Expanded(
                    child: TextField(
                      controller: _sadrzajNarudzbeController,
                      decoration:
                          InputDecoration(labelText: "Sadržaj narudžbe"),
                    ),
                  ),
                  SizedBox(width: 12),
                  Expanded(
                    child: TextField(
                      controller: _statusNarudzbeController,
                      decoration: InputDecoration(labelText: "Status narudžbe"),
                    ),
                  ),
                ],
              ),
              SizedBox(height: 12),
              Row(
                children: [
                  Row(
                    children: [
                      Text(_datumOd == null
                          ? "Datum od"
                          : _datumOd.toString().split(" ")[0]),
                      IconButton(
                        icon: Icon(Icons.calendar_today),
                        onPressed: () async {
                          DateTime? selected = await showDatePicker(
                            context: context,
                            initialDate: DateTime.now(),
                            firstDate: DateTime(2000),
                            lastDate: DateTime(2100),
                          );
                          if (selected != null) {
                            setState(() {
                              _datumOd = selected;
                            });
                          }
                        },
                      ),
                      if (_datumOd != null)
                        IconButton(
                          icon: Icon(Icons.close, color: Colors.red),
                          onPressed: () {
                            setState(() {
                              _datumOd = null;
                            });
                          },
                        ),
                    ],
                  ),
                  SizedBox(width: 12),
                  Row(
                    children: [
                      Text(_datumDo == null
                          ? "Datum do"
                          : _datumDo.toString().split(" ")[0]),
                      IconButton(
                        icon: Icon(Icons.calendar_today),
                        onPressed: () async {
                          DateTime? selected = await showDatePicker(
                            context: context,
                            initialDate: DateTime.now(),
                            firstDate: DateTime(2000),
                            lastDate: DateTime(2100),
                          );
                          if (selected != null) {
                            setState(() {
                              _datumDo = selected;
                            });
                          }
                        },
                      ),
                      if (_datumDo != null)
                        IconButton(
                          icon: Icon(Icons.close, color: Colors.red),
                          onPressed: () {
                            setState(() {
                              _datumDo = null;
                            });
                          },
                        ),
                    ],
                  ),
                  Spacer(),
                  ElevatedButton(
                    onPressed: _searchData,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Color.fromARGB(255, 173, 160, 117),
                      padding:
                          EdgeInsets.symmetric(horizontal: 24, vertical: 16),
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(12)),
                    ),
                    child:
                        Text("Pretraži", style: TextStyle(color: Colors.white)),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildDataListView() {
    if (narudzbaResult == null || narudzbaResult!.result.isEmpty) {
      return Center(child: Text("Nema podataka za prikaz."));
    }

    // Mapa statusa i boja
    Map<String, Color> statusBoje = {
      "kreirano": Colors.blue,
      "poslano": Colors.green,
      "otkazano": Colors.red,
    };

    // Mapa statusa i ikona
    Map<String, IconData> statusIkone = {
      "kreirano": Icons.hourglass_empty,
      "poslano": Icons.check_circle,
      "otkazano": Icons.cancel,
    };

    return Padding(
      padding: const EdgeInsets.all(12.0),
      child: ListView.builder(
        itemCount: narudzbaResult!.result.length,
        itemBuilder: (context, index) {
          Narudzba e = narudzbaResult!.result[index];

          var korisnik = korisnikResult?.result.firstWhere(
            (p) => p.korisnikId == e.korisnikId,
            orElse: () => Korisnik(korisnikId: 0, ime: '', prezime: ''),
          );

          var proizvod = proizvodResult?.result.firstWhere(
            (p) => p.proizvodId == e.proizvodId,
            orElse: () => Proizvod(proizvodId: 0, nazivProizvoda: ''),
          );

          var placanje = placanjeResult?.result.firstWhere(
            (p) => p.placanjeId == e.placanjeId,
            orElse: () => Placanje(placanjeId: 0, nacinPlacanja: ''),
          );

          var status = statusResult?.result.firstWhere(
            (s) => s.statusNarudzbeId == e.statusNarudzbeId,
          );

          String statusKey = status!.naziv!.toLowerCase();
          Color badgeColor = statusBoje[statusKey] ?? Colors.grey;
          IconData badgeIcon = statusIkone[statusKey] ?? Icons.help_outline;

          return Card(
            margin: EdgeInsets.symmetric(vertical: 8),
            elevation: 4,
            shape:
                RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
            child: Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(
                        "${korisnik!.ime} ${korisnik.prezime}",
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      Container(
                        padding:
                            EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                        decoration: BoxDecoration(
                          color: badgeColor,
                          borderRadius: BorderRadius.circular(12),
                        ),
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          children: [
                            Icon(badgeIcon, color: Colors.white, size: 16),
                            SizedBox(width: 4),
                            Text(
                              status.naziv!,
                              style: TextStyle(color: Colors.white),
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                  SizedBox(height: 8),
                  Text("Proizvod: ${proizvod!.nazivProizvoda}"),
                  Text("Plaćanje: ${placanje!.nacinPlacanja}"),
                  Text("Datum: ${e.datumNarudzbe.toString().split(' ')[0]}"),
                  Text("Količina: ${e.kolicinaProizvoda}"),
                  Text("Iznos: ${e.iznosNarudzbe}"),
                  Text("Telefon korisnika: ${korisnik.telefon ?? ''}"),
                  Text("Email korisnika: ${korisnik.email ?? ''}"),
                  SizedBox(height: 8),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                      ElevatedButton(
                        onPressed: () async {
                          final result = await Navigator.of(context).push(
                            MaterialPageRoute(
                              builder: (_) => StatusNarudzbaScreen(
                                  narudzbaId: e.narudzbaId!),
                            ),
                          );

                          if (result == true) {
                            _fetchNarudzba();
                          }
                        },
                        child: Text("Status"),
                      ),
                    ],
                  ),
                ],
              ),
            ),
          );
        },
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    double maxWidth = 1000;
    return MasterScreenWidget(
      child: Container(
          decoration: BoxDecoration(
            image: DecorationImage(
              image: AssetImage("assets/images/homepage.png"),
              fit: BoxFit.cover,
            ),
          ),
          child: Center(
            child: ConstrainedBox(
              constraints: BoxConstraints(maxWidth: maxWidth),
              child: Column(
                children: [
                  _buildSearch(),
                  const SizedBox(height: 8.0),
                  Expanded(child: _buildDataListView()),
                  const SizedBox(height: 8.0),
                  ElevatedButton(
                    onPressed: () async {
                      final result = await Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (context) => NarudzbaDetailsScreen(
                            onDataChanged: _fetchNarudzba,
                          ),
                        ),
                      );
                      if (result != null) {
                        _fetchNarudzba();
                      }
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Color.fromARGB(255, 173, 160, 117),
                      padding:
                          EdgeInsets.symmetric(horizontal: 24, vertical: 16),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),
                    child: Text(
                      "Dodaj novu narudžbu!",
                      style: TextStyle(color: Colors.white),
                    ),
                  ),
                ],
              ),
            ),
          )),
    );
  }

  void _refreshNarudzba() async {
    var narudzbaData = await _narudzbaProvider.get();

    setState(() {
      narudzbaResult = narudzbaData;
    });
  }
}
