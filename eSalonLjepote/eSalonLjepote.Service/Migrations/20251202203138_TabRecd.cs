using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class TabRecd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "Imcmoh+xORqJN6G/WtV2IrcY818=", "hBFJtg57d3RoVwee2fbgyg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "YrpQopL7pNp4+bq/wyQBAD/2peA=", "KWuspEDO8WS3c0TKWx77Vg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "AblURzeYXHLaEwYbJ8Snwn9SlWo=", "7MbCLCKBS+QMKk6AdFR/CA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "+R1O7rPSepqDemk9fDTpWOX/RUE=", "qL1j0yRCorZjJ1JbgKleRQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "xwkCvoTPtIUElEjU9s7R47R8l7w=", "9p0e+DdmLyrEwMRDSlVU3w==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 31, 38, 353, DateTimeKind.Local).AddTicks(6557));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 31, 38, 353, DateTimeKind.Local).AddTicks(6655));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 31, 38, 353, DateTimeKind.Local).AddTicks(6658));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 31, 38, 353, DateTimeKind.Local).AddTicks(6660));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 31, 38, 353, DateTimeKind.Local).AddTicks(6663));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8100,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8102,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8103,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8104,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8105,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8106,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8107,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8108,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8109,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8110,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8111,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8112,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8181,
                column: "DatumRecenzije",
                value: new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "CDLPQ300CJA90XyBk3Y3DadH4mw=", "PLKZN4VpRyZpONUQkEB2Qw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "/2P+WLs8FkIZFAmQxI3w8UasvNg=", "0uJeHvBHXU8BBBpaVCSpvw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "zjjO5jLXe9PPekeumF/e64nrQ9c=", "BrmuiufEyrCepdLMOKLJzg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "1B7bQBofZCntj9JB5+RrbmGLS84=", "qYPnH95hiyLykYZEy8+7WQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "x/3mxm7NQfbOwKzH6ugy8y2lNdM=", "bRUyRSTyp+4N3x8eu8d2cw==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 19, 23, 369, DateTimeKind.Local).AddTicks(6444));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 19, 23, 369, DateTimeKind.Local).AddTicks(6517));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 19, 23, 369, DateTimeKind.Local).AddTicks(6521));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 19, 23, 369, DateTimeKind.Local).AddTicks(6524));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 19, 23, 369, DateTimeKind.Local).AddTicks(6527));

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8100,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8102,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8103,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8104,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8105,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8106,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8107,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8108,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8109,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8110,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8111,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8112,
                column: "DatumRecenzije",
                value: null);

            migrationBuilder.UpdateData(
                table: "Recenzije",
                keyColumn: "RecenzijeId",
                keyValue: 8181,
                column: "DatumRecenzije",
                value: null);
        }
    }
}
