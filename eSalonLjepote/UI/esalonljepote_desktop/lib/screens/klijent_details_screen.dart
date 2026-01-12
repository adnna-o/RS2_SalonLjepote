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

  const KlijentiDetailsScreen({
    Key? key,
    this.klijenti,
    this.onDataChanged,
  }) : super(key: key);

  @override
  State<KlijentiDetailsScreen> createState() => _KlijentiDetailsScreenState();
}

class _KlijentiDetailsScreenState extends State<KlijentiDetailsScreen> {
  final _formKey = GlobalKey<FormBuilderState>();

  late KorisnikProvider _korisnikProvider;

  Korisnik? _korisnik;
  SearchResult<Korisnik>? _korisniciResult;
  Korisnik? _selectedKorisnik;

  bool _hasUnsavedChanges = false;

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
        _showError('Failed to load client data.');
      }
    } else {
      try {
        final result = await _korisnikProvider.get();
        setState(() {
          _korisniciResult = result;
        });
      } catch (e) {
        _showError('Failed to load users.');
      }
    }
  }

  Future<void> _submitForm() async {
    if (!_formKey.currentState!.saveAndValidate()) return;

    final formData = _formKey.currentState!.value;

    /// ============================
    /// UREĐIVANJE POSTOJEĆEG KLIJENTA
    /// ============================
    if (widget.klijenti != null && _korisnik != null) {
      final korisnikZaSpremanje = Korisnik(
        korisnikId: _korisnik!.korisnikId,
        ime: formData['ime'],
        prezime: formData['prezime'],
        email: formData['email'],
        telefon: formData['telefon'],
        korisnickoIme:
            _korisnik!.korisnickoIme, // 🔒 OSTAVLJA SE ISTO
      );

      try {
        await _korisnikProvider.update(
          korisnikZaSpremanje.korisnikId!,
          korisnikZaSpremanje,
        );

        widget.onDataChanged?.call();

        _showSuccess('Client updated successfully!');
        Navigator.of(context).pop(true);
      } catch (e) {
        _showError('Failed to save client.');
      }

      return;
    }

    /// ============================
    /// DODAVANJE NOVOG KLIJENTA
    /// ============================
    if (_selectedKorisnik == null) {
      _showError('Morate odabrati korisnika');
      return;
    }

    final klijentZaSpremanje = Klijenti(
      korisnikId: _selectedKorisnik!.korisnikId,
    );

    try {
      final klijentiProvider = context.read<KlijentiProvider>();
      await klijentiProvider.insert(klijentZaSpremanje);

      widget.onDataChanged?.call();

      _showSuccess('Klijent uspješno dodan!');
      Navigator.of(context).pop(true);
    } catch (e) {
      _showError('Greška prilikom dodavanja klijenta.');
    }
  }

  Future<bool> _confirmDiscardIfNeeded() async {
    if (!_hasUnsavedChanges) return true;

    final discard = await showDialog<bool>(
      context: context,
      builder: (_) => AlertDialog(
        title: const Text("Odbaciti promjene?"),
        content: const Text(
          "Napravili ste izmjene koje nisu spašene. Želite li odustati?",
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            child: const Text("Nastavi uređivanje"),
          ),
          ElevatedButton(
            onPressed: () => Navigator.pop(context, true),
            child: const Text("Odbaci"),
          ),
        ],
      ),
    );

    return discard ?? false;
  }

  void _showError(String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text(message), backgroundColor: Colors.red[400]),
    );
  }

  void _showSuccess(String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text(message), backgroundColor: Colors.green[400]),
    );
  }

  @override
  Widget build(BuildContext context) {
    final isEditing = widget.klijenti != null && _korisnik != null;

    return MasterScreenWidget(
      title: isEditing
          ? "Uredi klijenta: ${_korisnik!.ime} ${_korisnik!.prezime}"
          : "Dodaj klijenta",
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: FormBuilder(
            key: _formKey,
            onChanged: () => _hasUnsavedChanges = true,
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
                  _buildTextField('ime', 'Ime'),
                  _buildTextField('prezime', 'Prezime'),
                  _buildPhoneField(),
                  _buildEmailField(),
                ] else ...[
                  _buildDropdown(),
                ],
                const SizedBox(height: 24),
                _buildButtons(),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildTextField(String name, String label) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 8),
      child: FormBuilderTextField(
        name: name,
        decoration: InputDecoration(labelText: label),
        validator: (v) => v == null || v.isEmpty ? 'Required' : null,
      ),
    );
  }

  Widget _buildPhoneField() {
    return Padding(
      padding: const EdgeInsets.only(bottom: 8),
      child: FormBuilderTextField(
        name: 'telefon',
        decoration: const InputDecoration(labelText: 'Telefon'),
        keyboardType: TextInputType.number,
        inputFormatters: [
          FilteringTextInputFormatter.digitsOnly,
          LengthLimitingTextInputFormatter(9),
        ],
        validator: (v) {
          if (v == null || v.isEmpty) return 'Required';
          if (!RegExp(r'^\d{9}$').hasMatch(v)) {
            return 'Telefon mora imati 9 cifara';
          }
          return null;
        },
      ),
    );
  }

  Widget _buildEmailField() {
    return Padding(
      padding: const EdgeInsets.only(bottom: 8),
      child: FormBuilderTextField(
        name: 'email',
        decoration: const InputDecoration(labelText: 'Email'),
        validator: (v) {
          if (v != null && v.isNotEmpty) {
            if (!RegExp(r'^[^@]+@[^@]+\.[^@]+').hasMatch(v)) {
              return 'Invalid email';
            }
          }
          return null;
        },
      ),
    );
  }

  Widget _buildDropdown() {
    if (_korisniciResult == null) {
      return const Center(child: CircularProgressIndicator());
    }

    return DropdownButtonFormField<Korisnik>(
      decoration: const InputDecoration(labelText: "Odaberi korisnika"),
      value: _selectedKorisnik,
      items: _korisniciResult!.result
          .map(
            (k) => DropdownMenuItem(
              value: k,
              child: Text('${k.ime} ${k.prezime}'),
            ),
          )
          .toList(),
      onChanged: (v) => setState(() => _selectedKorisnik = v),
      validator: (v) => v == null ? 'Morate odabrati korisnika' : null,
    );
  }

  Widget _buildButtons() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      children: [
        OutlinedButton(
          onPressed: () async {
            if (await _confirmDiscardIfNeeded()) {
              Navigator.of(context).pop(false);
            }
          },
          child: const Text("Odustani"),
        ),
        const SizedBox(width: 16),
        ElevatedButton(
          onPressed: _submitForm,
          style: ElevatedButton.styleFrom(
            backgroundColor: Colors.orangeAccent,
            padding: const EdgeInsets.symmetric(horizontal: 32, vertical: 12),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
          ),
          child: Text(widget.klijenti == null ? 'Dodaj' : 'Spasi'),
        ),
      ],
    );
  }
}
