import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:esalonljepote_desktop/models/narudzba.dart';
import 'package:esalonljepote_desktop/models/status.dart';
import 'package:esalonljepote_desktop/models/korisnik.dart';
import 'package:esalonljepote_desktop/models/search_result.dart';
import 'package:esalonljepote_desktop/providers/narudzba_provider.dart';
import 'package:esalonljepote_desktop/providers/status_provider.dart';
import 'package:esalonljepote_desktop/providers/korisnik_provider.dart';
import 'package:intl/intl.dart';

class StatusNarudzbaScreen extends StatefulWidget {
  final int narudzbaId;
  StatusNarudzbaScreen({Key? key, required this.narudzbaId}) : super(key: key);

  @override
  State<StatusNarudzbaScreen> createState() => _StatusNarudzbaScreenState();
}

class _StatusNarudzbaScreenState extends State<StatusNarudzbaScreen> {
  late StatusProvider _statusProvider;
  late NarudzbaProvider _narudzbaProvider;
  late KorisnikProvider _korisnikProvider;

  SearchResult<Status>? statusResult;
  Narudzba? narudzba;
  Korisnik? korisnik;

  int? _selectedStatusId;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _statusProvider = context.read<StatusProvider>();
    _narudzbaProvider = context.read<NarudzbaProvider>();
    _korisnikProvider = context.read<KorisnikProvider>();

    _fetchData();
  }

  Future<void> _fetchData() async {
    try {
      // Učitaj sve statuse
      var statuses = await _statusProvider.get();

      // Dohvati narudžbu po ID
      var narudzbaResult = await _narudzbaProvider.get(filter: {
        'narudzbaId': widget.narudzbaId,
      });

      if (narudzbaResult.result.isNotEmpty) {
        narudzba = narudzbaResult.result.first;
        _selectedStatusId = narudzba!.statusNarudzbeId;

        // Dohvati korisnika narudžbe
        var korisnikResultData = await _korisnikProvider.get();
        korisnik = korisnikResultData.result.firstWhere(
          (k) => k.korisnikId == narudzba!.korisnikId,
          orElse: () => Korisnik(korisnikId: 0, ime: 'Nepoznat', prezime: ''),
        );
      }

      setState(() {
        statusResult = statuses;
      });
    } catch (e) {
      print("Greška pri učitavanju podataka: $e");
    }
  }

  Future<void> _updateStatus() async {
    if (_selectedStatusId == null || narudzba == null) return;

    try {
      // Kreiramo kompletan payload kakav backend očekuje
      final updateData = {
        "narudzbaId": narudzba!.narudzbaId,
        "korisnikId": narudzba!.korisnikId,
        "datumNarudzbe": narudzba!.datumNarudzbe?.toIso8601String(),
        "statusNarudzbeId": _selectedStatusId,
        "iznosNarudzbe": narudzba!.iznosNarudzbe,
      };

      await _narudzbaProvider.update(
        narudzba!.narudzbaId!,
        updateData,
      );

      setState(() {
        narudzba!.statusNarudzbeId = _selectedStatusId;
      });

      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text("Status narudžbe je ažuriran.")),
      );
    } catch (e) {
      print("PUT ERROR: $e");
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text("Greška pri ažuriranju statusa: $e")),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    if (statusResult == null || narudzba == null || korisnik == null) {
      return Scaffold(
        appBar: AppBar(title: Text("Status narudžbe")),
        body: Center(child: CircularProgressIndicator()),
      );
    }

    return Scaffold(
      appBar: AppBar(title: Text("Status narudžbe")),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Card(
          elevation: 4,
          shape:
              RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
          child: Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  "Korisnik: ${korisnik!.ime} ${korisnik!.prezime}",
                  style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                ),
                SizedBox(height: 8),
                Text(
                  "Datum narudžbe: ${narudzba!.datumNarudzbe != null ? DateFormat('dd.MM.yyyy').format(narudzba!.datumNarudzbe!) : 'Nepoznat'}",
                  style: TextStyle(fontSize: 16),
                ),
                SizedBox(height: 16),
                Text(
                  "Trenutni status narudžbe:",
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                ),
                SizedBox(height: 8),
                Expanded(
                  child: ListView.builder(
                    itemCount: statusResult!.result.length,
                    itemBuilder: (context, index) {
                      var status = statusResult!.result[index];
                      bool isCurrent =
                          status.statusNarudzbeId == narudzba!.statusNarudzbeId;
                      return Card(
                        color: isCurrent
                            ? Colors.green.shade200
                            : Colors.grey.shade200,
                        child: ListTile(
                          title: Text(status.naziv ?? "Nepoznat status"),
                          trailing: isCurrent
                              ? Icon(Icons.check_circle, color: Colors.green)
                              : null,
                        ),
                      );
                    },
                  ),
                ),
                SizedBox(height: 16),
                Text(
                  "Promijeni status narudžbe:",
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                ),
                DropdownButton<int>(
                  value: _selectedStatusId,
                  isExpanded: true,
                  items: statusResult!.result.map((status) {
                    return DropdownMenuItem<int>(
                      value: status.statusNarudzbeId,
                      child: Text(status.naziv ?? "Nepoznat status"),
                    );
                  }).toList(),
                  onChanged: (val) {
                    setState(() {
                      _selectedStatusId = val;
                    });
                  },
                ),
                SizedBox(height: 16),
                ElevatedButton(
                  onPressed: _selectedStatusId != null &&
                          _selectedStatusId != narudzba!.statusNarudzbeId
                      ? _updateStatus
                      : null,
                  child: Text("Sačuvaj promjenu"),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
