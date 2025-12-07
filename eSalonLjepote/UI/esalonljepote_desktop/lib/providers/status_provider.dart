import 'dart:convert';

import 'package:esalonljepote_desktop/models/status.dart';
import 'package:esalonljepote_desktop/providers/base_provider.dart';
import 'package:http/http.dart' as http;

class StatusProvider extends BaseProvider<Status> {
  StatusProvider() : super("Status");

  @override
  Status fromJson(data) {
    return Status.fromJson(data);
  }
}
