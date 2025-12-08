import 'dart:convert';
import 'dart:typed_data';

import 'package:esalonljepote_mobile/models/placanje.dart';
import 'package:esalonljepote_mobile/models/proizvod.dart';
import 'package:esalonljepote_mobile/models/search_result.dart';
import 'package:esalonljepote_mobile/providers/cart_provider.dart';
import 'package:esalonljepote_mobile/providers/placanje_provider.dart';
import 'package:esalonljepote_mobile/providers/proizvod_provider.dart';
import 'package:esalonljepote_mobile/screens/historija_narudzbi_screen.dart';
import 'package:esalonljepote_mobile/screens/preporuceni_proizvodi_screen.dart';
import 'package:esalonljepote_mobile/screens/proizvod_screen.dart';
import 'package:esalonljepote_mobile/utils/util.dart';
import 'package:esalonljepote_mobile/widget/master_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_paypal_payment/flutter_paypal_payment.dart';
import 'package:provider/provider.dart';

import '../models/korpa.dart';

class CartScreen extends StatefulWidget {
  @override
  _CartScreenState createState() => _CartScreenState();
}

class _CartScreenState extends State<CartScreen> {
  late CartProvider _cartProvider;
  late ProizvodProvider _proizvodProvider;
  late PlacanjeProvider _placanjeProvider;

  SearchResult<Korpa>? _korpa;
  SearchResult<Placanje>? _placanje;

  bool _isLoading = true;
  DateTime? _selectedDate;
  int? _selectedPlacanjeId;

  String _nacinPlacanja = "gotovina";

  static const String PAYPAL_CLIENT_ID =
      "AUWw046WAjBPE3PeyhJgXy_zaz1GhcZmkZ_lFyvnaz4cL-eqF1WbDamEv-Ax62oj2aSc1mNN-y-IXlpv";
  static const String PAYPAL_SECRET_KEY =
      "EGOsUtD-3EU9EVkQjObk0uDgAl92U1-9tZiOnAslOuJzkP17eJDk_yKI6YtX4k_XItFaWzYCBy17rl-O";
  static const bool PAYPAL_SANDBOX = true;
  static const String PAYPAL_CURRENCY = "EUR";
  static const double BAM_PER_EUR = 1.95583;
  static const double EUR_PER_BAM = 1 / BAM_PER_EUR;

  @override
  void initState() {
    super.initState();
    _cartProvider = context.read<CartProvider>();
    _proizvodProvider = context.read<ProizvodProvider>();
    _placanjeProvider = context.read<PlacanjeProvider>();

    _loadData();
  }

  Future<void> _loadData() async {
    try {
      await _proizvodProvider.fetchAll();

      final data = await _cartProvider.get();
      final placanja = await _placanjeProvider.get();

      for (final item in data.result) {
        item.proizvod ??= _proizvodProvider.items.firstWhere(
          (p) => p.proizvodId == item.proizvodId,
          orElse: () =>
              Proizvod(nazivProizvoda: "Nepoznat proizvod", cijena: 0),
        );
      }
      await _fetchInitialData();

      setState(() {
        _korpa = data;
        _placanje = placanja;
        _isLoading = false;
      });
    } catch (e) {
      debugPrint("Error loading data: $e");
      setState(() => _isLoading = false);
      _toast('Greška pri učitavanju podataka.');
    }
  }

  /*  Future<void> _loadData() async {
    try {
      await _proizvodProvider.fetchAll();
      await _fetchInitialData();
    } catch (e) {
      debugPrint("Error loading data: $e");
      setState(() => _isLoading = false);
      _toast('Greška pri učitavanju podataka.');
    }
  }*/

  Future<void> _fetchInitialData() async {
    try {
      final data = await _cartProvider.get();
      setState(() {
        _korpa = data;
        _isLoading = false;
      });
    } catch (e) {
      debugPrint('Error fetching cart data: $e');
      setState(() => _isLoading = false);
      _toast('Greška pri dohvatu korpe.');
    }
  }

  void _toast(String message) {
    if (!mounted) return;
    ScaffoldMessenger.of(context)
        .showSnackBar(SnackBar(content: Text(message)));
  }

  String _formatDatum(DateTime? dt) {
    if (dt == null) return "Odaberite datum i vrijeme dostave";
    final d = dt.day.toString().padLeft(2, '0');
    final m = dt.month.toString().padLeft(2, '0');
    final y = dt.year.toString();
    final hh = dt.hour.toString().padLeft(2, '0');
    final mm = dt.minute.toString().padLeft(2, '0');
    return "$d.$m.$y $hh:$mm";
  }

