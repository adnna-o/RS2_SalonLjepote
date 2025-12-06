import 'dart:convert';

import 'package:esalonljepote_mobile/models/status.dart';
import 'package:esalonljepote_mobile/models/termini.dart';
import 'package:esalonljepote_mobile/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class StatusProvider extends BaseProvider<Status> {
  StatusProvider() : super("Status");

  @override
  Status fromJson(data) {
    return Status.fromJson(data);
  }
}
