import 'dart:convert';
import 'package:esalonljepote_mobile/models/klijenti.dart';
import 'package:esalonljepote_mobile/models/narudzba.dart';
import 'package:esalonljepote_mobile/models/proizvod.dart';
import 'package:esalonljepote_mobile/models/search_result.dart';
import 'package:esalonljepote_mobile/models/termini.dart';
import 'package:esalonljepote_mobile/utils/util.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:http/http.dart';
 
abstract class BaseProvider<T> with ChangeNotifier {
  static String? _baseUrl;
  final String _endpoint;
  late final String totalUrl;
 
  BaseProvider(String endpoint) : _endpoint = endpoint {
    _baseUrl = const String.fromEnvironment("baseUrl",
        defaultValue: "http://10.0.2.2:7071/");
    totalUrl = "$_baseUrl$_endpoint";
  }
//https://localhost:44314/
//http://10.0.2.2:7074/
//http://host.docker.internal:7071/
//http://192.168.2.77:7071/
  T fromJson(dynamic data);
 
  Future<SearchResult<T>> get({dynamic filter}) async {
    var url = "$_baseUrl$_endpoint";
 
    if (filter != null) {
      final queryString = getQueryString(filter);
      url = "$url?$queryString";
    }
 
    final uri = Uri.parse(url);
    final headers = createHeaders();
 
    try {
      final response = await http.get(uri, headers: headers);
 
      if (isValidResponse(response)) {
        final data = jsonDecode(response.body);
 
        final result = SearchResult<T>();
 
        if (data is List) {
          for (dynamic item in data) {
            result.result.add(fromJson(item));
          }
          result.count = result.result.length;
        } else if (data is Map && data.containsKey('result')) {
          result.count = data['count'] ?? 0;
          for (dynamic item in data['result']) {
            result.result.add(fromJson(item));
          }
        }
 
        return result;
      } else {
        throw Exception("Unknown error");
      }
    } catch (e) {
      debugPrint("Error during GET request: $e");
      rethrow;
    }
  }
 
  Future<T> update(int id, [dynamic request]) async {
    final uri = Uri.parse("$_baseUrl$_endpoint/$id");
    final headers = createHeaders();
 
    try {
      final jsonRequest = jsonEncode(request);
      final response = await http.put(uri, headers: headers, body: jsonRequest);
 
      if (isValidResponse(response)) {
        final data = jsonDecode(response.body);
        return fromJson(data);
      } else {
        throw Exception("Unknown error");
      }
    } catch (e) {
      debugPrint("Error during PUT request: $e");
      rethrow;
    }
  }
 
  Future<T> insert(dynamic request) async {
    final uri = Uri.parse("$_baseUrl$_endpoint");
    final headers = createHeaders();
 
    try {
      final jsonRequest = jsonEncode(request);
      debugPrint("POST $uri\nHeaders: $headers\nBody: $jsonRequest");
 
      final response =
          await http.post(uri, headers: headers, body: jsonRequest);
 
      debugPrint("Response: ${response.statusCode} ${response.body}");
 
      if (isValidResponse(response)) {
        final data = jsonDecode(response.body);
        return fromJson(data);
      } else {
        throw Exception("Error: ${response.statusCode} ${response.body}");
      }
    } catch (e) {
      debugPrint("Error during POST request: $e");
      rethrow;
    }
  }
 
  Future<T> delete(int? id) async {
    final uri = Uri.parse("$_baseUrl$_endpoint/$id");
    final headers = createHeaders();
 
    final response = await http.delete(uri, headers: headers);
 
    if (isValidResponse(response)) {
      final data = jsonDecode(response.body);
      notifyListeners();
      return fromJson(data);
    } else {
      throw Exception("Unknown error");
    }
  }
 
  String getQueryString(Map params,
      {String prefix = '&', bool inRecursion = false}) {
    String query = '';
    params.forEach((key, value) {
      var newKey = key;
      if (inRecursion) {
        newKey = (key is int) ? '[$key]' : '.$key';
      }
      if (value is String || value is int || value is double || value is bool) {
        query += '$prefix$newKey=${Uri.encodeComponent(value.toString())}';
      } else if (value is DateTime) {
        query += '$prefix$newKey=${value.toIso8601String()}';
      } else if (value is List || value is Map) {
        final mapped = (value is List) ? value.asMap() : value;
        mapped.forEach((k, v) {
          query += getQueryString({k: v},
              prefix: '$prefix$newKey', inRecursion: true);
        });
      }
    });
    return query;
  }
 
  bool isValidResponse(Response response) {
    if (response.statusCode < 300) {
      return true;
    } else if (response.statusCode == 401) {
      throw Exception("Unauthorized");
    } else {
      debugPrint(response.body);
      throw Exception("Something bad happened, please try again");
    }
  }
 
  Map<String, String> createHeaders() {
    final username = Authorization.username ?? "";
    final password = Authorization.password ?? "";
    final basicAuth =
        "Basic ${base64Encode(utf8.encode('$username:$password'))}";
 
    return {
      "Content-Type": "application/json",
      "Authorization": basicAuth,
    };
  }
 
  Future<T> getById(int? id) async {
    final uri = Uri.parse("$_baseUrl$_endpoint/$id");
    final headers = createHeaders();
 
    final response = await http.get(uri, headers: headers);
    if (isValidResponse(response)) {
      final data = jsonDecode(response.body);
      return fromJson(data);
    } else {
      throw Exception("Unknown error");
    }
  }
 
