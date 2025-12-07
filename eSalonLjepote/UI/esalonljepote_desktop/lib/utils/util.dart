import 'package:esalonljepote_desktop/models/korisnik.dart';
import 'package:esalonljepote_desktop/models/uloga.dart';

class Authorization {
  static String? username;
  static String? password;
  static Korisnik? korisnik;
  static Uloga? uloga;
}

extension IterableExtensions<E> on Iterable<E> {
  E? firstOrNull(bool Function(E element) test) {
    for (var e in this) {
      if (test(e)) return e;
    }
    return null;
  }
}
