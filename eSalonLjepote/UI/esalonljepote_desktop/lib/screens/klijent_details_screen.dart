import 'package:esalonljepote_desktop/models/klijenti.dart';
import 'package:esalonljepote_desktop/models/korisnik.dart';
import 'package:esalonljepote_desktop/models/search_result.dart';
import 'package:esalonljepote_desktop/providers/klijenti_provider.dart';
import 'package:esalonljepote_desktop/providers/korisnik_provider.dart';
import 'package:esalonljepote_desktop/widget/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';

class KlijentiDetailsScreen extends StatefulWidget {
  final Klijenti? klijenti;
  final Function? onDataChanged;

  KlijentiDetailsScreen({Key? key, this.klijenti, this.onDataChanged})
      : super(key: key);

  @override
  State<KlijentiDetailsScreen> createState() => _KlijentiDetailsScreenState();
}

class _KlijentiDetailsScreenState extends State<KlijentiDetailsScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  late KorisnikProvider _korisnikProvider;
  Korisnik? _korisnik; // Za uređivanje postojećeg klijenta
  SearchResult<Korisnik>? _korisniciResult; // Za dropdown kod dodavanja
  Korisnik? _selectedKorisnik; // Odabrani korisnik kod dodavanja

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _korisnikProvider = context.read<KorisnikProvider>();
    _loadKorisnikOrUsers();
  }

  Future<void> _loadKorisnikOrUsers() async {
    if (widget.klijenti?.korisnikId != null) {
      try {
        final result =
            await _korisnikProvider.getById(widget.klijenti!.korisnikId);
        setState(() {
          _korisnik = result;
        });
      } catch (e) {
        print('Error loading korisnik: $e');
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Failed to load client data.'),
            backgroundColor: Colors.red[400],
          ),
        );
      }
    } else {
      try {
        final result = await _korisnikProvider.get();
        setState(() {
          _korisniciResult = result;
        });
      } catch (e) {
        print('Error loading korisnici: $e');
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Failed to load users.'),
            backgroundColor: Colors.red[400],
          ),
        );
      }
    }
  }

  Future<void> _submitForm() async {
    final formData = _formKey.currentState!.value;

    if (widget.klijenti != null && _korisnik != null) {
      // Uređivanje postojećeg klijenta – ostaje kao prije
      if (_formKey.currentState!.saveAndValidate()) {
        final korisnikZaSpremanje = Korisnik(
          korisnikId: _korisnik!.korisnikId,
          ime: formData['ime'],
          prezime: formData['prezime'],
          email: formData['email'],
          telefon: formData['telefon'],
          korisnickoIme: _korisnik!.korisnickoIme != null
              ? formData['ime'] + formData['prezime']
              : _korisnik!.korisnickoIme,
        );

        try {
          await _korisnikProvider.update(
              korisnikZaSpremanje.korisnikId!, korisnikZaSpremanje);

          if (widget.onDataChanged != null) widget.onDataChanged!();

          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text('Client updated successfully!'),
              backgroundColor: Colors.green[400],
            ),
          );

          Navigator.of(context).pop();
        } catch (e) {
          print('Error saving korisnik: $e');
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text('Failed to save client. Please try again.'),
              backgroundColor: Colors.red[400],
            ),
          );
        }
      }
    } else {
      // Dodavanje novog klijenta – koristi dropdown za korisnika
      if (_selectedKorisnik == null) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Morate odabrati korisnika'),
            backgroundColor: Colors.red[400],
          ),
        );
        return;
      }

      if (_formKey.currentState!.saveAndValidate()) {
        final klijentZaSpremanje = Klijenti(
          korisnikId: _selectedKorisnik!.korisnikId,
        );

        try {
          final klijentiProvider = context.read<KlijentiProvider>();
          await klijentiProvider.insert(klijentZaSpremanje);

          if (widget.onDataChanged != null) widget.onDataChanged!();

          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text('Klijent uspješno dodan!'),
              backgroundColor: Colors.green[400],
            ),
          );

          Navigator.of(context).pop();
        } catch (e) {
          print('Error saving klijent: $e');
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text('Greška prilikom dodavanja klijenta.'),
              backgroundColor: Colors.red[400],
            ),
          );
        }
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final isEditing = widget.klijenti != null && _korisnik != null;

    return MasterScreenWidget(
      title: isEditing
          ? "Uredi klijenta: ${_korisnik!.ime} ${_korisnik!.prezime}"
          : "Dodaj klijenta",
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: FormBuilder(
            key: _formKey,
            initialValue: isEditing
                ? {
                    'ime': _korisnik!.ime,
                    'prezime': _korisnik!.prezime,
                    'email': _korisnik!.email ?? '',
                    'telefon': _korisnik!.telefon ?? '',
                  }
                : {},
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                if (isEditing) ...[
                  // Polja za uređivanje kao prije
                  FormBuilderTextField(
                    name: 'ime',
                    decoration: InputDecoration(labelText: 'Ime'),
                    validator: (v) =>
                        v == null || v.isEmpty ? 'Required' : null,
                  ),
                  SizedBox(height: 8),
                  FormBuilderTextField(
                    name: 'prezime',
                    decoration: InputDecoration(labelText: 'Prezime'),
                    validator: (v) =>
                        v == null || v.isEmpty ? 'Required' : null,
                  ),
                  SizedBox(height: 8),
                  FormBuilderTextField(
                    name: 'telefon',
                    decoration: InputDecoration(labelText: 'Telefon'),
                    keyboardType: TextInputType.number,
                    inputFormatters: [
                      FilteringTextInputFormatter.digitsOnly, // samo brojevi
                      LengthLimitingTextInputFormatter(
                          9), // maksimalno 9 cifara
                    ],
                    validator: (v) {
                      if (v == null || v.isEmpty) return 'Required';
                      final regex = RegExp(r'^\d{9}$'); // tačno 9 cifara
                      if (!regex.hasMatch(v))
                        return 'Telefon mora imati 9 cifara';
                      return null;
                    },
                  ),

                  SizedBox(height: 8),
                  FormBuilderTextField(
                    name: 'email',
                    decoration: InputDecoration(labelText: 'Email'),
                    validator: (v) {
                      if (v != null && v.isNotEmpty) {
                        final regex = RegExp(r'^[^@]+@[^@]+\.[^@]+');
                        if (!regex.hasMatch(v)) return 'Invalid email';
                      }
                      return null;
                    },
                  ),
                ] else ...[
                  // Dropdown za odabir korisnika kod dodavanja
                  _korisniciResult == null
                      ? Center(child: CircularProgressIndicator())
                      : DropdownButtonFormField<Korisnik>(
                          value: _selectedKorisnik,
                          decoration:
                              InputDecoration(labelText: "Odaberi korisnika"),
                          items: _korisniciResult!.result.map((k) {
                            return DropdownMenuItem<Korisnik>(
                              value: k,
                              child: Text("${k.ime} ${k.prezime}"),
                            );
                          }).toList(),
                          onChanged: (value) {
                            setState(() {
                              _selectedKorisnik = value;
                            });
                          },
                          validator: (v) =>
                              v == null ? 'Morate odabrati korisnika' : null,
                        ),
                  SizedBox(height: 8),
                ],
                SizedBox(height: 16),
                ElevatedButton(
                  onPressed: _submitForm,
                  child: Text(isEditing ? 'Spasi' : 'Dodaj'),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
