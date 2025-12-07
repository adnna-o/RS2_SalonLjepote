using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class korpData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kolicina",
                table: "Korpa",
                newName: "KolicinaProizvoda");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "+bwrh9v639E36KBQt8y1CWbNRJE=", "LGowJv4NC2iAK3fw52QYHQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "pEwqXyI9wSdR6DDLS1se0YykanQ=", "Za5rA04FutctMmOPIn0wtw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "n5rg4SV7G0kPETZDYRic6k9OPBU=", "hZHQaBVvdlUdDcZPpNbWQw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "JF8SaSWXDW0xATrFNoI0zr3N5NE=", "Cw9rMNjxu1apykJl7441Gw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "STFZtDlgIcZ6QJUCBme0JBtRfIk=", "Rx5K6scb2Rp7d1R9mLpVlA==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 12, 40, 294, DateTimeKind.Local).AddTicks(8399));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 12, 40, 294, DateTimeKind.Local).AddTicks(8477));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 12, 40, 294, DateTimeKind.Local).AddTicks(8479));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 12, 40, 294, DateTimeKind.Local).AddTicks(8482));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 12, 40, 294, DateTimeKind.Local).AddTicks(8484));

            migrationBuilder.UpdateData(
                table: "RadnoVrijeme",
                keyColumn: "RadnoVrijemeId",
                keyValue: 7001,
                columns: new[] { "RadnoVrijemeDo", "RadnoVrijemeOd" },
                values: new object[] { new DateTime(2025, 12, 7, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 7, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KolicinaProizvoda",
                table: "Korpa",
                newName: "Kolicina");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "atj4WVS2nhJvPrcBtUHDEXkQzy8=", "VzqzZUbtbOJa8BRXeOLWfA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "cLFMZnD4DvZWnoVKT4G67+1jzdE=", "ePV31R90lw/ha2xcElwNBQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "cNyIt7WEDP6utHMcvOHOAAHyRgo=", "TrMDPsZxXJxszTRhCTUI/Q==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "lmdgSoW2+wZnjoZ9xkJ9/bedI7E=", "AZarj/qt9TMHfPePpyjb5Q==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "2YyF4ehOfexfjc2mkaOHRWYDLTo=", "lMp31xnrYkmjbstZ951qNA==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 20, 46, 296, DateTimeKind.Local).AddTicks(4455));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 20, 46, 296, DateTimeKind.Local).AddTicks(4523));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 20, 46, 296, DateTimeKind.Local).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 20, 46, 296, DateTimeKind.Local).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 20, 46, 296, DateTimeKind.Local).AddTicks(4530));

            migrationBuilder.UpdateData(
                table: "RadnoVrijeme",
                keyColumn: "RadnoVrijemeId",
                keyValue: 7001,
                columns: new[] { "RadnoVrijemeDo", "RadnoVrijemeOd" },
                values: new object[] { new DateTime(2025, 12, 6, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 6, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
