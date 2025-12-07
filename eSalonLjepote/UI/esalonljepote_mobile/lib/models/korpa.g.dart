// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'korpa.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Korpa _$KorpaFromJson(Map<String, dynamic> json) => Korpa(
      (json['korpaId'] as num?)?.toInt(),
      (json['korisnikId'] as num?)?.toInt(),
      (json['proizvodId'] as num?)?.toInt(),
      (json['kolicinaProizvoda'] as num?)?.toInt(),
      (json['cijena'] as num?)?.toDouble(),
      json['proizvod'] == null
          ? null
          : Proizvod.fromJson(json['proizvod'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$KorpaToJson(Korpa instance) => <String, dynamic>{
      'korpaId': instance.korpaId,
      'korisnikId': instance.korisnikId,
      'proizvodId': instance.proizvodId,
      'kolicinaProizvoda': instance.kolicinaProizvoda,
      'cijena': instance.cijena,
      'proizvod': instance.proizvod,
    };
