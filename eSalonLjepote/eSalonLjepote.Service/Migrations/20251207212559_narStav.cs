using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class narStav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kolicina",
                table: "NarudzbaStavkas",
                newName: "KolicinaProizvoda");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "zasp1XEl5Z7uRU6U/2vFo/qPcpQ=", "IaJMDnsVG9J3Oyy4AZXeBw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "cXVmQBOslYtlCIOEop1QoPQ/2oc=", "CCRwuFbY9IESEZyhkEb0KQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "nmTlTuAq8gDsxO6htsw9xc5soo0=", "RPB4oF9s0tbF6DN2Y2Wszg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "/eXUETNPhn2zDCmcM0N7deMB03g=", "slXEAcv1+8zp8Hrlq/L0Dw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "MhrEKq18VgydjfrK/xv4z8zdlxE=", "6PRUSvqMmms/bJgnDT8fPg==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 25, 58, 703, DateTimeKind.Local).AddTicks(7415));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 25, 58, 703, DateTimeKind.Local).AddTicks(7482));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 25, 58, 703, DateTimeKind.Local).AddTicks(7484));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 25, 58, 703, DateTimeKind.Local).AddTicks(7486));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 7, 22, 25, 58, 703, DateTimeKind.Local).AddTicks(7488));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KolicinaProizvoda",
                table: "NarudzbaStavkas",
                newName: "Kolicina");

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
        }
    }
}
