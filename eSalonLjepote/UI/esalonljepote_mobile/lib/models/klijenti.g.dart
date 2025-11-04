// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'klijenti.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Klijenti _$KlijentiFromJson(Map<String, dynamic> json) => Klijenti(
      klijentId: (json['klijentId'] as num?)?.toInt(),
      korisnikId: (json['korisnikId'] as num?)?.toInt(),
      korisnik: json['korisnik'] == null
          ? null
          : Korisnik.fromJson(json['korisnik'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$KlijentiToJson(Klijenti instance) => <String, dynamic>{
      'klijentId': instance.klijentId,
      'korisnikId': instance.korisnikId,
      'korisnik': instance.korisnik,
    };
