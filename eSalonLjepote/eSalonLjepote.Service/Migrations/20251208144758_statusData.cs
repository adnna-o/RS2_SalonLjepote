using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class statusData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "na6+S+ooxRmcRk2hJYCEQjlur5o=", "+71P4aQsCsT5OCyskNn17A==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "YbjCKzd1f4iJNgBrwre3VgYqczA=", "zRIrLe7ozOlnf/DCdYkcbg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "URj3aX27t50tYJjrLjB14VVw76s=", "oGTaFmNbj1EAWmrQCq/SqA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "4P+guLBq+IxVBBv6vzmlEHTSyJg=", "QO5/KCVSAJYybSc7IDELNQ==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "r5H/WB/q5SBR/ASd0BIYjqUdnns=", "IhAZ7g0PVU8pORFSMJpreQ==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 35, 25, 502, DateTimeKind.Local).AddTicks(981));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 35, 25, 502, DateTimeKind.Local).AddTicks(1043));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 35, 25, 502, DateTimeKind.Local).AddTicks(1045));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 35, 25, 502, DateTimeKind.Local).AddTicks(1047));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 8, 15, 35, 25, 502, DateTimeKind.Local).AddTicks(1049));

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusNarudzbeId", "Naziv" },
                values: new object[] { 8576, "otkazano" });
        }
    }
}
