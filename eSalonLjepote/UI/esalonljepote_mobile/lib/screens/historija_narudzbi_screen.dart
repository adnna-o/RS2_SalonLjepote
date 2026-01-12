import 'package:esalonljepote_mobile/models/narudzba.dart';
import 'package:esalonljepote_mobile/models/narudzbastavka.dart';
import 'package:esalonljepote_mobile/models/proizvod.dart';
import 'package:esalonljepote_mobile/providers/korisnik_provider.dart';
import 'package:esalonljepote_mobile/providers/narudzba_provider.dart';
import 'package:esalonljepote_mobile/providers/narudzba_stavka_provider.dart';
import 'package:esalonljepote_mobile/providers/proizvod_provider.dart';
import 'package:esalonljepote_mobile/screens/proizvod_screen.dart';
import 'package:esalonljepote_mobile/utils/util.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
 
class OrdersScreen extends StatefulWidget {
  @override
  _OrdersScreenState createState() => _OrdersScreenState();
}
 
class _OrdersScreenState extends State<OrdersScreen> {
  bool _isLoading = true;
  late KorisnikProvider _korisnikProvider;
  late NarudzbaProvider _narudzbaProvider;
  late NarudzbaStavkaProvider stavkeProvider;
  late ProizvodProvider productProvider;
 
  List<Narudzba> _narudzbe = [];
  Map<int, List<NarudzbaStavka>> _stavkePoNarudzbi = {};
  Map<int, Proizvod> _proizvodById = {};
  Map<int, String> _korisniciById = {};
 
  @override
  void initState() {
    super.initState();
    _loadNarudzbe();
  }
 
  Future<void> _loadNarudzbe() async {
    if (Authorization.userId == null) {
      _toast('Korisnik nije ulogovan.');
      setState(() => _isLoading = false);
      return;
    }
 
    try {
      _narudzbaProvider = context.read<NarudzbaProvider>();
      stavkeProvider = context.read<NarudzbaStavkaProvider>();
      productProvider = context.read<ProizvodProvider>();
      _korisnikProvider = context.read<KorisnikProvider>();
 
      final korisniciResult = await _korisnikProvider.get();
      _korisniciById = {
        for (final k in korisniciResult.result)
          if (k.korisnikId != null) k.korisnikId!: k.ime!,
      };
      final narudzbeResult =
          await _narudzbaProvider.getByUser(Authorization.userId!);
 
      final narudzbe = (narudzbeResult.result ?? [])
          .where((n) => n.korisnikId == Authorization.userId)
          .toList();
 
      if (narudzbe.isEmpty) {
        setState(() {
          _narudzbe = [];
          _stavkePoNarudzbi = {};
          _proizvodById = {};
          _isLoading = false;
        });
        return;
      }
 
      final stavkeResult = await stavkeProvider.get();
      final idsNarudzbi =
          narudzbe.map((n) => n.narudzbaId).whereType<int>().toSet();
 
      final Map<int, List<NarudzbaStavka>> stavkeMap = {};
      final Set<int> potrebniproizvodIds = {};
<<<<<<< HEAD

=======
 
>>>>>>> b2c5380df42d98abaa1d43c2a6fdfa1a82d9b070
      for (final s in (stavkeResult.result ?? [])) {
        final nid = s.narudzbaId;
        if (nid == null || !idsNarudzbi.contains(nid)) continue;
 
        (stavkeMap[nid] ??= []).add(s);
        if (s.proizvodId != null) potrebniproizvodIds.add(s.proizvodId!);
      }
<<<<<<< HEAD

=======
 
>>>>>>> b2c5380df42d98abaa1d43c2a6fdfa1a82d9b070
      final jelaResult = await productProvider.get();
      final Map<int, Proizvod> jelaMap = {};
      for (final j in (jelaResult.result ?? [])) {
        final id = j.proizvodId;
        if (id != null && potrebniproizvodIds.contains(id)) {
          jelaMap[id] = j;
        }
      }
<<<<<<< HEAD

=======
 
>>>>>>> b2c5380df42d98abaa1d43c2a6fdfa1a82d9b070
      for (final jid in potrebniproizvodIds) {
        if (!jelaMap.containsKey(jid)) {
          debugPrint(
              "Upozorenje: jeloId=$jid nije pronađen u ProductProvider.get()");
        }
      }
 
      setState(() {
        _narudzbe = narudzbe;
        _stavkePoNarudzbi = stavkeMap;
        _proizvodById = jelaMap;
        _isLoading = false;
      });
    } catch (e) {
      setState(() => _isLoading = false);
      debugPrint("Greška kod dohvata narudžbi: $e");
      _toast('Greška pri dohvatu narudžbi.');
    }
  }
 
  String _naziviJelaZaNarudzbu(int? narudzbaId) {
    if (narudzbaId == null) return "-";
    final stavke = _stavkePoNarudzbi[narudzbaId] ?? [];
    if (stavke.isEmpty) return "(nema stavki)";
 
    final nazivi = <String>[];
    for (final s in stavke) {
      final jid = s.proizvodId;
      final j = (jid != null) ? _proizvodById[jid] : null;
      nazivi.add(j?.nazivProizvoda ?? "Jelo #${jid ?? '?'}");
    }
 
    const maxPrikaza = 3;
    if (nazivi.length > maxPrikaza) {
      return "${nazivi.take(maxPrikaza).join(', ')} +${nazivi.length - maxPrikaza}";
    }
    return nazivi.join(', ');
  }
 
