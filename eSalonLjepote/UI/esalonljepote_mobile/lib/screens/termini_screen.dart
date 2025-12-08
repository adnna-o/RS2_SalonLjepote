import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';
import 'package:esalonljepote_mobile/models/klijenti.dart';
import 'package:esalonljepote_mobile/models/korisnik.dart';
import 'package:esalonljepote_mobile/models/termini.dart';
import 'package:esalonljepote_mobile/models/usluga.dart';
import 'package:esalonljepote_mobile/models/zaposleni.dart';
import 'package:esalonljepote_mobile/models/search_result.dart';
import 'package:esalonljepote_mobile/providers/klijenti_provider.dart';
import 'package:esalonljepote_mobile/providers/korisnik_provider.dart';
import 'package:esalonljepote_mobile/providers/termini_provider.dart';
import 'package:esalonljepote_mobile/providers/usluga_provider.dart';
import 'package:esalonljepote_mobile/providers/zaposleni_provider.dart';
import 'package:esalonljepote_mobile/widget/master_screen.dart';
import 'dart:ui';

class ZakaziTerminScreen extends StatefulWidget {
  const ZakaziTerminScreen({Key? key}) : super(key: key);

  @override
  State<ZakaziTerminScreen> createState() => _ZakaziTerminScreenState();
}