  Future<List<Proizvod>> fetchRecommendedProizvodi() async {
    try {
      final uri = Uri.parse('$totalUrl/preporuceni');
      final response = await http.get(uri, headers: createHeaders());
 
      if (isValidResponse(response)) {
        return (jsonDecode(response.body) as List)
            .map((item) => Proizvod.fromJson(item))
            .toList();
      } else {
        throw Exception('Invalid response: ${response.body}');
      }
    } catch (e) {
      debugPrint('Error fetching recommended proizvodi: $e');
      rethrow;
    }
  }
 
  Future<List<Narudzba>> getIzvjestajHistorijeNarudzbi(
      {Map<String, dynamic>? filter}) async {
    var url = "$_baseUrl$_endpoint/Izvjestaj";
 
    if (filter != null) {
      final queryString = getQueryString(filter);
      url = "$url?$queryString";
    }
 
    final uri = Uri.parse(url);
    final headers = createHeaders();
 
    try {
      final response = await http.get(uri, headers: headers);
 
      if (isValidResponse(response)) {
        final data = jsonDecode(response.body);
        final lista = <Narudzba>[];
 
        final resultList = data['result'] as List<dynamic>;
        for (dynamic item in resultList) {
          lista.add(fromJson(item) as Narudzba);
        }
 
        return lista;
      } else {
        throw Exception("Greška pri dohvatu izvještaja");
      }
    } catch (e) {
      debugPrint("Greška: $e");
      rethrow;
    }
  }
 
  /// Checkout iz korpe
  /* Future<int> checkoutFromCart(
    int userId,
    String? paymentId, {
    int? proizvodId,
    DateTime? datumNarudzbe,
  }) async {
    final uri = Uri.parse('${_baseUrl}Narudzba/checkoutFromCart');
    final headers = createHeaders();
 
    final bodyMap = <String, dynamic>{
      "korisnikId": userId,
      "paymentId": paymentId,
      if (proizvodId != null) "proizvodId": proizvodId,
      if (datumNarudzbe != null)
        "datumNarudzbe": datumNarudzbe.toIso8601String(),
    };
 
    final response = await http.post(
      uri,
      headers: headers,
      body: jsonEncode(bodyMap),
    );
 
    debugPrint('checkoutFromCart ${response.statusCode}: ${response.body}');
 
    if (response.statusCode >= 200 && response.statusCode < 300) {
      final data = jsonDecode(response.body);
      return data is int ? data : int.parse(data.toString());
    } else {
      throw Exception(
          'Checkout failed: ${response.statusCode} ${response.body}');
    }
  }*/
 
  /* Future<int> checkoutFromCart(int korisnikId, String? paymentId,
    {DateTime? datumNarudzbe, int? placanjeId}) async {
  final request = {
    "korisnikId": korisnikId,
    "paypalPaymentId": paymentId,
    "datumNarudzbe": datumNarudzbe?.toIso8601String(),
    "placanjeId": placanjeId
  };
 
  final response = await http.post(
    Uri.parse("$_baseUrl/Korpa/Checkout"),
    headers: createHeaders(),
    body: jsonEncode(request),
  );
 
  if (response.statusCode == 200) {
    return int.parse(response.body);
  } else {
    throw Exception("Greška pri kreiranju narudžbe: ${response.body}");
  }
}*/
 
  /*Future<int> checkoutFromCart(
    int korisnikId,
    String? paypalPaymentId, {
    DateTime? datumNarudzbe,
    int? placanjeId,
  }) async {
    final url = Uri.parse('${_baseUrl}/Korpa/Checkout');
    final headers = createHeaders();
 
    final body = jsonEncode({
      "korisnikId": korisnikId,
      "paypalPaymentId": paypalPaymentId,
      "datumNarudzbe": datumNarudzbe?.toIso8601String(),
      "placanjeId": placanjeId ?? (paypalPaymentId == null ? 1 : 2)
    });
 
    final response = await http.post(url, headers: headers, body: body);
 
    if (response.statusCode == 200) {
      final decoded = jsonDecode(response.body);
      if (decoded is int) return decoded;
      if (decoded is Map && decoded.containsKey('narudzbaId')) {
        return decoded['narudzbaId'];
      }
      throw Exception("Neočekivan odgovor sa servera");
    } else {
      throw Exception(
          "Greška ${response.statusCode}: ${response.body}");
    }
  }*/
 
  Future<int> checkoutFromCart(
    int userId,
    String? paymentId, {
    int? statusId,
    DateTime? datumNarudzbe,
    int? placanjeId,
  }) async {
    final uri = Uri.parse('${_baseUrl}Narudzba/checkoutFromCart');
    final headers = createHeaders();
 
    final bodyMap = <String, dynamic>{
      "korisnikId": userId,
      "paymentId": paymentId,
      if (statusId != null) "statusId": statusId,
      if (datumNarudzbe != null)
        "datumNarudzbe": datumNarudzbe.toIso8601String(),
      "placanjeId":placanjeId,
    };
 
    final resp = await http.post(
      uri,
      headers: headers,
      body: jsonEncode(bodyMap),
    );
 
    debugPrint('checkoutFromCart ${resp.statusCode}: ${resp.body}');
 
    if (resp.statusCode >= 200 && resp.statusCode < 300) {
      final data = jsonDecode(resp.body);
      return data is int ? data : int.parse(data.toString());
    } else {
      throw Exception('Checkout failed: ${resp.statusCode} ${resp.body}');
    }
  }
 
  Future<SearchResult<T>> getTermini() async {
    var result = await get();
    return result;
  }
}
 