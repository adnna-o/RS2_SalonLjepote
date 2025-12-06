using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class NaruData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Narudzba",
                keyColumn: "NarudzbaId",
                keyValue: 3100,
                columns: new[] { "StateMachine", "StatusNarudzbeId" },
                values: new object[] { "poslano", 8578 });

            migrationBuilder.UpdateData(
                table: "Narudzba",
                keyColumn: "NarudzbaId",
                keyValue: 3200,
                columns: new[] { "StateMachine", "StatusNarudzbeId" },
                values: new object[] { "poslano", 8578 });

            migrationBuilder.UpdateData(
                table: "Narudzba",
                keyColumn: "NarudzbaId",
                keyValue: 3300,
                columns: new[] { "StateMachine", "StatusNarudzbeId" },
                values: new object[] { "poslano", 8578 });

            migrationBuilder.UpdateData(
                table: "Narudzba",
                keyColumn: "NarudzbaId",
                keyValue: 3400,
                columns: new[] { "StateMachine", "StatusNarudzbeId" },
                values: new object[] { "poslano", 8578 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Narudzba",
                keyColumn: "NarudzbaId",
                keyValue: 3100,
                columns: new[] { "StateMachine", "StatusNarudzbeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Narudzba",
                keyColumn: "NarudzbaId",
                keyValue: 3200,
                columns: new[] { "StateMachine", "StatusNarudzbeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Narudzba",
                keyColumn: "NarudzbaId",
                keyValue: 3300,
                columns: new[] { "StateMachine", "StatusNarudzbeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Narudzba",
                keyColumn: "NarudzbaId",
                keyValue: 3400,
                columns: new[] { "StateMachine", "StatusNarudzbeId" },
                values: new object[] { null, null });
        }
    }
}