  Future<void> _pickDate() async {
    final now = DateTime.now();
    final initial = _selectedDate ?? now.add(const Duration(hours: 1));

    final pickedDate = await showDatePicker(
      context: context,
      initialDate: initial,
      firstDate: now,
      lastDate: now.add(const Duration(days: 60)),
      helpText: 'Izaberite datum isporuke',
    );

    if (pickedDate == null) return;

    final pickedTime = await showTimePicker(
      context: context,
      initialTime: TimeOfDay.fromDateTime(initial),
      helpText: 'Izaberite vrijeme',
    );

    final withTime = pickedTime != null
        ? DateTime(
            pickedDate.year,
            pickedDate.month,
            pickedDate.day,
            pickedTime.hour,
            pickedTime.minute,
          )
        : DateTime(pickedDate.year, pickedDate.month, pickedDate.day, 12, 0);

    setState(() => _selectedDate = withTime);
  }

  double _calcSubtotalBAM() {
    if (_korpa == null) return 0;
    double sum = 0;
    for (final item in _korpa!.result) {
      final qty = (item.kolicinaProizvoda ?? 0);
      final price = (item.cijena ?? 0);
      sum += price * qty;
    }
    return sum;
  }

  String _to2(double v) => v.toStringAsFixed(2);
  String _bamToEurStr(double bam) => _to2(bam * EUR_PER_BAM);

  /* Future<bool> _guardDateSelected() async {
    if (_selectedDate != null) return true;
    await showDialog<void>(
      context: context,
      builder: (ctx) => AlertDialog(
        title: const Text('Nedostaje datum'),
        content: const Text('Molimo odaberite datum isporuke prije plaćanja.'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(ctx),
            child: const Text('U redu'),
          ),
        ],
      ),
    );
    return false;
  }*/
  int _eurCentsFromBAM(double bam) => ((bam * EUR_PER_BAM) * 100).round();
  String _eurStrFromCents(int cents) => _to2(cents / 100.0);

  List<Map<String, dynamic>> _buildPaypalTransactions() {
    final subtotalBAM = _calcSubtotalBAM();
    if (subtotalBAM <= 0) {
      throw Exception('Korpa je prazna ili iznosi nisu postavljeni.');
    }

    int subtotalEurCents = 0;

    final items = _korpa!.result.map((item) {
      final proizvod = _proizvodProvider.items
          .firstWhere((x) => x.proizvodId == item.proizvodId);
      final int qty = (item.kolicinaProizvoda ?? 0);
      final double unitBAM = (item.cijena ?? 0);

      final unitEurCents = _eurCentsFromBAM(unitBAM);
      subtotalEurCents += unitEurCents * qty;

      return {
        "name": proizvod.nazivProizvoda ?? "Nepoznato proizvod",
        "quantity": qty,
        "price": _eurStrFromCents(unitEurCents),
        "currency": PAYPAL_CURRENCY
      };
    }).toList();

    final int shippingEurCents = 0;
    final int discountCents = 0;

    final int totalEurCents =
        subtotalEurCents + shippingEurCents - discountCents;

    final subtotalStr = _eurStrFromCents(subtotalEurCents);
    final shippingStr = _eurStrFromCents(shippingEurCents);
    final totalStr = _eurStrFromCents(totalEurCents);

    return [
      {
        "amount": {
          "total": totalStr,
          "currency": PAYPAL_CURRENCY,
          "details": {
            "subtotal": subtotalStr,
            "shipping": shippingStr,
            "shipping_discount": 0
          }
        },
        "description": "Plaćanje narudžbe u eRestoran aplikaciji",
        "item_list": {
          "items": items,
        }
      }
    ];
  }

