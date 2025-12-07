// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'status.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Status _$StatusFromJson(Map<String, dynamic> json) => Status(
      (json['statusNarudzbeId'] as num?)?.toInt(),
      json['naziv'] as String?,
    );

Map<String, dynamic> _$StatusToJson(Status instance) => <String, dynamic>{
      'statusNarudzbeId': instance.statusNarudzbeId,
      'naziv': instance.naziv,
    };
