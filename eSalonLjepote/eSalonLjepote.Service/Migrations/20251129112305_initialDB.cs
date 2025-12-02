using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalonLjepote.Service.Migrations
{
    /// <inheritdoc />
    public partial class initialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Spol = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LozinkaSalt = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LozinkaHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Korisnik__80B06D41211C7C55", x => x.KorisnikId);
                });

            migrationBuilder.CreateTable(
                name: "Placanje",
                columns: table => new
                {
                    PlacanjeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NacinPlacanja = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Placanje__DDE16DECD82236A6", x => x.PlacanjeId);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    ProizvodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivProizvoda = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Cijena = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proizvod__21A8BFF863D092F1", x => x.ProizvodId);
                });

            migrationBuilder.CreateTable(
                name: "RadnoVrijeme",
                columns: table => new
                {
                    RadnoVrijemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RadnoVrijemeOd = table.Column<DateTime>(type: "datetime", nullable: false),
                    RadnoVrijemeDo = table.Column<DateTime>(type: "datetime", nullable: false),
                    VrijemePauze = table.Column<TimeSpan>(type: "time", nullable: true),
                    NeradniDani = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KolektivniOdmor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RadnoVri__715793313FEB1A8E", x => x.RadnoVrijemeId);
                });

            migrationBuilder.CreateTable(
                name: "Uloga",
                columns: table => new
                {
                    UlogaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivUloge = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloga", x => x.UlogaId);
                });

            migrationBuilder.CreateTable(
                name: "Usluga",
                columns: table => new
                {
                    UslugaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivUsluge = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cijena = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Trajanje = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usluga__0BE5E72F452B3D17", x => x.UslugaId);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    AdministratorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumZaposlenja = table.Column<DateTime>(type: "datetime", nullable: true),
                    OpisPosla = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Administ__ACDEFED39E30171F", x => x.AdministratorId);
                    table.ForeignKey(
                        name: "FK_Korisnik_Administrator",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId");
                });

            migrationBuilder.CreateTable(
                name: "Novosti",
                columns: table => new
                {
                    NovostiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OpisNovisti = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DatumObjave = table.Column<DateTime>(type: "datetime", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    Aktivna = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Novosti__451A108B7338A4FF", x => x.NovostiId);
                    table.ForeignKey(
                        name: "FK_Korisnik_Novosti",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId");
                });

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    RecenzijeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    OpisRecenzije = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ocjena = table.Column<int>(type: "int", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Recenzij__C077A33644BE8AAA", x => x.RecenzijeId);
                    table.ForeignKey(
                        name: "FK_Korisnik_Recenzije",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId");
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    ZaposleniId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumZaposlenja = table.Column<DateTime>(type: "datetime", nullable: true),
                    Zanimanje = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Zaposlen__8D3A91B7793939DB", x => x.ZaposleniId);
                    table.ForeignKey(
                        name: "FK_Korisnik_Zaposleni",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId");
                });

            migrationBuilder.CreateTable(
                name: "Korpa",
                columns: table => new
                {
                    KorpaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProizvodId = table.Column<int>(type: "int", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    Kolicina = table.Column<int>(type: "int", maxLength: 20, nullable: true),
                    Cijena = table.Column<decimal>(type: "decimal(18,2)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpa", x => x.KorpaId);
                    table.ForeignKey(
                        name: "FK_Korpa_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId");
                    table.ForeignKey(
                        name: "FK_Korpa_Proizvod_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvod",
                        principalColumn: "ProizvodId");
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    NarudzbaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProizvodId = table.Column<int>(type: "int", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    PlacanjeId = table.Column<int>(type: "int", nullable: true),
                    DatumNarudzbe = table.Column<DateTime>(type: "datetime", nullable: true),
                    KolicinaProizvoda = table.Column<int>(type: "int", nullable: true),
                    IznosNarudzbe = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Narudzba__FBEC1377EFA0DB96", x => x.NarudzbaId);
                    table.ForeignKey(
                        name: "FK_Korisnik_Narudzba",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId");
                    table.ForeignKey(
                        name: "FK_Placanje_Narudzba",
                        column: x => x.PlacanjeId,
                        principalTable: "Placanje",
                        principalColumn: "PlacanjeId");
                    table.ForeignKey(
                        name: "FK_Proizvod_Narudzba",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvod",
                        principalColumn: "ProizvodId");
                });

            migrationBuilder.CreateTable(
                name: "OcjeneProizvoda",
                columns: table => new
                {
                    OcjeneProizvodaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocjena = table.Column<int>(type: "int", maxLength: 20, nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProizvodId = table.Column<int>(type: "int", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcjeneProizvoda", x => x.OcjeneProizvodaId);
                    table.ForeignKey(
                        name: "FK_OcjeneProizvoda_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcjeneProizvoda_Proizvod_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvod",
                        principalColumn: "ProizvodId");
                });

            migrationBuilder.CreateTable(
                name: "KorisnikUloga",
                columns: table => new
                {
                    KorisnikUlogaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UlogaId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    DatumIzmjene = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Korisnik__1608726E523E7C40", x => x.KorisnikUlogaId);
                    table.ForeignKey(
                        name: "FK_Korisnik_KorisniciUloga",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId");
                    table.ForeignKey(
                        name: "FK_Uloga_KorisniciUloga",
                        column: x => x.UlogaId,
                        principalTable: "Uloga",
                        principalColumn: "UlogaId");
                });

            migrationBuilder.CreateTable(
                name: "Galerija",
                columns: table => new
                {
                    GalerijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AdministratorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Galerija__39DCA3DB934C538C", x => x.GalerijaId);
                    table.ForeignKey(
                        name: "FK_Administrator_Galerija",
                        column: x => x.AdministratorId,
                        principalTable: "Administrator",
                        principalColumn: "AdministratorId");
                });

            migrationBuilder.CreateTable(
                name: "SalonLjepote",
                columns: table => new
                {
                    SalonLjepoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivSalona = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RadnoVrijemeId = table.Column<int>(type: "int", nullable: false),
                    AdministratorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SalonLje__E16A2127A1A571C5", x => x.SalonLjepoteId);
                    table.ForeignKey(
                        name: "FK_Administrator_SalonLjepote",
                        column: x => x.AdministratorId,
                        principalTable: "Administrator",
                        principalColumn: "AdministratorId");
                    table.ForeignKey(
                        name: "FK_RadnoVrijeme_SalonLjepote",
                        column: x => x.RadnoVrijemeId,
                        principalTable: "RadnoVrijeme",
                        principalColumn: "RadnoVrijemeId");
                });

            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    KlijentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    NarudzbaId = table.Column<int>(type: "int", nullable: true),
                    UslugaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Klijenti__5F05D8AE56D1252D", x => x.KlijentId);
                    table.ForeignKey(
                        name: "FK_Klijenti_Narudzba_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzba",
                        principalColumn: "NarudzbaId");
                    table.ForeignKey(
                        name: "FK_Klijenti_Usluga_UslugaId",
                        column: x => x.UslugaId,
                        principalTable: "Usluga",
                        principalColumn: "UslugaId");
                    table.ForeignKey(
                        name: "FK_Korisnik_Klijenti",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikId");
                });

            migrationBuilder.CreateTable(
                name: "NarudzbaStavkas",
                columns: table => new
                {
                    NarudzbaStavkaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NarudzbaId = table.Column<int>(type: "int", nullable: false),
                    ProizvodId = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: true),
                    Cijena = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NarudzbaStavkas", x => x.NarudzbaStavkaId);
                    table.ForeignKey(
                        name: "FK_NarudzbaStavkas_Narudzba_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzba",
                        principalColumn: "NarudzbaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NarudzbaStavkas_Proizvod_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvod",
                        principalColumn: "ProizvodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    TerminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlijentId = table.Column<int>(type: "int", nullable: false),
                    UslugaId = table.Column<int>(type: "int", nullable: false),
                    ZaposleniId = table.Column<int>(type: "int", nullable: false),
                    DatumTermina = table.Column<DateTime>(type: "datetime", nullable: false),
                    VrijemeTermina = table.Column<TimeSpan>(type: "time", nullable: false),
                    BrojTransakcije = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Termini__42126C9559EA80B6", x => x.TerminId);
                    table.ForeignKey(
                        name: "FK_Klijent_Termini",
                        column: x => x.KlijentId,
                        principalTable: "Klijenti",
                        principalColumn: "KlijentId");
                    table.ForeignKey(
                        name: "FK_Usluga_Termini",
                        column: x => x.UslugaId,
                        principalTable: "Usluga",
                        principalColumn: "UslugaId");
                    table.ForeignKey(
                        name: "FK_Zaposleni_Termini",
                        column: x => x.ZaposleniId,
                        principalTable: "Zaposleni",
                        principalColumn: "ZaposleniId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_KorisnikId",
                table: "Administrator",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Galerija_AdministratorId",
                table: "Galerija",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Klijenti_KorisnikId",
                table: "Klijenti",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Klijenti_NarudzbaId",
                table: "Klijenti",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_Klijenti_UslugaId",
                table: "Klijenti",
                column: "UslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUloga_KorisnikId",
                table: "KorisnikUloga",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUloga_UlogaId",
                table: "KorisnikUloga",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_KorisnikId",
                table: "Korpa",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_ProizvodId",
                table: "Korpa",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_KorisnikId",
                table: "Narudzba",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_PlacanjeId",
                table: "Narudzba",
                column: "PlacanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_ProizvodId",
                table: "Narudzba",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaStavkas_NarudzbaId",
                table: "NarudzbaStavkas",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaStavkas_ProizvodId",
                table: "NarudzbaStavkas",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Novosti_KorisnikId",
                table: "Novosti",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_OcjeneProizvoda_KorisnikId",
                table: "OcjeneProizvoda",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_OcjeneProizvoda_ProizvodId",
                table: "OcjeneProizvoda",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_KorisnikId",
                table: "Recenzije",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonLjepote_AdministratorId",
                table: "SalonLjepote",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonLjepote_RadnoVrijemeId",
                table: "SalonLjepote",
                column: "RadnoVrijemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_KlijentId",
                table: "Termini",
                column: "KlijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_UslugaId",
                table: "Termini",
                column: "UslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_ZaposleniId",
                table: "Termini",
                column: "ZaposleniId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposleni_KorisnikId",
                table: "Zaposleni",
                column: "KorisnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Galerija");

            migrationBuilder.DropTable(
                name: "KorisnikUloga");

            migrationBuilder.DropTable(
                name: "Korpa");

            migrationBuilder.DropTable(
                name: "NarudzbaStavkas");

            migrationBuilder.DropTable(
                name: "Novosti");

            migrationBuilder.DropTable(
                name: "OcjeneProizvoda");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "SalonLjepote");

            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.DropTable(
                name: "Uloga");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "RadnoVrijeme");

            migrationBuilder.DropTable(
                name: "Klijenti");

            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "Usluga");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Placanje");

            migrationBuilder.DropTable(
                name: "Proizvod");
        }
    }
}