  void _toast(String message) {
    if (!mounted) return;
    ScaffoldMessenger.of(context)
        .showSnackBar(SnackBar(content: Text(message)));
  }
 
  String _formatDatum(DateTime? dt) {
    if (dt == null) return "-";
    return "${dt.day.toString().padLeft(2, '0')}.${dt.month.toString().padLeft(2, '0')}.${dt.year}";
  }
 
  @override
  Widget build(BuildContext context) {
    return WillPopScope(
    onWillPop: () async {
<<<<<<< HEAD
      // Preusmjeri na listu proizvoda
=======
>>>>>>> b2c5380df42d98abaa1d43c2a6fdfa1a82d9b070
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (context) => ProizvodScreen()),
      );
<<<<<<< HEAD
      return false; // sprječava default back
=======
      return false; 
>>>>>>> b2c5380df42d98abaa1d43c2a6fdfa1a82d9b070
    },
    child:  Scaffold(
      appBar: AppBar(title: const Text("Historija mojih narudžbi")),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _narudzbe.isEmpty
              ? const Center(child: Text("Nemate prethodnih narudžbi."))
              : ListView.builder(
                  itemCount: _narudzbe.length,
                  itemBuilder: (ctx, i) {
                    final n = _narudzbe[i];
                    final stavke = _stavkePoNarudzbi[n.narudzbaId] ?? [];
                    final meta = _statusMeta(n);
 
                    return Card(
                      margin: const EdgeInsets.symmetric(
                          vertical: 6, horizontal: 12),
                      child: ExpansionTile(
                        title: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(
                              _naziviJelaZaNarudzbu(n.narudzbaId),
                              style:
                                  const TextStyle(fontWeight: FontWeight.w600),
                            ),
                            const SizedBox(height: 6),
                            Row(
                              mainAxisSize: MainAxisSize.min,
                              children: [
                                Icon(meta.icon, size: 16, color: meta.color),
                                const SizedBox(width: 6),
                                Container(
                                  padding: const EdgeInsets.symmetric(
                                      horizontal: 8, vertical: 4),
                                  decoration: BoxDecoration(
                                    color: meta.color.withOpacity(0.12),
                                    borderRadius: BorderRadius.circular(999),
                                    border: Border.all(
                                        color: meta.color.withOpacity(0.5)),
                                  ),
                                  child: Text(
                                    meta.label,
                                    style: TextStyle(
                                      color: meta.color,
                                      fontWeight: FontWeight.w600,
                                      fontSize: 12,
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ],
                        ),
                        subtitle: Padding(
                          padding: const EdgeInsets.only(top: 6),
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text("Datum: ${_formatDatum(n.datumNarudzbe)}"),
                              Text(
                                  "Korisnik: ${_korisniciById[n.korisnikId] ?? 'Nepoznato ime'}"),
                              const SizedBox(height: 4),
                              Text(
                                "Stavke narudžbi: {${stavke.length}}",
                                style: const TextStyle(color: Colors.black54),
                              ),
                            ],
                          ),
                        ),
                        children: stavke.isEmpty
                            ? const [ListTile(title: Text("Nema stavki."))]
                            : stavke.map((s) {
                                final jid = s.proizvodId;
                                final j = (jid != null) ? _proizvodById[jid] : null;
                                return ListTile(
                                  leading: const Icon(Icons.fastfood_rounded),
                                  title:
                                      Text(j?.nazivProizvoda ?? "Jelo #${jid ?? '?'}"),
                                  subtitle:
                                      Text("Količina: ${s.kolicinaProizvoda ?? 0}"),
                                  trailing: Text(
                                    "${(s.cijena ?? ((s.cijena ?? 0) * (s.kolicinaProizvoda ?? 1))).toStringAsFixed(2)} KM",
                                    style: const TextStyle(
                                        fontWeight: FontWeight.w600),
                                  ),
                                );
                              }).toList(),
                      ),
                    );
                  },
                ),
    ));
  }
}
 
class _StatusMeta {
  final String label;
  final IconData icon;
  final Color color;
  const _StatusMeta(this.label, this.icon, this.color);
}
 
_StatusMeta _statusMeta(Narudzba n) {
  final raw = (n.stateMachine ?? '').toLowerCase();
 
  if (raw.contains('kreiran')) {
    return _StatusMeta('Kreirana', Icons.fiber_new_rounded, Colors.blue);
  }
  if (raw.contains('prihva') || raw.contains('accept')) {
    return _StatusMeta('Prihvaćena', Icons.verified_rounded, Colors.teal);
  }
  if (raw.contains('tok') || raw.contains('progress')) {
    return _StatusMeta(
        'U toku', Icons.pending_actions_rounded, Colors.amber[800]!);
  }
  if (raw.contains('zavr') || raw.contains('done') || raw.contains('isporu')) {
    return _StatusMeta(
        'Završena', Icons.check_circle_rounded, Colors.green[700]!);
  }
  if (raw.contains('otkaz') || raw.contains('cancel')) {
    return _StatusMeta('Otkazana', Icons.cancel_rounded, Colors.red[700]!);
  }
  return _StatusMeta(
    (n.stateMachine ?? 'Nepoznat'),
    Icons.help_outline_rounded,
    Colors.grey[600]!,
  );
}