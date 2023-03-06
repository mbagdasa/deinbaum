using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace deinBaum.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaumArt",
                columns: table => new
                {
                    BaumArtID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Art = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaumArt", x => x.BaumArtID);
                });

            migrationBuilder.CreateTable(
                name: "BaumMerkmal",
                columns: table => new
                {
                    BaumMerkmalID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Merkmal = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaumMerkmal", x => x.BaumMerkmalID);
                });

            migrationBuilder.CreateTable(
                name: "BaumZustand",
                columns: table => new
                {
                    BaumZustandID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Zustand = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaumZustand", x => x.BaumZustandID);
                });

            migrationBuilder.CreateTable(
                name: "Feldmitarbeiter",
                columns: table => new
                {
                    FeldmitarbeiterID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Vorname = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Profilbild = table.Column<byte[]>(type: "bytea", nullable: true),
                    ArbeitetNochInDerFirma = table.Column<bool>(type: "boolean", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feldmitarbeiter", x => x.FeldmitarbeiterID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Loginname = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    IstAdminBerechtigt = table.Column<bool>(type: "boolean", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    TokenErstelltAm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TokenLaeuftAbAm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IstUserAktiv = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Loginname);
                });

            migrationBuilder.CreateTable(
                name: "Waldeigentuemer",
                columns: table => new
                {
                    WaldeigentuemerID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Vorname = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PLZ = table.Column<int>(type: "integer", nullable: false),
                    Ort = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MobileNr = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waldeigentuemer", x => x.WaldeigentuemerID);
                });

            migrationBuilder.CreateTable(
                name: "Baum",
                columns: table => new
                {
                    BaumID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParzellenNr = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ErsteErfassung = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LetzteBearbeitung = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Baumhoehe = table.Column<double>(type: "double precision", nullable: false),
                    Umfang = table.Column<double>(type: "double precision", nullable: false),
                    Alter = table.Column<int>(type: "integer", nullable: true),
                    ArtID = table.Column<int>(type: "integer", nullable: true),
                    Bemerkung = table.Column<string>(type: "text", nullable: true),
                    WGS84XKoordinaten = table.Column<double>(name: "WGS84_XKoordinaten", type: "double precision", nullable: false),
                    WGS84YKoordinaten = table.Column<double>(name: "WGS84_YKoordinaten", type: "double precision", nullable: false),
                    EllipsoidischeHoehe = table.Column<double>(type: "double precision", nullable: true),
                    HoeheMeterUeberMeer = table.Column<double>(type: "double precision", nullable: true),
                    LV95EKoordinaten = table.Column<double>(name: "LV95_EKoordinaten", type: "double precision", nullable: false),
                    LV95NKoordinaten = table.Column<double>(name: "LV95_NKoordinaten", type: "double precision", nullable: false),
                    FeldmitarbeiterID = table.Column<int>(type: "integer", nullable: true),
                    WaldeigentuemerID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baum", x => x.BaumID);
                    table.ForeignKey(
                        name: "FK_Baum_BaumArt_ArtID",
                        column: x => x.ArtID,
                        principalTable: "BaumArt",
                        principalColumn: "BaumArtID");
                    table.ForeignKey(
                        name: "FK_Baum_Feldmitarbeiter_FeldmitarbeiterID",
                        column: x => x.FeldmitarbeiterID,
                        principalTable: "Feldmitarbeiter",
                        principalColumn: "FeldmitarbeiterID");
                    table.ForeignKey(
                        name: "FK_Baum_Waldeigentuemer_WaldeigentuemerID",
                        column: x => x.WaldeigentuemerID,
                        principalTable: "Waldeigentuemer",
                        principalColumn: "WaldeigentuemerID");
                });

            migrationBuilder.CreateTable(
                name: "BaumMerkmalRelation",
                columns: table => new
                {
                    BaumID = table.Column<int>(type: "integer", nullable: false),
                    MerkmalID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaumMerkmalRelation", x => new { x.MerkmalID, x.BaumID });
                    table.ForeignKey(
                        name: "FK_BaumMerkmalRelation_BaumMerkmal_MerkmalID",
                        column: x => x.MerkmalID,
                        principalTable: "BaumMerkmal",
                        principalColumn: "BaumMerkmalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaumMerkmalRelation_Baum_BaumID",
                        column: x => x.BaumID,
                        principalTable: "Baum",
                        principalColumn: "BaumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaumZustandRelation",
                columns: table => new
                {
                    BaumID = table.Column<int>(type: "integer", nullable: false),
                    ZustandID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaumZustandRelation", x => new { x.ZustandID, x.BaumID });
                    table.ForeignKey(
                        name: "FK_BaumZustandRelation_BaumZustand_ZustandID",
                        column: x => x.ZustandID,
                        principalTable: "BaumZustand",
                        principalColumn: "BaumZustandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaumZustandRelation_Baum_BaumID",
                        column: x => x.BaumID,
                        principalTable: "Baum",
                        principalColumn: "BaumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    FotoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Fotobytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    BaumID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.FotoID);
                    table.ForeignKey(
                        name: "FK_Foto_Baum_BaumID",
                        column: x => x.BaumID,
                        principalTable: "Baum",
                        principalColumn: "BaumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BaumArt",
                columns: new[] { "BaumArtID", "Art" },
                values: new object[,]
                {
                    { 1, "Abies alba" },
                    { 2, "Acer campestris" },
                    { 3, "Acer opalus" },
                    { 4, "Acer platanoides" },
                    { 5, "Acer pseudoplatanus" },
                    { 6, "Alnus glutinosa" },
                    { 7, "Alnus incana" },
                    { 8, "Betula pendula" },
                    { 9, "Betula pubescens" },
                    { 10, "Carpinus betulus" },
                    { 11, "Castanea sativa" },
                    { 12, "Cornus mas" },
                    { 13, "Corylus avellana" },
                    { 14, "Crataegus laevigata" },
                    { 15, "Crataegus monogyna" },
                    { 16, "Fagus sylvatica" },
                    { 17, "Frangula alnus" },
                    { 18, "Fraxinus excelsior" },
                    { 19, "Fraxinus ornus" },
                    { 20, "Ilex aquifolium" },
                    { 21, "Juglans regia" },
                    { 22, "Juniperus communis" },
                    { 23, "Laburnum alpinum" },
                    { 24, "Laburnum anagyroides" },
                    { 25, "Larix decidua" },
                    { 26, "Malus sylvestris" },
                    { 27, "Ostrya carpinifolia" },
                    { 28, "Picea abies" },
                    { 29, "Pinus cembra" },
                    { 30, "Pinus mugo ssp.uncinata" },
                    { 31, "Pinus sylvestris" },
                    { 32, "Populus alba" },
                    { 33, "Populus nigra" },
                    { 34, "Populus tremula" },
                    { 35, "Prunus avium" },
                    { 36, "Prunus padus" },
                    { 37, "Prunus spinosa" },
                    { 38, "Pyrus pyraster" },
                    { 39, "Quercus petraea" },
                    { 40, "Quercus pubescens" },
                    { 41, "Quercus robur" },
                    { 42, "Salix alba" },
                    { 43, "Salix caprea" },
                    { 44, "Sambucus nigra" },
                    { 45, "Sorbus aria" },
                    { 46, "Sorbus aucuparia" },
                    { 47, "Sorbus domestica" },
                    { 48, "Sorbus torminalis" },
                    { 49, "Taxus baccata" },
                    { 50, "Tilia cordata" },
                    { 51, "Tilia platyphyllos" },
                    { 52, "Ulmus glabra" },
                    { 53, "Ulmus laevis" },
                    { 54, "Ulmus minor" }
                });

            migrationBuilder.InsertData(
                table: "BaumMerkmal",
                columns: new[] { "BaumMerkmalID", "Merkmal" },
                values: new object[,]
                {
                    { 1, "Keine Strukturen " },
                    { 2, "mehrstämmig tiefer 1 m" },
                    { 3, "Zwiesel untere Stammhälfte" },
                    { 4, "Flötenbaum" },
                    { 5, "Spechthöhle gross" },
                    { 6, "kleine Spechthöhle/n" },
                    { 7, "Höhle/n in Totholz" },
                    { 8, "Frasshöhle in Totholz" },
                    { 9, "Mulmhöhle" },
                    { 10, "hohler Stammfuss" },
                    { 11, "andere Höhle/Vertiefung" },
                    { 12, "wassergefüllte Höhle (Dendrotelm)" },
                    { 13, "Riss/Rindenverletzung < 50 cm" },
                    { 14, "Riss/Rindenverletzung > 50 cm" },
                    { 15, "Riss/Rindenverletzung verheilt" },
                    { 16, "Astausbruch gross" },
                    { 17, "Kronenbruch" },
                    { 18, "Stammbruch" },
                    { 19, "Totholz in der Krone (> 10 cm)" },
                    { 20, "Hexenbesen" },
                    { 21, "Klebäste" },
                    { 22, "Wucherung (Maserknolle/Krebs)" },
                    { 23, "Baumpilze verholzt" },
                    { 24, "Baumpilze weich" },
                    { 25, "Moose  (> 10 % Stammfläche)" },
                    { 26, "Flechten (> 10 % Stammfläche)" },
                    { 27, "Efeu" },
                    { 28, "Waldrebe" },
                    { 29, "Farne" },
                    { 30, "Misteln" },
                    { 31, "Epiphyten (weitere)" },
                    { 32, "Harz-/Saftfluss" },
                    { 33, "Ameisenstrasse" },
                    { 34, "Frassgänge von Insekten" },
                    { 35, "Bienen-/Wespen-/Hornissennest" },
                    { 36, "Horst (> 30 cm)" },
                    { 37, "Nest (< 30 cm)" },
                    { 38, "Säugetierhöhle Stammfuss" }
                });

            migrationBuilder.InsertData(
                table: "BaumZustand",
                columns: new[] { "BaumZustandID", "Zustand" },
                values: new object[,]
                {
                    { 1, "tot" },
                    { 2, "lebendig" },
                    { 3, "stehend" },
                    { 4, "liegend" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baum_ArtID",
                table: "Baum",
                column: "ArtID");

            migrationBuilder.CreateIndex(
                name: "IX_Baum_FeldmitarbeiterID",
                table: "Baum",
                column: "FeldmitarbeiterID");

            migrationBuilder.CreateIndex(
                name: "IX_Baum_WaldeigentuemerID",
                table: "Baum",
                column: "WaldeigentuemerID");

            migrationBuilder.CreateIndex(
                name: "IX_BaumMerkmalRelation_BaumID",
                table: "BaumMerkmalRelation",
                column: "BaumID");

            migrationBuilder.CreateIndex(
                name: "IX_BaumZustandRelation_BaumID",
                table: "BaumZustandRelation",
                column: "BaumID");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_BaumID",
                table: "Foto",
                column: "BaumID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaumMerkmalRelation");

            migrationBuilder.DropTable(
                name: "BaumZustandRelation");

            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BaumMerkmal");

            migrationBuilder.DropTable(
                name: "BaumZustand");

            migrationBuilder.DropTable(
                name: "Baum");

            migrationBuilder.DropTable(
                name: "BaumArt");

            migrationBuilder.DropTable(
                name: "Feldmitarbeiter");

            migrationBuilder.DropTable(
                name: "Waldeigentuemer");
        }
    }
}
