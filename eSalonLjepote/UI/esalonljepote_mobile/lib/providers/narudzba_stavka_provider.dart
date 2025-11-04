import 'dart:convert';

import 'package:esalonljepote_mobile/models/narudzbastavka.dart';
import 'package:http/http.dart' as http;
import 'package:provider/provider.dart';
import 'package:esalonljepote_mobile/providers/base_provider.dart';

import 'package:flutter/material.dart';
class NarudzbaStavkaProvider extends BaseProvider<NarudzbaStavka> {
  NarudzbaStavkaProvider() : super("NarudzbaStavka");

  @override
  NarudzbaStavka fromJson(data) {
    return NarudzbaStavka.fromJson(data);
  }


}
