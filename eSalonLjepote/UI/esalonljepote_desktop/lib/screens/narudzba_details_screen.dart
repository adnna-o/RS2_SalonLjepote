import 'dart:collection';
import 'dart:convert';

import 'package:esalonljepote_desktop/models/klijenti.dart';
import 'package:esalonljepote_desktop/models/korisnik.dart';
import 'package:esalonljepote_desktop/models/narudzba.dart';
import 'package:esalonljepote_desktop/models/placanje.dart';
import 'package:esalonljepote_desktop/models/proizvod.dart';
import 'package:esalonljepote_desktop/models/termini.dart';
import 'package:esalonljepote_desktop/models/usluga.dart';
import 'package:esalonljepote_desktop/models/zaposleni.dart';
import 'package:esalonljepote_desktop/providers/klijenti_provider.dart';
import 'package:esalonljepote_desktop/providers/korisnik_provider.dart';
import 'package:esalonljepote_desktop/providers/narudzba_provider.dart';
import 'package:esalonljepote_desktop/providers/placanje_provider.dart';
import 'package:esalonljepote_desktop/providers/proizvod_provider.dart';
import 'package:esalonljepote_desktop/providers/termini_provider.dart';
import 'package:esalonljepote_desktop/providers/usluga_provider.dart';
import 'package:esalonljepote_desktop/providers/zaposleni_provider.dart';
import 'package:esalonljepote_desktop/widget/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';
import 'package:http/http.dart' as http;

class NarudzbaDetailsScreen extends StatefulWidget {
  final Narudzba? narudzba;
  final Function? onDataChanged;
  NarudzbaDetailsScreen({Key? key, this.narudzba, this.onDataChanged})
      : super(key: key);

  @override
  State<NarudzbaDetailsScreen> createState() => _NarudzbaDetailsScreen();
}