  Future<bool> _guardDateSelected() async {
    if (_selectedDate != null) return true;
    await showDialog<void>(
      context: context,
      builder: (ctx) => AlertDialog(
        title: const Text('Nedostaje datum'),
        content: const Text('Molimo odaberite datum isporuke prije plaćanja.'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(ctx),
            child: const Text('U redu'),
          ),
        ],
      ),
    );
    return false;
  }

  Future<void> _startPaypalCheckout() async {
    if (_korpa == null || _korpa!.result.isEmpty) {
      _toast('Korpa je prazna.');
      return;
    }
    if (!await _guardDateSelected()) return;

    final subtotalBAM = _calcSubtotalBAM();
    if (subtotalBAM <= 0) {
      _toast('Ukupno je 0. Dodaj artikle ili postavi cijene.');
      return;
    }

    late final List<Map<String, dynamic>> transactions;
    try {
      transactions = _buildPaypalTransactions();
    } catch (e) {
      _toast(e.toString());
      return;
    }

    Navigator.of(context).push(MaterialPageRoute(
      builder: (BuildContext context) => PaypalCheckoutView(
        sandboxMode: PAYPAL_SANDBOX,
        clientId: PAYPAL_CLIENT_ID,
        secretKey: PAYPAL_SECRET_KEY,
        transactions: transactions,
        note: "Hvala što koristite našu aplikaciju!",
        onSuccess: (Map params) async {
          final paymentId =
              (params['data']?['id'] ?? params['id'] ?? params['paymentId'])
                  ?.toString();

          final totalKolicina = _korpa!.result
              .map((x) => x.kolicinaProizvoda ?? 0)
              .reduce((a, b) => a + b);

          final id = await _cartProvider.checkoutFromCart(
            Authorization.userId!,
            paymentId,
            datumNarudzbe: _selectedDate,
            placanjeId: _selectedPlacanjeId,
          );

          if (!mounted) return;
          _toast('Narudžba #$id kreirana!');
          Navigator.pushReplacement(
            context,
            MaterialPageRoute(builder: (context) => OrdersScreen()),
          );
        },
        onError: (error) {
          debugPrint("PayPal onError: $error");
          _toast('Greška u plaćanju: $error');
          if (mounted) Navigator.pop(context);
        },
        onCancel: () {
          debugPrint('PayPal cancelled');
          _toast('Plaćanje otkazano');
          if (mounted) Navigator.pop(context);
        },
      ),
    ));
  }

  Future<void> _startCashCheckout() async {
    if (_korpa == null || _korpa!.result.isEmpty) {
      _toast('Korpa je prazna.');
      return;
    }
    if (!await _guardDateSelected()) return;

    final confirm = await showDialog<bool>(
      context: context,
      builder: (ctx) => AlertDialog(
        title: const Text('Plaćanje gotovinom'),
        content:
            const Text('Želite li potvrditi narudžbu sa plaćanjem gotovinom?'),
        actions: [
          TextButton(
              onPressed: () => Navigator.pop(ctx, false),
              child: const Text('Odustani')),
          ElevatedButton(
              onPressed: () => Navigator.pop(ctx, true),
              child: const Text('Potvrdi')),
        ],
      ),
    );

    if (confirm != true) return;

    try {
      final id = await _cartProvider.checkoutFromCart(
        Authorization.userId!,
        null,
        datumNarudzbe: _selectedDate,
        placanjeId: _selectedPlacanjeId,
      );

      if (!mounted) return;
      _toast('Narudžba #$id kreirana (gotovina).');
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (context) => OrdersScreen()),
      );
    } catch (e) {
      _toast('Greška pri kreiranju narudžbe: $e');
    }
  }

  @override
  Widget build(BuildContext context) {
    final subtotalBAM = _calcSubtotalBAM();
    final subtotalEUR = _bamToEurStr(subtotalBAM);
    final canCheckout =
        !_isLoading && _korpa != null && _korpa!.result.isNotEmpty;

    return WillPopScope(
      onWillPop: () async => false,
      child: MasterScreenWidget(
        title: "Moja korpa",
        child: _isLoading
            ? const Center(child: CircularProgressIndicator())
            : (_korpa == null || _korpa!.result.isEmpty)
                ? const Center(child: Text("Vaša korpa je prazna"))
                : Column(
                    children: [
                      Padding(
                        padding: const EdgeInsets.all(16),
                        child: GestureDetector(
                          onTap: _pickDate,
                          child: Container(
                            padding: const EdgeInsets.symmetric(
                                vertical: 10, horizontal: 14),
                            decoration: BoxDecoration(
                              color: const Color(0xFFFFF3CD),
                              borderRadius: BorderRadius.circular(12),
                              border:
                                  Border.all(color: const Color(0xFFFFE69C)),
                            ),
                            child: Row(
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                const Icon(Icons.date_range_rounded, size: 18),
                                const SizedBox(width: 8),
                                Expanded(
                                  child: Text(
                                    _formatDatum(_selectedDate),
                                    textAlign: TextAlign.center,
                                    style: const TextStyle(
                                        fontWeight: FontWeight.w600),
                                    maxLines: 1,
                                    overflow: TextOverflow.ellipsis,
                                  ),
                                ),
                                const SizedBox(width: 6),
                                const Icon(Icons.chevron_right_rounded,
                                    size: 18),
                              ],
                            ),
                          ),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.symmetric(
                            horizontal: 16, vertical: 8),
                        child: Container(
                          padding: const EdgeInsets.symmetric(
                              horizontal: 14, vertical: 8),
                          decoration: BoxDecoration(
                            color: const Color(0xFFFFF7E6),
                            borderRadius: BorderRadius.circular(12),
                            border: Border.all(color: Colors.orangeAccent),
                          ),
                          child: Row(
                            children: [
                              const Icon(Icons.payment_rounded,
                                  color: Colors.orange),
                              const SizedBox(width: 10),
                              const Text("Način plaćanja: "),
                              const SizedBox(width: 10),
                              Expanded(
                                child: DropdownButton<int>(
                                  value: _selectedPlacanjeId,
                                  isExpanded: true,
                                  underline: const SizedBox(),
                                  items: _placanje == null
                                      ? []
                                      : _placanje!.result
                                          .map((p) => DropdownMenuItem<int>(
                                                value: p.placanjeId!,
                                                child: Text(p.nacinPlacanja ??
                                                    "Nepoznato plaćanje"),
                                              ))
                                          .toList(),
                                  onChanged: (val) {
                                    final selected = _placanje!.result
                                        .firstWhere((p) => p.placanjeId == val);
                                    setState(() {
                                      _selectedPlacanjeId = val;
                                      _nacinPlacanja =
                                          selected.nacinPlacanja!.toLowerCase();
                                    });
                                  },
                                ),
                              ),
                            ],
                          ),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.symmetric(horizontal: 16),
                        child: _DetailsCard(
                          rows: [
                            _DetailRowData(
                              icon: Icons.timelapse_rounded,
                              title: "Artikala",
                              subtitle: "${_korpa!.result.length}",
                            ),
                            _DetailRowData(
                              icon: Icons.shopping_bag_rounded,
                              title: "Ukupno (KM)",
                              subtitle: subtotalBAM.toStringAsFixed(2),
                            ),
                            _DetailRowData(
                              icon: Icons.payments_rounded,
                              title: "≈ u EUR (PayPal)",
                              subtitle: subtotalEUR,
                            ),
                          ],
                          leftButton: OutlinedButton.icon(
                            onPressed: () => Navigator.of(context).maybePop(),
                            icon:
                                const Icon(Icons.arrow_back_rounded, size: 18),
                            label: const Text("Nazad"),
                          ),
                          rightButton: ElevatedButton.icon(
                            onPressed: canCheckout
                                ? () {
                                    if (_nacinPlacanja.toLowerCase() == "kes") {
                                      _startCashCheckout(); 
                                    } else if (_nacinPlacanja.toLowerCase() ==
                                        "kartica") {
                                      _startPaypalCheckout(); 
                                    } else {
                                      _toast("Nepoznat način plaćanja.");
                                    }
                                  }
                                : null,
                            icon: const Icon(Icons.send_rounded, size: 18),
                            label: Text(
                              _nacinPlacanja.toLowerCase() == "kes"
                                  ? "Pošalji narudžbu"
                                  : "Pošalji zahtjev",
                            ),
                          ),
                        ),
                      ),
                      Expanded(
                        child: ListView.separated(
                          padding: const EdgeInsets.all(16),
                          itemCount: _korpa!.result.length,
                          separatorBuilder: (_, __) =>
                              const SizedBox(height: 8),
                          itemBuilder: (context, index) {
                            final item = _korpa!.result[index];
                            final proizvod = item.proizvod!;
                            final qty = item.kolicinaProizvoda ?? 0;
                            final lineTotal = (proizvod.cijena ?? 0) * qty;

                            return _CartLine(
                              title:
                                  proizvod.nazivProizvoda ?? "Nepoznato jelo",
                              subtitle:
                                  "$qty x ${proizvod.cijena?.toStringAsFixed(2) ?? '0'} KM",
                              trailing: "${lineTotal.toStringAsFixed(2)} KM",
                              imageBytes: (proizvod.slika != null &&
                                      proizvod.slika!.isNotEmpty)
                                  ? base64Decode(proizvod.slika!)
                                  : null,
                              onDelete: () async {
                                await _cartProvider.delete(item.korpaId);
                                await _loadData();
                              },
                            );
                          },
                        ),
                      ),
                    ],
                  ),
      ),
    );
  }
}

