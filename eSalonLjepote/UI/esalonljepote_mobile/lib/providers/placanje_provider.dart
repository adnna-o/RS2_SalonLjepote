import 'dart:convert';

import 'package:esalonljepote_mobile/models/novosti.dart';
import 'package:esalonljepote_mobile/models/placanje.dart';
import 'package:esalonljepote_mobile/models/recenzije.dart';
import 'package:esalonljepote_mobile/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class PlacanjeProvider extends BaseProvider<Placanje> {
  PlacanjeProvider() : super("Placanje");

  @override
  Placanje fromJson(data) {
    return Placanje.fromJson(data);
  }
}
