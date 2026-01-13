import 'dart:convert';
import 'dart:ui';

import 'package:esalonljepote_mobile/main.dart';
import 'package:esalonljepote_mobile/models/korisnik.dart';
import 'package:esalonljepote_mobile/providers/korisnik_provider.dart';
import 'package:esalonljepote_mobile/providers/uloga_provider.dart';
import 'package:esalonljepote_mobile/utils/util.dart';
import 'package:esalonljepote_mobile/widget/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

class KorisnikProfileScreen extends StatefulWidget {
  const KorisnikProfileScreen({Key? key}) : super(key: key);

  @override
  State<KorisnikProfileScreen> createState() => _KorisnikProfileScreen();
}

class _KorisnikProfileScreen extends State<KorisnikProfileScreen> {
  late KorisnikProvider _korisnikProvider;
  late UlogaProvider _ulogaProvider;
  List<Korisnik>? korisnikResult;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _korisnikProvider = context.read<KorisnikProvider>();
    _ulogaProvider = context.read<UlogaProvider>();
    _fetchKorisnici();
  }

  Future<void> _fetchKorisnici() async {
    try {
      var data = await _korisnikProvider.get();
      setState(() {
        korisnikResult = data.result.where((korisnik) {
          return korisnik.korisnikUlogas.any((uloga) => uloga.ulogaId == 2);
        }).toList();
      });
    } catch (e) {
      print('Error fetching korisnici: $e');
    }
  }

  ImageProvider? getProfileImage(String? slika) {
    if (slika == null || slika.trim().isEmpty) {
      return null;
    }

    if (slika.startsWith('http')) {
      return NetworkImage(slika);
    }

    try {
      final cleanBase64 = slika.contains(',') ? slika.split(',').last : slika;
      return MemoryImage(base64Decode(cleanBase64));
    } catch (e) {
      debugPrint("Neispravna slika: $e");
      return null;
    }
  }

  @override
  Widget build(BuildContext context) {
    final korisnik = korisnikResult?.first;

    return MasterScreenWidget(
      child: Stack(
        children: [
          Container(
            decoration: const BoxDecoration(
              image: DecorationImage(
                image: AssetImage("assets/images/Background.jpg"),
                fit: BoxFit.cover,
              ),
            ),
          ),
          BackdropFilter(
            filter: ImageFilter.blur(sigmaX: 5, sigmaY: 5),
            child: Container(
              color: Colors.black.withOpacity(0.3),
            ),
          ),
          Center(
            child: korisnik != null
                ? SingleChildScrollView(
                    padding: const EdgeInsets.all(20),
                    child: Container(
                      constraints: const BoxConstraints(maxWidth: 400),
                      decoration: BoxDecoration(
                        color: Colors.white.withOpacity(0.85),
                        borderRadius: BorderRadius.circular(24),
                        boxShadow: [
                          BoxShadow(
                            color: Colors.black.withOpacity(0.2),
                            blurRadius: 20,
                            offset: const Offset(0, 10),
                          ),
                        ],
                      ),
                      padding: const EdgeInsets.all(24),
                      child: Column(
                        children: [
                          CircleAvatar(
                            radius: 60,
                            backgroundColor: Colors.pink.shade200,
                            backgroundImage: getProfileImage(korisnik.slika),
                            child: getProfileImage(korisnik.slika) == null
                                ? const Icon(Icons.person,
                                    size: 60, color: Colors.white)
                                : null,
                          ),
                          const SizedBox(height: 16),
                          Text(
                            "${korisnik.ime ?? ''} ${korisnik.prezime ?? ''}",
                            style: const TextStyle(
                              fontSize: 24,
                              fontWeight: FontWeight.bold,
                              color: Colors.black87,
                            ),
                          ),
                          const SizedBox(height: 6),
                          Text(
                            korisnik.email ?? '',
                            style: TextStyle(
                              fontSize: 16,
                              color: Colors.grey[700],
                            ),
                          ),
                          const SizedBox(height: 20),
                          const Divider(),
                          const SizedBox(height: 12),
                          _buildProfileDetail("Korisničko ime",
                              korisnik.korisnickoIme ?? '', Icons.person),
                          _buildProfileDetail(
                              "Datum rođenja",
                              (korisnik.datumRodjenja != null &&
                                      korisnik.datumRodjenja!.isNotEmpty)
                                  ? DateFormat('dd.MM.yyyy').format(
                                      DateTime.parse(korisnik.datumRodjenja!))
                                  : 'N/A',
                              Icons.cake_outlined),
                          _buildProfileDetail("Telefon",
                              korisnik.telefon ?? 'N/A', Icons.phone),
                          _buildProfileDetail(
                              "Spol", korisnik.spol ?? 'N/A', Icons.wc),
                          const SizedBox(height: 24),
                          SizedBox(
                            width: double.infinity,
                            child: ElevatedButton.icon(
                              icon: const Icon(Icons.logout),
                              style: ElevatedButton.styleFrom(
                                backgroundColor: Colors.pink.shade400,
                                padding: const EdgeInsets.symmetric(
                                    vertical: 14, horizontal: 16),
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(12),
                                ),
                              ),
                              onPressed: _logout,
                              label: const Text(
                                "Odjavi se",
                                style: TextStyle(
                                  fontSize: 16,
                                  fontWeight: FontWeight.bold,
                                  color: Colors.white,
                                ),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  )
                : const Center(child: CircularProgressIndicator()),
          ),
        ],
      ),
    );
  }

  void _logout() {
    Authorization.korisnik = null;
    Navigator.of(context).pushAndRemoveUntil(
      MaterialPageRoute(builder: (context) => LoginPage()),
      (route) => false,
    );
  }

  Widget _buildProfileDetail(String title, String value, IconData icon) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: Container(
        decoration: BoxDecoration(
          color: Colors.grey[200],
          borderRadius: BorderRadius.circular(10.0),
          boxShadow: [
            BoxShadow(
              color: Colors.black12,
              blurRadius: 8.0,
              offset: Offset(0, 4),
            ),
          ],
        ),
        padding: EdgeInsets.all(12.0),
        child: LayoutBuilder(
          builder: (context, constraints) {
            return Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Icon(icon, color: Colors.blueAccent, size: 28),
                SizedBox(width: 16),
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        "$title:",
                        style: TextStyle(
                          fontWeight: FontWeight.bold,
                          fontSize: 18,
                        ),
                      ),
                      SizedBox(height: 4),
                      Text(
                        value,
                        style: TextStyle(fontSize: 16),
                        softWrap: true,
                      ),
                    ],
                  ),
                ),
              ],
            );
          },
        ),
      ),
    );
  }

  Widget _buildProfilePicture(String? profilnaSlika) {
    return CircleAvatar(
      radius: 60,
      backgroundColor: Colors.blueAccent,
      backgroundImage: profilnaSlika != null
          ? (profilnaSlika.startsWith('http')
              ? NetworkImage(profilnaSlika)
              : MemoryImage(base64Decode(profilnaSlika))) as ImageProvider
          : null,
      child: profilnaSlika == null
          ? Text(
              "N/A",
              style: TextStyle(
                color: Colors.white,
                fontSize: 48,
              ),
            )
          : null,
    );
  }
}
