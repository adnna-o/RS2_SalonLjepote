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

//Inicijalizacija providera
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

//buduca funkcija
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
              // Prvi red: ime i prezime klijenta
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

              // Drugi red: količina, iznos i sadržaj
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
                      decoration:
                          InputDecoration(labelText: "Status narudžbe"),
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
                      // Dugme za poništavanje
                      if (_datumOd != null)
                        IconButton(
                          icon: Icon(Icons.close, color: Colors.red),
                          onPressed: () {
                            setState(() {
                              _datumOd = null; // poništava datum
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
                      // Dugme za poništavanje
                      if (_datumDo != null)
                        IconButton(
                          icon: Icon(Icons.close, color: Colors.red),
                          onPressed: () {
                            setState(() {
                              _datumDo = null; // poništava datum
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
  return Padding(
    padding: const EdgeInsets.all(12.0),
    child: Card(
      elevation: 4,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      child: LayoutBuilder(
        builder: (context, constraints) {
          return DataTable2(
            columnSpacing: 12,
            horizontalMargin: 12,
            minWidth: 700, // minimalna širina bez overflowa
            headingRowColor: MaterialStateProperty.all(
              Color.fromARGB(255, 173, 160, 117),
            ),
            headingTextStyle:
                TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
            columns: const [
              DataColumn2(label: Text('Korisnik'), size: ColumnSize.L),
              DataColumn2(label: Text('Proizvod'), size: ColumnSize.M),
              DataColumn2(label: Text('Plaćanje'), size: ColumnSize.S),
              DataColumn2(label: Text('Datum'), size: ColumnSize.S),
              DataColumn2(label: Text('Količina'), size: ColumnSize.S),
              DataColumn2(label: Text('Iznos'), size: ColumnSize.S),
              DataColumn2(label: Text('Akcije'), size: ColumnSize.S),
            ],
            rows: (narudzbaResult?.result ?? []).map((Narudzba e) {
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

              return DataRow(cells: [
                DataCell(Text("${korisnik!.ime} ${korisnik.prezime}",
                    overflow: TextOverflow.ellipsis)),
                DataCell(Text(proizvod!.nazivProizvoda!,
                    overflow: TextOverflow.ellipsis)),
                DataCell(Text(placanje!.nacinPlacanja!,
                    overflow: TextOverflow.ellipsis)),
                DataCell(Text(
                    e.datumNarudzbe.toString().split(" ")[0],
                    overflow: TextOverflow.ellipsis)),
                DataCell(Text(e.kolicinaProizvoda.toString())),
                DataCell(Text(e.iznosNarudzbe.toString())),
                DataCell(
                  ElevatedButton(
                    onPressed: () {
                      Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (_) =>
                              StatusNarudzbaScreen(narudzbaId: e.narudzbaId!),
                        ),
                      );
                    },
                    child: Text("Status"),
                  ),
                ),
              ]);
            }).toList(),
          );
        },
      ),
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
            // centriramo sadržaj horizontalno
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