class _ZakaziTerminScreenState extends State<ZakaziTerminScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  late TerminiProvider _terminProvider;
  late UslugaProvider _uslugaProvider;
  late KorisnikProvider _korisnikProvider;
  late ZaposleniProvider _zaposleniProvider;
  late KlijentiProvider _klijentiProvider;

  List<Usluga>? _usluge;
  List<Zaposleni>? _zaposleni;
  List<Klijenti>? _klijenti;
  List<Korisnik>? _korisnici;

  Usluga? _selectedUsluga;
  Zaposleni? _selectedZaposleni;
  List<Zaposleni>? _filteredZaposleni;

  DateTime? _selectedDate;
  final TextEditingController _vrijemeController = TextEditingController();

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _terminProvider = context.read<TerminiProvider>();
    _uslugaProvider = context.read<UslugaProvider>();
    _korisnikProvider = context.read<KorisnikProvider>();
    _zaposleniProvider = context.read<ZaposleniProvider>();
    _klijentiProvider = context.read<KlijentiProvider>();

    _fetchData();
  }

  List<Termini>? _zauzetiTermini;

  Future<void> _fetchData() async {
    try {
      final klijenti = await _klijentiProvider.get();
      final usluge = await _uslugaProvider.get();
      final zaposleni = await _zaposleniProvider.get();
      final korisnici = await _korisnikProvider.get();
      final termini = await _terminProvider.get();

      setState(() {
        _klijenti = klijenti.result;
        _usluge = usluge.result;
        _zaposleni = zaposleni.result;
        _filteredZaposleni = _zaposleni;
        _korisnici = korisnici.result;
        _zauzetiTermini = termini.result;
      });
    } catch (e) {
      debugPrint('Greška pri dohvatu podataka: $e');
    }
  }

  Future<void> _rezervisiTermin() async {
    final currentUser = _korisnikProvider.currentUser;

    if (currentUser == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
            content: Text('Morate biti prijavljeni da rezervišete termin')),
      );
      return;
    }

    final klijent = _klijenti?.firstWhere(
      (k) => k.korisnikId == currentUser.korisnikId,
      orElse: () => Klijenti(klijentId: 0),
    );

    if (klijent == null || klijent.klijentId == 0) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Vaš klijent zapis nije pronađen')),
      );
      return;
    }

    if (_selectedUsluga == null ||
        _selectedDate == null ||
        _selectedZaposleni == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Odaberite sve potrebne informacije')),
      );
      return;
    }

    final noviTermin = Termini(
      klijentId: klijent.klijentId,
      uslugaId: _selectedUsluga!.uslugaId,
      zaposleniId: _selectedZaposleni!.zaposleniId,
      datumTermina: _selectedDate,
      vrijemeTermina: _vrijemeController.text,
    );

    try {
      await _terminProvider.insert(noviTermin.toJson());
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Termin uspješno rezervisan ✅')),
      );
      Navigator.pop(context);
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Došlo je do greške, pokušajte ponovo')),
      );
    }
  }

  Future<void> _pickDate() async {
    final now = DateTime.now();

    final selected = await showDatePicker(
      context: context,
      initialDate: now,
      firstDate: now,
      lastDate: DateTime(now.year, now.month + 1, 0),
      selectableDayPredicate: (day) => true,
      builder: (context, child) => Theme(
        data: Theme.of(context).copyWith(
          colorScheme: ColorScheme.light(
            primary: Colors.pink.shade400,
            onPrimary: Colors.white,
            onSurface: Colors.black87,
          ),
        ),
        child: child!,
      ),
    );

    if (selected != null) setState(() => _selectedDate = selected);
  }

  List<String> _dostupniSati(DateTime dan, int zaposleniId) {
    final sveOpcije = List.generate(10, (i) {
      final sat = 9 + i;
      return '${sat.toString().padLeft(2, '0')}:00';
    });

    final zauzeti = _zauzetiTermini
            ?.where((t) =>
                t.zaposleniId == zaposleniId &&
                t.datumTermina?.day == dan.day &&
                t.datumTermina?.month == dan.month &&
                t.datumTermina?.year == dan.year)
            .map((t) => _cleanTime(t.vrijemeTermina))
            .toList() ??
        [];

    return sveOpcije.where((sat) => !zauzeti.contains(sat)).toList();
  }

  String _cleanTime(String? time) {
  if (time == null) return "";
  return time.length >= 5 ? time.substring(0, 5) : time;
}


  @override
  Widget build(BuildContext context) {
    final currentUser = _korisnikProvider.currentUser;

    if (currentUser == null) {
      return MasterScreenWidget(
        child: Center(
          child: Text(
            "Molimo prijavite se da biste rezervisali termin 💇‍♀️",
            style: const TextStyle(fontSize: 18),
          ),
        ),
      );
    }

    return MasterScreenWidget(
      child: Container(
        width: double.infinity,
        height: double.infinity,
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage('assets/images/Background.jpg'),
            fit: BoxFit.cover,
          ),
        ),
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(20),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                "Zakažite termin ✨",
                style: Theme.of(context).textTheme.headlineSmall?.copyWith(
                      fontWeight: FontWeight.bold,
                      color: Colors.pink.shade400,
                    ),
              ),
              const SizedBox(height: 10),
              Text(
                "Dobrodošli, ${currentUser.ime}",
                style: const TextStyle(fontSize: 16, color: Colors.black54),
              ),
              const SizedBox(height: 20),
              _buildCard(
                icon: Icons.spa_outlined,
                title: "Odaberite tretman",
                child: DropdownButtonFormField<Usluga>(
                  decoration: const InputDecoration(border: InputBorder.none),
                  items: _usluge?.map((u) {
                        return DropdownMenuItem<Usluga>(
                          value: u,
                          child: Text(u.nazivUsluge ?? "Nepoznato"),
                        );
                      }).toList() ??
                      [],
                  value: _selectedUsluga,
                  onChanged: (val) {
                    setState(() {
                      _selectedUsluga = val;
                      _filterZaposleniByUsluga(val);
                    });
                  },
                ),
              ),
              const SizedBox(height: 16),
              _buildCard(
                icon: Icons.person_outline,
                title: "Odaberite zaposlenog",
                child: DropdownButtonFormField<Zaposleni>(
                  decoration: const InputDecoration(border: InputBorder.none),
                  items: _filteredZaposleni?.map((z) {
                        final korisnik = _korisnici?.firstWhere(
                          (k) => k.korisnikId == z.korisnikId,
                          orElse: () => Korisnik(ime: "Nepoznato", prezime: ""),
                        );
                        return DropdownMenuItem<Zaposleni>(
                          value: z,
                          child: Text(
                              "${korisnik?.ime ?? ''} ${korisnik?.prezime ?? ''}"),
                        );
                      }).toList() ??
                      [],
                  value: _selectedZaposleni,
                  onChanged: (val) => setState(() => _selectedZaposleni = val),
                ),
              ),
              const SizedBox(height: 16),
              _buildCard(
                icon: Icons.calendar_today_outlined,
                title: "Datum termina",
                child: ListTile(
                  contentPadding: EdgeInsets.zero,
                  title: Text(
                    _selectedDate != null
                        ? "${_selectedDate!.day}.${_selectedDate!.month}.${_selectedDate!.year}"
                        : "Kliknite za odabir datuma",
                  ),
                  trailing: const Icon(Icons.edit_calendar_rounded),
                  onTap: _pickDate,
                ),
              ),
              const SizedBox(height: 16),
              _buildCard(
                icon: Icons.access_time_rounded,
                title: "Vrijeme termina",
                child: ListTile(
                  contentPadding: EdgeInsets.zero,
                  title: Text(
                    _vrijemeController.text.isNotEmpty
                        ? _vrijemeController.text
                        : 'Kliknite za odabir vremena',
                  ),
                  trailing: const Icon(Icons.schedule_rounded),
                  onTap: () async {
                    if (_selectedDate == null || _selectedZaposleni == null) {
                      ScaffoldMessenger.of(context).showSnackBar(
                        SnackBar(
                            content: Text('Prvo odaberite datum i zaposlenog')),
                      );
                      return;
                    }

                    final slobodni = _dostupniSati(
                        _selectedDate!, _selectedZaposleni!.zaposleniId!);

                    showModalBottomSheet(
                      context: context,
                      builder: (context) {
                        return ListView(
                          children: slobodni.map((sat) {
                            return ListTile(
                              title: Text(sat),
                              onTap: () {
                                _vrijemeController.text = sat;
                                Navigator.pop(context);
                                setState(() {});
                              },
                            );
                          }).toList(),
                        );
                      },
                    );
                  },
                ),
              ),
              const SizedBox(height: 24),
              SizedBox(
                width: double.infinity,
                child: ElevatedButton.icon(
                  icon: const Icon(Icons.check_circle_outline),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.pink.shade400,
                    foregroundColor: Colors.white,
                    padding: const EdgeInsets.symmetric(vertical: 14),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12),
                    ),
                  ),
                  onPressed: _rezervisiTermin,
                  label: const Text(
                    "Rezerviši termin",
                    style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildCard(
      {required IconData icon, required String title, required Widget child}) {
    return Card(
      elevation: 3,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      margin: EdgeInsets.zero,
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                Icon(icon, color: Colors.pink.shade400),
                const SizedBox(width: 8),
                Text(title,
                    style: const TextStyle(
                        fontWeight: FontWeight.w600, fontSize: 16)),
              ],
            ),
            const SizedBox(height: 10),
            child,
          ],
        ),
      ),
    );
  }

  void _filterZaposleniByUsluga(Usluga? val) {
    if (val == null) return;
    final naziv = val.nazivUsluge?.toLowerCase() ?? '';

    if (naziv.contains('frizerske')) {
      _filteredZaposleni = _zaposleni
          ?.where((z) => z.zanimanje?.toLowerCase() == 'frizer')
          .toList();
    } else if (naziv.contains('kozmeticke')) {
      _filteredZaposleni = _zaposleni
          ?.where((z) => z.zanimanje?.toLowerCase() == 'kozmeticar')
          .toList();
    } else if (naziv.contains('sminkanje')) {
      _filteredZaposleni = _zaposleni
          ?.where((z) => z.zanimanje?.toLowerCase() == 'sminker')
          .toList();
    } else {
      _filteredZaposleni = _zaposleni;
    }
  }
}
