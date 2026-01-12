import 'dart:convert';

import 'package:esalonljepote_mobile/models/narudzba.dart';
import 'package:esalonljepote_mobile/models/search_result.dart';
import 'package:http/http.dart' as http;
import 'package:provider/provider.dart';
import 'package:esalonljepote_mobile/providers/base_provider.dart';

import 'package:flutter/material.dart';

class NarudzbaProvider extends BaseProvider<Narudzba> {
  NarudzbaProvider() : super("Narudzba");

  @override
  Narudzba fromJson(data) {
    return Narudzba.fromJson(data);
  }

  Future<SearchResult<Narudzba>> getByUser(int userId) async {
    return await get(filter: {"korisnikId": userId});
  }

  
}
