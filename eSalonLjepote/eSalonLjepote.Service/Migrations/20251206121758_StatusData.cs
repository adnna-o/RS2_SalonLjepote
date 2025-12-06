using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class StatusData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "kcsmEyybz8A5tnXF3hPU2wbGBFI=", "HlG7oskR82jpNU0ydZGBUw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "zHtEYeV4Dr77HS4+YgO7gtOxaIc=", "xMhT14sIbM76+28pIWSdXA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "9yA/6WPB31iVVa8MXh9Z9yUTrq0=", "OA+8dGaSYK2HVTJSWy9bjQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "jyHkuizbe7LCH5NPp6BNGeItksY=", "etRmRiUozRNrR8vEBItXqg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "h+qfC4MHoEsHuxkLmLVo4wJ83dQ=", "sD/j6O2AW5AOB4pqcMmAYQ==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 17, 57, 970, DateTimeKind.Local).AddTicks(6001));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 17, 57, 970, DateTimeKind.Local).AddTicks(6067));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 17, 57, 970, DateTimeKind.Local).AddTicks(6069));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 17, 57, 970, DateTimeKind.Local).AddTicks(6071));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 13, 17, 57, 970, DateTimeKind.Local).AddTicks(6073));

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusNarudzbeId", "Naziv" },
                values: new object[] { 8578, "poslano" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusNarudzbeId",
                keyValue: 8578);

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "sqAWEhRTpkyKPGSLGv1u5LCD+Gw=", "qUsYR5zRdQti2W1vVv980g==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "k6aEjEK2sGkquJs4DqOMGN6TVqE=", "5Dage6LYnzRzoBMdyM/6zw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "0dGiFAXlYo3qFasQ96bPgL6JGjM=", "UBN4zvuBOtN8iv/Gw469/Q==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "S0iPulHC7h/Gn3sGNhNS5+SShZE=", "MX9WrCxlA8MHuQ13lJAdFg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "WVjSf9djsroqy9lIogjdcGWSJxE=", "TTOfVYD/Cz9oNHm/M//W+A==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 12, 19, 3, 794, DateTimeKind.Local).AddTicks(1649));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 12, 19, 3, 794, DateTimeKind.Local).AddTicks(1713));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 12, 19, 3, 794, DateTimeKind.Local).AddTicks(1715));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 12, 19, 3, 794, DateTimeKind.Local).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 6, 12, 19, 3, 794, DateTimeKind.Local).AddTicks(1719));
        }
    }
}