class _DateChip extends StatelessWidget {
  final String label;
  final VoidCallback? onTap;
  const _DateChip({required this.label, this.onTap});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onTap,
      child: Container(
        padding: const EdgeInsets.symmetric(horizontal: 14, vertical: 10),
        decoration: BoxDecoration(
          color: const Color(0xFFFFF3CD),
          borderRadius: BorderRadius.circular(12),
          border: Border.all(color: const Color(0xFFFFE69C)),
        ),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Icon(Icons.date_range_rounded, size: 18),
            const SizedBox(width: 8),
            Expanded(
              child: Text(
                label,
                textAlign: TextAlign.center,
                style: const TextStyle(fontWeight: FontWeight.w600),
                maxLines: 1,
                overflow: TextOverflow.ellipsis,
              ),
            ),
            const SizedBox(width: 6),
            const Icon(Icons.chevron_right_rounded, size: 18),
          ],
        ),
      ),
    );
  }
}

class _DetailRowData {
  final IconData icon;
  final String title;
  final String subtitle;
  _DetailRowData({
    required this.icon,
    required this.title,
    required this.subtitle,
  });
}

class _DetailsCard extends StatelessWidget {
  final List<_DetailRowData> rows;
  final Widget leftButton;
  final Widget rightButton;

  const _DetailsCard({
    required this.rows,
    required this.leftButton,
    required this.rightButton,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: const Color(0xFFFFF7E6),
        borderRadius: BorderRadius.circular(18),
        boxShadow: const [BoxShadow(color: Colors.black12, blurRadius: 6)],
      ),
      child: Padding(
        padding: const EdgeInsets.fromLTRB(14, 12, 14, 12),
        child: Column(
          children: [
            for (int i = 0; i < rows.length; i++) ...[
              ListTile(
                leading: Icon(rows[i].icon, size: 22),
                title: Text(
                  rows[i].title,
                  style: const TextStyle(fontSize: 13, color: Colors.black54),
                ),
                subtitle: Text(
                  rows[i].subtitle,
                  style: const TextStyle(
                      fontSize: 15, fontWeight: FontWeight.w600),
                ),
                dense: true,
                contentPadding: const EdgeInsets.symmetric(horizontal: 4),
              ),
              if (i != rows.length - 1) const Divider(height: 0, thickness: .6),
            ],
            const SizedBox(height: 10),
            Row(
              children: [
                Expanded(child: leftButton),
                const SizedBox(width: 10),
                Expanded(child: rightButton),
              ],
            ),
          ],
        ),
      ),
    );
  }
}

