import 'dart:convert';

import 'package:esalonljepote_mobile/models/proizvod.dart';
import 'package:json_annotation/json_annotation.dart';

part 'korpa.g.dart';

@JsonSerializable()
class Korpa {
  int? korpaId;
  int? korisnikId;
  int? proizvodId;
  int? kolicinaProizvoda;
  double? cijena;
  Proizvod? proizvod;

  Korpa(this.korpaId, this.korisnikId, this.proizvodId, this.kolicinaProizvoda,
      this.cijena,this.proizvod, );

  factory Korpa.fromJson(Map<String, dynamic> json) => _$KorpaFromJson(json);

  Map<String, dynamic> toJson() => _$KorpaToJson(this);
}
