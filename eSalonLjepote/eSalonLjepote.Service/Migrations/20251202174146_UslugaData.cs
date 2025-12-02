using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class UslugaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Slika",
                table: "Usluga",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "kOL2iFhHE11Iz5uQz1F+iz2wCYE=", "gOsIfRAsyHJLzl7q6hX5AA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "qJzxuM/m4iSYC085MOY+UXFK4Ts=", "aJle3hUWv9NE9n+9HFYlOA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "lkj6Mz4l4BTpcrbP3cy73xa6DBo=", "czLsbopJho6BRIlsYzIapw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "Z4cYed4ZSOXaWbqsypQERZp3zzE=", "ETyZYfudotv+/cGdYuITcw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "K7ay8M9DPHnDiL+ayOmtv58vxG8=", "tOvK+AUcaGdzib9oTwfYlA==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 18, 41, 45, 449, DateTimeKind.Local).AddTicks(9684));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 18, 41, 45, 449, DateTimeKind.Local).AddTicks(9759));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 18, 41, 45, 449, DateTimeKind.Local).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 18, 41, 45, 449, DateTimeKind.Local).AddTicks(9765));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 18, 41, 45, 449, DateTimeKind.Local).AddTicks(9767));

            migrationBuilder.UpdateData(
                table: "RadnoVrijeme",
                keyColumn: "RadnoVrijemeId",
                keyValue: 7001,
                columns: new[] { "RadnoVrijemeDo", "RadnoVrijemeOd" },
                values: new object[] { new DateTime(2025, 12, 2, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 2, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Usluga",
                keyColumn: "UslugaId",
                keyValue: 7110,
                column: "Slika",
                value: null);

            migrationBuilder.UpdateData(
                table: "Usluga",
                keyColumn: "UslugaId",
                keyValue: 7111,
                column: "Slika",
                value: null);

            migrationBuilder.UpdateData(
                table: "Usluga",
                keyColumn: "UslugaId",
                keyValue: 7112,
                column: "Slika",
                value: null);

            migrationBuilder.UpdateData(
                table: "Usluga",
                keyColumn: "UslugaId",
                keyValue: 7113,
                column: "Slika",
                value: null);

            migrationBuilder.UpdateData(
                table: "Usluga",
                keyColumn: "UslugaId",
                keyValue: 7114,
                column: "Slika",
                value: null);

            migrationBuilder.UpdateData(
                table: "Usluga",
                keyColumn: "UslugaId",
                keyValue: 7115,
                column: "Slika",
                value: null);

            migrationBuilder.UpdateData(
                table: "Usluga",
                keyColumn: "UslugaId",
                keyValue: 7121,
                column: "Slika",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Usluga");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "yagXaLSezzVBlxGTMrHmCrDAh/w=", "IieTG24loHmLkLK7F3quxA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "lWx4D7ZPZXJnAITFBgdnaxmB0mA=", "zzYCwSu1xvz6kdaJ3XxtBg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "PcUIKxMbDDobf7ydWrboMYmTIQQ=", "Nu4eUgTghSXcS3BtLMMiyg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "ZaIkOzqkjPGNQLDC4gcYzjZ+bP8=", "RpOnBbiXBGRicKt8uuELVg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "lRxH1wiXUFH5j+S1v/ob2mbbtfw=", "8fkSek5Cvzwu2hAvFjyBrg==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 11, 29, 12, 24, 0, 417, DateTimeKind.Local).AddTicks(8487));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 11, 29, 12, 24, 0, 417, DateTimeKind.Local).AddTicks(8544));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 11, 29, 12, 24, 0, 417, DateTimeKind.Local).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 11, 29, 12, 24, 0, 417, DateTimeKind.Local).AddTicks(8548));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 11, 29, 12, 24, 0, 417, DateTimeKind.Local).AddTicks(8550));

            migrationBuilder.UpdateData(
                table: "RadnoVrijeme",
                keyColumn: "RadnoVrijemeId",
                keyValue: 7001,
                columns: new[] { "RadnoVrijemeDo", "RadnoVrijemeOd" },
                values: new object[] { new DateTime(2025, 11, 29, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 29, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