class _CartLine extends StatelessWidget {
  final String title;
  final String subtitle;
  final String trailing;
  final Uint8List? imageBytes;
  final VoidCallback onDelete;

  const _CartLine({
    required this.title,
    required this.subtitle,
    required this.trailing,
    required this.onDelete,
    this.imageBytes,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(14),
        boxShadow: const [BoxShadow(color: Colors.black12, blurRadius: 4)],
      ),
      child: ListTile(
        leading: ClipRRect(
          borderRadius: BorderRadius.circular(8),
          child: imageBytes != null
              ? Image.memory(
                  imageBytes!,
                  width: 56,
                  height: 56,
                  fit: BoxFit.cover,
                )
              : Container(
                  width: 56,
                  height: 56,
                  color: Colors.grey[300],
                  child: const Icon(Icons.image_not_supported_rounded),
                ),
        ),
        title: Text(
          title,
          maxLines: 1,
          overflow: TextOverflow.ellipsis,
          style: const TextStyle(fontWeight: FontWeight.w600),
        ),
        subtitle: Text(subtitle),
        trailing: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
              trailing,
              style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 13),
            ),
            const SizedBox(height: 6),
            GestureDetector(
              onTap: onDelete,
              child:
                  const Icon(Icons.delete_outline_rounded, color: Colors.red),
            ),
          ],
        ),
      ),
    );
  }
}

class _RecommendationBanner extends StatelessWidget {
  final String text;
  final String actionText;
  final VoidCallback onPressed;

  const _RecommendationBanner({
    required this.text,
    required this.actionText,
    required this.onPressed,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.fromLTRB(14, 12, 14, 12),
      decoration: BoxDecoration(
        color: const Color(0xFFFFEDE0),
        borderRadius: BorderRadius.circular(16),
        border: Border.all(color: const Color(0xFFFFD7BF)),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(children: [
            const Icon(Icons.local_fire_department_rounded, size: 18),
            const SizedBox(width: 8),
            Expanded(
              child: Text(
                text,
                style: const TextStyle(fontWeight: FontWeight.w600),
              ),
            ),
          ]),
          const SizedBox(height: 10),
          SizedBox(
            width: double.infinity,
            child: ElevatedButton(
              onPressed: onPressed,
              style: ElevatedButton.styleFrom(
                shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(14)),
                padding: const EdgeInsets.symmetric(vertical: 12),
              ),
              child: Text(actionText),
            ),
          )
        ],
      ),
    );
  }
}
