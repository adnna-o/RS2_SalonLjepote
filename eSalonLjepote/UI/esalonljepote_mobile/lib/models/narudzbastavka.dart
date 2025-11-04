import 'dart:ffi';

import 'package:json_annotation/json_annotation.dart';

/// This allows the `User` class to access private members in
/// the generated file. The value for this is *.g.dart, where
/// the star denotes the source file name.
part 'narudzbastavka.g.dart';

/// An annotation for the code generator to know that this class needs the
/// JSON serialization logic to be generated.
@JsonSerializable()
class NarudzbaStavka {
  NarudzbaStavka({
    this.narudzbaStavkaId,
    this.narudzbaId,
    this.proizvodId,
    this.datumNarudzbe,
    this.kolicina,
    this.cijena,
  });
  int? narudzbaStavkaId;
  int? narudzbaId;
  int? proizvodId;
  DateTime? datumNarudzbe;
  int? kolicina;
  double? cijena;

  factory NarudzbaStavka.fromJson(Map<String, dynamic> json) =>
      _$NarudzbaStavkaFromJson(json);

  Map<String, dynamic> toJson() => _$NarudzbaStavkaToJson(this);
}
