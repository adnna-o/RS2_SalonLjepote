import 'package:esalonljepote_desktop/models/klijenti.dart';
import 'package:esalonljepote_desktop/models/korisnik.dart';
import 'package:esalonljepote_desktop/providers/korisnik_provider.dart';
import 'package:esalonljepote_desktop/widget/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';

class KlijentiDetailsScreen extends StatefulWidget {
  final Klijenti? klijenti;
  final Function? onDataChanged;

  KlijentiDetailsScreen({Key? key, this.klijenti, this.onDataChanged}) : super(key: key);

  @override
  State<KlijentiDetailsScreen> createState() => _KlijentiDetailsScreenState();
}

class _KlijentiDetailsScreenState extends State<KlijentiDetailsScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  late KorisnikProvider _korisnikProvider;
  Korisnik? _korisnik;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _korisnikProvider = context.read<KorisnikProvider>();
    _loadKorisnik();
  }

  Future<void> _loadKorisnik() async {
    if (widget.klijenti?.korisnikId != null) {
      try {
        final result = await _korisnikProvider.getById(widget.klijenti!.korisnikId);
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
      setState(() {
        _korisnik = Korisnik(
          ime: '',
          prezime: '',
          email: '',
          telefon: '',
          korisnickoIme: '',
          password: '',
        );
      });
    }
  }

  Future<void> _submitForm() async {
    if (_formKey.currentState!.saveAndValidate() && _korisnik != null) {
      final formData = _formKey.currentState!.value;

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
        if (_korisnik!.korisnikId != null) {
          await _korisnikProvider.update(korisnikZaSpremanje.korisnikId!, korisnikZaSpremanje);
        } else {
          await _korisnikProvider.insert(korisnikZaSpremanje);
        }

        if (widget.onDataChanged != null) widget.onDataChanged!();

        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(_korisnik!.korisnikId != null
                ? 'Client updated successfully!'
                : 'Client added successfully!'),
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
  }

  @override
  Widget build(BuildContext context) {
    if (_korisnik == null) {
      return MasterScreenWidget(
        title: 'Loading Client...',
        child: Center(child: CircularProgressIndicator()),
      );
    }

    final isEditing = _korisnik!.korisnikId != null;

    return MasterScreenWidget(
      title: isEditing
          ? "Uredi klijenta: ${_korisnik!.ime} ${_korisnik!.prezime}"
          : "Dodaj klijenta",
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: FormBuilder(
            key: _formKey,
            initialValue: {
              'ime': _korisnik!.ime,
              'prezime': _korisnik!.prezime,
              'email': _korisnik!.email ?? '',
              'telefon': _korisnik!.telefon ?? '',
            },
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                FormBuilderTextField(
                  name: 'ime',
                  decoration: InputDecoration(labelText: 'Ime'),
                  validator: (v) => v == null || v.isEmpty ? 'Required' : null,
                ),
                SizedBox(height: 8),
                FormBuilderTextField(
                  name: 'prezime',
                  decoration: InputDecoration(labelText: 'Prezime'),
                  validator: (v) => v == null || v.isEmpty ? 'Required' : null,
                ),
                SizedBox(height: 8),
                FormBuilderTextField(
                  name: 'telefon',
                  decoration: InputDecoration(labelText: 'Telefon'),
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
