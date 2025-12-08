// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'narudzbastavka.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

NarudzbaStavka _$NarudzbaStavkaFromJson(Map<String, dynamic> json) =>
    NarudzbaStavka(
      narudzbaStavkaId: (json['narudzbaStavkaId'] as num?)?.toInt(),
      narudzbaId: (json['narudzbaId'] as num?)?.toInt(),
      proizvodId: (json['proizvodId'] as num?)?.toInt(),
      datumNarudzbe: json['datumNarudzbe'] == null
          ? null
          : DateTime.parse(json['datumNarudzbe'] as String),
      kolicinaProizvoda: (json['kolicinaProizvoda'] as num?)?.toInt(),
      cijena: (json['cijena'] as num?)?.toDouble(),
    );

Map<String, dynamic> _$NarudzbaStavkaToJson(NarudzbaStavka instance) =>
    <String, dynamic>{
      'narudzbaStavkaId': instance.narudzbaStavkaId,
      'narudzbaId': instance.narudzbaId,
      'proizvodId': instance.proizvodId,
      'datumNarudzbe': instance.datumNarudzbe?.toIso8601String(),
      'kolicinaProizvoda': instance.kolicinaProizvoda,
      'cijena': instance.cijena,
    };