class _NarudzbaDetailsScreen extends State<NarudzbaDetailsScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  late KorisnikProvider _korisnikProvider;
  late NarudzbaProvider _narudzbaProvider;
  late ProizvodProvider _proizvodProvider;
  late PlacanjeProvider _placanjeProvider;

  List<Korisnik>? korisnikResult;
  List<Narudzba>? narudzbaResult;
  List<Proizvod>? proizvodResult;
  List<Placanje>? placanjeResult;

  String? _selectedKorisnikId;
  String? _selectedProizvodId;
  String? _selectedPlacanjeId;
  String? _klijentIme;
  String? _zaposleniIme;

  late Map<String, dynamic> _initialValue;

  bool isLoading = false;
  bool _hasUnsavedChanges = false;

  @override
  void initState() {
    String todayFormatted =
        "${DateTime.now().year}-${DateTime.now().month.toString().padLeft(2, '0')}-${DateTime.now().day.toString().padLeft(2, '0')}";
    super.initState();
    _initialValue = {
      'narudzbaId': widget.narudzba?.narudzbaId,
      'korisnikId': widget.narudzba?.korisnikId,
      'proizvodId': widget.narudzba?.proizvodId,
      'placanjeId': widget.narudzba?.placanjeId,
      'datumNarudzbe': widget.narudzba?.datumNarudzbe ?? todayFormatted,
      'kolicinaProizvoda': widget.narudzba?.kolicinaProizvoda.toString(),
      'iznosNarudzbe': widget.narudzba?.iznosNarudzbe,
    };
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _narudzbaProvider = context.read<NarudzbaProvider>();
    _placanjeProvider = context.read<PlacanjeProvider>();
    _proizvodProvider = context.read<ProizvodProvider>();
    _korisnikProvider = context.read<KorisnikProvider>();

    _fecthKorisnici();
    _fetchNarudzba();
    _fetchProizvod();
    _fetchPlacanje();
  }

  Future<void> _fetchNarudzba() async {
    try {
      var narudzbaData = await _narudzbaProvider.get();
      setState(() {
        narudzbaResult = narudzbaData.result;
      });
    } catch (e) {
      print('Error fetching usluga: $e');
    }
  }

  Future<void> _fecthKorisnici() async {
    try {
      var korisnikData = await _korisnikProvider.get();
      setState(() {
        korisnikResult = korisnikData.result;
      });
    } catch (e) {
      print('Error fetching korisnik: $e');
    }
  }

  Future<void> _fetchProizvod() async {
    try {
      var proizvodData = await _proizvodProvider.get();
      setState(() {
        proizvodResult = proizvodData.result;
      });
    } catch (e) {
      print('Error fetching appointments: $e');
    }
  }

  Future<void> _fetchPlacanje() async {
    try {
      var placanjeData = await _placanjeProvider.get();
      setState(() {
        placanjeResult = placanjeData.result;
      });
    } catch (e) {
      print('Error fetching appointments: $e');
    }
  }

  void _showSuccessMessage(String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(message),
        backgroundColor: Colors.green[400],
        duration: Duration(seconds: 3),
      ),
    );
  }

  Future<void> _submitForm() async {
    if (_formKey.currentState!.saveAndValidate()) {
      final formData = _formKey.currentState!.value;

      final mutableFormData = Map<String, dynamic>.from(formData);

      // --- PARSIRANJE FK POLJA ---
      mutableFormData['korisnikId'] =
          int.tryParse(mutableFormData['korisnikId'].toString());

      mutableFormData['placanjeId'] =
          int.tryParse(mutableFormData['placanjeId'].toString());

      mutableFormData['proizvodId'] =
          int.tryParse(mutableFormData['proizvodId'].toString());

      // --- KOLICINA ---
      var kolicinaStr = mutableFormData['kolicinaProizvoda']?.toString().trim();

      if (kolicinaStr == null || kolicinaStr.isEmpty) {
        mutableFormData['kolicinaProizvoda'] = null;
      } else {
        mutableFormData['kolicinaProizvoda'] = int.tryParse(kolicinaStr);
      }

      // --- CIJENA PROIZVODA ---
      var iznosStr = mutableFormData['iznosNarudzbe']?.toString().trim();

      double? cijena = double.tryParse(iznosStr?.replaceAll(',', '.') ?? '');

      mutableFormData['iznosNarudzbe'] = cijena;

      // --- TOTAL ---
      if (mutableFormData['kolicinaProizvoda'] != null &&
          cijena != null &&
          mutableFormData['kolicinaProizvoda'] > 0) {
        mutableFormData['iznosNarudzbe'] =
            cijena * mutableFormData['kolicinaProizvoda'];
      }

      try {
        if (widget.narudzba == null) {
          await _narudzbaProvider.insert(Narudzba.fromJson(mutableFormData));
          _showSuccessMessage('Narudžba uspješno dodana!');
        } else {
          await _narudzbaProvider.update(
              widget.narudzba!.narudzbaId!, Narudzba.fromJson(mutableFormData));
          _showSuccessMessage('Narudžba uspješno ažurirana!');
        }

        widget.onDataChanged?.call();
        Navigator.of(context).pop();
      } catch (e) {
        print('Error: $e');
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Greška prilikom spremanja narudžbe.')),
        );
      }
    }
  }

  Future<bool> _confirmDiscardIfNeeded() async {
    if (!_hasUnsavedChanges) return true;

    final discard = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text("Odbaciti promjene?"),
        content: const Text(
            "Napravili ste izmjene koje nisu spašene. Želite li odustati i odbaciti promjene?"),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(context).pop(false),
            child: const Text("Nastavi uređivanje"),
          ),
          ElevatedButton(
            onPressed: () => Navigator.of(context).pop(true),
            child: const Text("Odbaci"),
          ),
        ],
      ),
    );

    return discard ?? false;
  }

  void _successDialogADD(String message) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: Text('Success'),
          content: Text(message),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: Text('OK'),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: FormBuilder(
            key: _formKey,
            initialValue: _initialValue,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  widget.narudzba == null
                      ? 'Dodaj narudzbu'
                      : 'Uredi narudzbu',
                  style: TextStyle(
                    color: Colors.black,
                    fontSize: 24,
                  ),
                ),
                FormBuilderDropdown<String>(
                    name: 'korisnikId',
                    decoration: InputDecoration(
                      labelText: 'Klijent',
                    ),
                    items: korisnikResult?.map((klijent) {
                          var korisnik = korisnikResult?.firstWhere(
                            (k) => k.korisnikId == klijent.korisnikId,
                          );

                          var displayText = korisnik != null
                              ? "${korisnik.ime} ${korisnik.prezime}"
                              : "Nepoznato";
                          return DropdownMenuItem<String>(
                            value: klijent.korisnikId.toString(),
                            child: Text(displayText),
                          );
                        }).toList() ??
                        [],
                    initialValue: _initialValue['korisnikId']?.toString(),
                    onChanged: (value) {
                      setState(() {
                        _selectedKorisnikId = value;
                      });
                      print("Odabrani klijentId: $_selectedKorisnikId");
                    },
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Ovo polje je obavezno!';
                      }
                      return null;
                    }),
                SizedBox(height: 16.0),
                FormBuilderTextField(
                  decoration: InputDecoration(
                    labelText: "Datum narudzbe",
                    border: OutlineInputBorder(),
                    suffixIcon: Icon(Icons.calendar_today),
                  ),
                  name: "datumNarudzbe",
                  readOnly: true,
                  initialValue: _initialValue['datumNarudzbe'],
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Ovo polje je obavezno! Datum u formatu yyyy-MM-dd';
                    }
                    return null;
                  },
                ),
                SizedBox(height: 16),
                FormBuilderDropdown<String>(
                    name: 'proizvodId',
                    decoration: InputDecoration(
                      labelText: 'Proizvod',
                    ),
                    items: proizvodResult?.map((proizvod) {
                          var proizvodi = proizvodResult?.firstWhere(
                            (k) => k.proizvodId == proizvod.proizvodId,
                          );

                          var displayText = proizvodi != null
                              ? "${proizvodi.nazivProizvoda}"
                              : "Nepoznato";
                          return DropdownMenuItem<String>(
                            value: proizvod.proizvodId.toString(),
                            child: Text(displayText),
                          );
                        }).toList() ??
                        [],
                    initialValue: _initialValue['proizvodId']?.toString(),
                    onChanged: (value) {
                      setState(() {
                        _selectedProizvodId = value;
                      });

                      // Pronađi odabrani proizvod
                      var selectedProizvod = proizvodResult?.firstWhere(
                        (p) => p.proizvodId.toString() == value,
                      );

                      if (selectedProizvod != null) {
                        // Postavi cijenu u formi
                        _formKey.currentState?.fields['iznosNarudzbe']
                            ?.didChange(
                                selectedProizvod.cijena?.toStringAsFixed(2));

                        // Automatski izračunaj iznos ako je već unesena količina
                        var kolicinaStr = _formKey.currentState
                                ?.fields['kolicinaProizvoda']?.value ??
                            '1';
                        var kolicina =
                            int.tryParse(kolicinaStr.toString()) ?? 1;

                        var iznos = (selectedProizvod.cijena ?? 0) * kolicina;

                        _formKey.currentState?.fields['iznosNarudzbe']
                            ?.didChange(iznos.toStringAsFixed(2));
                      }
                    },
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Ovo polje je obavezno!';
                      }
                      return null;
                    }),
                SizedBox(height: 16),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    FormBuilderTextField(
                      name: 'iznosNarudzbe',
                      readOnly: true,
                      keyboardType:
                          const TextInputType.numberWithOptions(decimal: true),
                      decoration: InputDecoration(
                        labelText: 'Cijena *',
                        isDense: true,
                        contentPadding: const EdgeInsets.symmetric(
                            vertical: 16, horizontal: 12),
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(12),
                        ),
                        focusedBorder: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(12),
                          borderSide: const BorderSide(
                              color: Colors.orange, width: 1.5),
                        ),
                        errorBorder: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(12),
                          borderSide:
                              const BorderSide(color: Colors.red, width: 1),
                        ),
                        focusedErrorBorder: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(12),
                          borderSide:
                              const BorderSide(color: Colors.red, width: 1.5),
                        ),
                        prefixIcon:
                            const Icon(Icons.euro, color: Colors.orangeAccent),
                      ),
                      validator: (v) {
                        final raw = (v ?? '').trim().replaceAll(',', '.');
                        final d = double.tryParse(raw);
                        if (raw.isEmpty) return 'Cijena je obavezna.';
                        if (d == null) return 'Unesite ispravan broj.';
                        if (d <= 0) return 'Cijena mora biti veća od 0.';
                        return null;
                      },
                    ),
                  ],
                ),
                SizedBox(
                  height: 16,
                ),
                Padding(
                  padding: const EdgeInsets.symmetric(vertical: 8.0),
                  child: Row(
                    children: [
                      Expanded(
                          child: FormBuilderTextField(
                        name: 'kolicinaProizvoda',
                        initialValue:
                            widget.narudzba?.kolicinaProizvoda?.toString() ??
                                '',
                        decoration: const InputDecoration(
                          labelText: 'Količina proizvoda',
                          hintText: 'Unesite količinu',
                          border: OutlineInputBorder(),
                        ),
                        keyboardType: TextInputType.number,
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return 'Količina je obavezna.';
                          }
                          final parsed = int.tryParse(value);
                          if (parsed == null) {
                            return 'Unesite ispravan broj.';
                          }
                          if (parsed <= 0) {
                            return 'Količina mora biti veća od 0.';
                          }
                          return null;
                        },
                      )),
                    ],
                  ),
                ),
                SizedBox(height: 16),
                FormBuilderDropdown<String>(
                    name: 'placanjeId',
                    decoration: InputDecoration(
                      labelText: 'Placanje',
                    ),
                    items: placanjeResult?.map((placanje) {
                          var placanjeNacin = placanjeResult?.firstWhere(
                            (k) => k.placanjeId == placanje.placanjeId,
                          );

                          var displayText = placanjeNacin != null
                              ? "${placanjeNacin.nacinPlacanja}"
                              : "Nepoznato";
                          return DropdownMenuItem<String>(
                            value: placanje.placanjeId.toString(),
                            child: Text(displayText),
                          );
                        }).toList() ??
                        [],
                    initialValue: _initialValue['placanjeId']?.toString(),
                    onChanged: (value) {
                      setState(() {
                        _selectedPlacanjeId = value;
                      });
                      print("Odabrani klijentId: $_selectedPlacanjeId");
                    },
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Ovo polje je obavezno!';
                      }
                      return null;
                    }),
                SizedBox(height: 16),
                SizedBox(height: 16),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    Padding(
                      padding: const EdgeInsets.all(16),
                      child: OutlinedButton(
                        onPressed: () async {
                          final canLeave = await _confirmDiscardIfNeeded();
                          if (canLeave) Navigator.of(context).pop(false);
                        },
                        child: const Text("Odustani"),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.all(16),
                      child: ElevatedButton(
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.orangeAccent,
                          padding: const EdgeInsets.symmetric(
                              horizontal: 32, vertical: 12),
                          textStyle: const TextStyle(
                              fontSize: 16, fontWeight: FontWeight.bold),
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(12)),
                        ),
                        onPressed: _submitForm,
                        child: Text(widget.narudzba == null ? 'Dodaj' : 'Spasi'),
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
      title: widget.narudzba != null
          ? "Narudzba: ${_klijentIme}"
          : "Detalji narudzbe",
    );
  }
}
