using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class statusDat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "Cwlz94sfraOljhogq4Y/Ckf18zM=", "SyBxWf7HahpUq93b8gc9cA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "lExqhNAVpTWj0ll3Le5moi+azWo=", "uujcTuW4SD6bsw35z+xD6Q==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "8rViO5RYZl81Rck7Ghbqomm6aiE=", "Wy1CHDmpIsGaCc06Z0sEbA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "LK2MZXQnd7PXNm6ryTtkZUvRmfA=", "fKh5ot18W7g3AYoCLYV1Ww==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "RflzSS/hDVpp7u2Q4IYY7gEzsqw=", "CQQVAHHP7WP56wp2Cs1Pzg==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 16, 9, 56, 64, DateTimeKind.Local).AddTicks(9942));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 16, 9, 56, 65, DateTimeKind.Local).AddTicks(7));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 16, 9, 56, 65, DateTimeKind.Local).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 16, 9, 56, 65, DateTimeKind.Local).AddTicks(11));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 16, 9, 56, 65, DateTimeKind.Local).AddTicks(14));

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusNarudzbeId", "Naziv" },
                values: new object[] { 8576, "otkazano" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusNarudzbeId",
                keyValue: 8576);

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "QdGXjmW1PmBuj3n8c30OrrtDrsE=", "AyqT0jL0/ELJE+BAMN1orA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "kmJEmAdpOjNkq7CTGPKG8GK8ou8=", "HhIt7H1S/wlhNr5HS0EXyA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "9mlpxYUPUju9+Hut9bj99u1DG74=", "7pO5vBlFmouIT5Wkn8O9Nw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "QydKFZvgUBjGQ2nUQ7/hL7Eb7Nk=", "Xh4liB14FlskwiErBQunyw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "h04fVn75e+JgpKDzgmAY38n4e1M=", "tWrPVQUNCvOYCvJ578xe0A==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 47, 57, 738, DateTimeKind.Local).AddTicks(4979));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 47, 57, 738, DateTimeKind.Local).AddTicks(5042));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 47, 57, 738, DateTimeKind.Local).AddTicks(5044));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 47, 57, 738, DateTimeKind.Local).AddTicks(5046));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 47, 57, 738, DateTimeKind.Local).AddTicks(5048));
        }
    }
}
