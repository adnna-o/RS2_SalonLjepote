import 'dart:convert';
import 'package:esalonljepote_desktop/models/klijenti.dart';
import 'package:esalonljepote_desktop/models/korisnik.dart';
import 'package:esalonljepote_desktop/models/zaposleni.dart';
import 'package:esalonljepote_desktop/providers/klijenti_provider.dart';
import 'package:esalonljepote_desktop/providers/korisnik_provider.dart';
import 'package:esalonljepote_desktop/providers/zaposleni_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:http/http.dart' as http;

class ZaposleniDetailsScreen extends StatefulWidget {
  final Zaposleni? zaposleni;
  final Function? onDataChanged;

  ZaposleniDetailsScreen({Key? key, this.zaposleni, this.onDataChanged})
      : super(key: key);

  @override
  _ZaposleniDetailsScreen createState() => _ZaposleniDetailsScreen();
}

class _ZaposleniDetailsScreen extends State<ZaposleniDetailsScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  late ZaposleniProvider _zaposleniProvider;
  late KorisnikProvider _korisnikProvider;

  List<Korisnik>? _korisnici;
  late Map<String, dynamic> _initialValue;
  String? _selectedKorisniciId;
  bool isLoading = false;

  @override
  void initState() {
    super.initState();
    _initialValue = {
      'korisnikId': widget.zaposleni?.korisnikId,
      'zaposleniId': widget.zaposleni?.zaposleniId,
      'zanimanje': widget.zaposleni?.zanimanje,
      'datumZaposlenja': widget.zaposleni?.datumZaposlenja,
    };
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _zaposleniProvider = context.read<ZaposleniProvider>();
    _korisnikProvider = context.read<KorisnikProvider>();
    _fetchKorisnici();
  }

  Future<void> _fetchKorisnici() async {
    try {
      var korisnikData = await _korisnikProvider.get();
      setState(() {
        _korisnici = korisnikData.result;
      });
    } catch (e) {
      print('Error fetching korisnici: $e');
    }
  }

  Future<void> _submitForm() async {
    if (_formKey.currentState!.saveAndValidate()) {
      final formData = _formKey.currentState!.value;
      final mutableFormData = Map<String, dynamic>.from(formData);
      if (mutableFormData['korisnikId'] != null) {
        mutableFormData['korisnikId'] =
            int.tryParse(mutableFormData['korisnikId'] as String) ?? 0;
      }
      // Formatiranje datumZaposlenja
      if (mutableFormData['datumZaposlenja'] != null &&
          mutableFormData['datumZaposlenja'] is DateTime) {
        mutableFormData['datumZaposlenja'] =
            (mutableFormData['datumZaposlenja'] as DateTime).toIso8601String();
      }

      try {
        if (widget.zaposleni == null) {
          await _zaposleniProvider.insert(Zaposleni.fromJson(mutableFormData));
          _showSuccessMessage('Zaposleni uspešno dodan!');
        } else {
          await _zaposleniProvider.update(
            widget.zaposleni!.zaposleniId!,
            Zaposleni.fromJson(mutableFormData),
          );
          _showSuccessMessage('Zaposleni uspešno ažuriran!');
        }

        if (widget.onDataChanged != null) {
          widget.onDataChanged!();
        }

        Navigator.of(context).pop();
      } catch (e) {
        print('Error: $e');
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
              content:
                  Text('Greška prilikom čuvanja podataka! Pokušajte ponovo.')),
        );
      }
    }
  }

  void _showSuccessMessage(String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text(message), backgroundColor: Colors.green[400]),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
            widget.zaposleni == null ? 'Dodaj Zaposlenog' : 'Uredi Zaposlenog'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: FormBuilder(
            key: _formKey,
            initialValue: _initialValue,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                FormBuilderDropdown<String>(
                  name: 'korisnikId',
                  decoration: InputDecoration(
                    labelText: 'Zaposleni',
                  ),
                  items: _korisnici?.map((korisnik) {
                        return DropdownMenuItem<String>(
                          value: korisnik.korisnikId.toString(),
                          child: Text("${korisnik.ime} ${korisnik.prezime}"),
                        );
                      }).toList() ?? [],
                  initialValue: _initialValue['korisnikId']?.toString(),
                  onChanged: (value) {
                    setState(() {
                      _selectedKorisniciId = value;
                    });
                  },
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Ovo polje je obavezno!';
                    }
                    return null;
                  },
                ),
                SizedBox(height: 16),
                FormBuilderTextField(
                  name: 'zanimanje',
                  decoration: InputDecoration(labelText: 'Zanimanje'),
                  validator: (value) {
                    if (value == null || value.trim().isEmpty) {
                      return 'Zanimanje je obavezno!';
                    }
                    return null;
                  },
                ),
                SizedBox(height: 16),
                FormBuilderDateTimePicker(
                  name: 'datumZaposlenja',
                  inputType: InputType.date,
                  decoration: InputDecoration(
                    labelText: 'Datum Zaposlenja',
                    border: OutlineInputBorder(),
                    suffixIcon: Icon(Icons.calendar_today),
                  ),
                  format: DateFormat('yyyy-MM-dd'),
                  initialValue: widget.zaposleni?.datumZaposlenja,
                  enabled: widget.zaposleni == null,  // Ako je zaposleni null, polje je editabilno
                ),
                SizedBox(height: 24),
                ElevatedButton(
                  onPressed: _submitForm,
                  child: Text(widget.zaposleni == null ? 'Dodaj' : 'Spremi'),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
