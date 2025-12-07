import 'dart:convert';
 
import 'package:json_annotation/json_annotation.dart';
 
part 'status.g.dart';
 
@JsonSerializable()
class Status{
int? statusNarudzbeId;
String? naziv;
 
 
Status(this.statusNarudzbeId, this.naziv);
 
factory Status.fromJson(Map<String,dynamic> json)=>_$StatusFromJson(json);
 
Map<String,dynamic> toJson()=>_$StatusToJson(this);
}