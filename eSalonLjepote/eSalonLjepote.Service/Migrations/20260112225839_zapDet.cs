using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class zapDet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "qvD1hCv5WsWoT3d6nYC6G7D8IuU=", "NvSciWraJYePdgq9Qharew==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "H4zHMPQ3TQXovDfdR3rRtfuqq9I=", "R13GCjMLlMUdIhP01EDxVA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "9gviwoGNt6sHQYcJ7dkna0fF9q8=", "BLXGrngzVNYzAwNaF1ZewQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "TUhs1MdN5BIjneLUmj3bqXXureU=", "W78g1+NIlnMjfZq/OoosWg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "AD65afWOeCJkAN7dvce3TlOT4Co=", "HHKML6C0x/06XLDCVcolJw==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 23, 58, 38, 698, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 23, 58, 38, 698, DateTimeKind.Local).AddTicks(9248));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 23, 58, 38, 698, DateTimeKind.Local).AddTicks(9254));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 23, 58, 38, 698, DateTimeKind.Local).AddTicks(9259));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 23, 58, 38, 698, DateTimeKind.Local).AddTicks(9264));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "WOaVAsITH4KCeRXR1GKdWvL008Y=", "iCd6B/CFBf/tYwCTR1nIaw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "RfLhUYnfvzE9HNXnuteMBh/62kc=", "EC+jrjVmyjDoGMyYo1/UAQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "e+JeHmlc+NgYfx2vqZuhdp7b83c=", "WEtzgPGJ5Wm3DFGcHn2kRw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "O+TJO4vp6eNu9VqQM5cZqNW6G58=", "WWIBAGY4JaBDhV6eMsyKkQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "ELDDGc8b6AqHJS6hbatsKF7DZXY=", "YDsnKySw3r6pOeGqIszVKQ==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 17, 23, 57, 698, DateTimeKind.Local).AddTicks(6198));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 17, 23, 57, 698, DateTimeKind.Local).AddTicks(6275));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 17, 23, 57, 698, DateTimeKind.Local).AddTicks(6280));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 17, 23, 57, 698, DateTimeKind.Local).AddTicks(6283));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2026, 1, 12, 17, 23, 57, 698, DateTimeKind.Local).AddTicks(6287));
        }
    }
}
