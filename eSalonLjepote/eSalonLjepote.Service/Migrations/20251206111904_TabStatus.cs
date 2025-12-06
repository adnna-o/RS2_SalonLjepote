using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class TabStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "Narudzba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusNarudzbeId",
                table: "Narudzba",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusNarudzbeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusNarudzbeId);
                });

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

            migrationBuilder.UpdateData(
                table: "RadnoVrijeme",
                keyColumn: "RadnoVrijemeId",
                keyValue: 7001,
                columns: new[] { "RadnoVrijemeDo", "RadnoVrijemeOd" },
                values: new object[] { new DateTime(2025, 12, 6, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 6, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_StatusNarudzbeId",
                table: "Narudzba",
                column: "StatusNarudzbeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Status_",
                table: "Narudzba",
                column: "StatusNarudzbeId",
                principalTable: "Status",
                principalColumn: "StatusNarudzbeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Status_",
                table: "Narudzba");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_StatusNarudzbeId",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "StatusNarudzbeId",
                table: "Narudzba");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1001,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "s9np5v3kc4qCecXV60DcWwOCZr8=", "gkbEw/mV0XOsZX5CYgxeLw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1002,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "JTSHbBcw89OAKUfV6kJcN5LHelM=", "YA9Dr3nQWABSsmS3Von4XA==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1003,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "sovcaZGprPo+QsKsndqqepp7cp4=", "BknA1uk6rfvmR1Szguzntg==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1004,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "sWiZ0frQYZxqlKDJDucWEWiZ24M=", "wsnVJmu9di2De+QxY/lAYw==" });

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "KorisnikId",
                keyValue: 1005,
                columns: new[] { "LozinkaHash", "LozinkaSalt" },
                values: new object[] { "c9fHCuTSXTVCY4jeSPOFn4jOe4M=", "gWIzSx48wwW29q2epqhQ/w==" });

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1012,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 42, 27, 150, DateTimeKind.Local).AddTicks(1563));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1013,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 42, 27, 150, DateTimeKind.Local).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1014,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 42, 27, 150, DateTimeKind.Local).AddTicks(1643));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1015,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 42, 27, 150, DateTimeKind.Local).AddTicks(1646));

            migrationBuilder.UpdateData(
                table: "KorisnikUloga",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1016,
                column: "DatumIzmjene",
                value: new DateTime(2025, 12, 2, 21, 42, 27, 150, DateTimeKind.Local).AddTicks(1649));

            migrationBuilder.UpdateData(
                table: "RadnoVrijeme",
                keyColumn: "RadnoVrijemeId",
                keyValue: 7001,
                columns: new[] { "RadnoVrijemeDo", "RadnoVrijemeOd" },
                values: new object[] { new DateTime(2025, 12, 2, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 2, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
