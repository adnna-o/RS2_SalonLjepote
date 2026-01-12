// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'narudzba.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Narudzba _$NarudzbaFromJson(Map<String, dynamic> json) => Narudzba(
      narudzbaId: (json['narudzbaId'] as num?)?.toInt(),
      korisnikId: (json['korisnikId'] as num?)?.toInt(),
      proizvodId: (json['proizvodId'] as num?)?.toInt(),
      placanjeId: (json['placanjeId'] as num?)?.toInt(),
      datumNarudzbe: json['datumNarudzbe'] == null
          ? null
          : DateTime.parse(json['datumNarudzbe'] as String),
      kolicinaProizvoda: (json['kolicinaProizvoda'] as num?)?.toInt(),
      iznosNarudzbe: (json['iznosNarudzbe'] as num?)?.toDouble(),
      korisnik: json['korisnik'] == null
          ? null
          : Korisnik.fromJson(json['korisnik'] as Map<String, dynamic>),
      proizvod: json['proizvod'] == null
          ? null
          : Proizvod.fromJson(json['proizvod'] as Map<String, dynamic>),
      paymentId: json['paymentId'] as String?,
      stateMachine: json['stateMachine'] as String?,
      statusNarudzbeId: (json['statusNarudzbeId'] as num?)?.toInt(),
    );

Map<String, dynamic> _$NarudzbaToJson(Narudzba instance) => <String, dynamic>{
      'narudzbaId': instance.narudzbaId,
      'korisnikId': instance.korisnikId,
      'proizvodId': instance.proizvodId,
      'placanjeId': instance.placanjeId,
      'datumNarudzbe': instance.datumNarudzbe?.toIso8601String(),
      'kolicinaProizvoda': instance.kolicinaProizvoda,
      'iznosNarudzbe': instance.iznosNarudzbe,
      'paymentId': instance.paymentId,
      'stateMachine': instance.stateMachine,
      'statusNarudzbeId': instance.statusNarudzbeId,
      'korisnik': instance.korisnik,
      'proizvod': instance.proizvod,
    };
