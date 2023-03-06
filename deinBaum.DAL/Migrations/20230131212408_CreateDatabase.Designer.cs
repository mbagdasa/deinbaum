﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using deinBaum.DAL;

#nullable disable

namespace deinBaum.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230131212408_CreateDatabase")]
    partial class CreateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("deinBaum.DAL.Model.BaumArtDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("BaumArtID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Art")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.ToTable("BaumArt");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Art = "Abies alba"
                        },
                        new
                        {
                            ID = 2,
                            Art = "Acer campestris"
                        },
                        new
                        {
                            ID = 3,
                            Art = "Acer opalus"
                        },
                        new
                        {
                            ID = 4,
                            Art = "Acer platanoides"
                        },
                        new
                        {
                            ID = 5,
                            Art = "Acer pseudoplatanus"
                        },
                        new
                        {
                            ID = 6,
                            Art = "Alnus glutinosa"
                        },
                        new
                        {
                            ID = 7,
                            Art = "Alnus incana"
                        },
                        new
                        {
                            ID = 8,
                            Art = "Betula pendula"
                        },
                        new
                        {
                            ID = 9,
                            Art = "Betula pubescens"
                        },
                        new
                        {
                            ID = 10,
                            Art = "Carpinus betulus"
                        },
                        new
                        {
                            ID = 11,
                            Art = "Castanea sativa"
                        },
                        new
                        {
                            ID = 12,
                            Art = "Cornus mas"
                        },
                        new
                        {
                            ID = 13,
                            Art = "Corylus avellana"
                        },
                        new
                        {
                            ID = 14,
                            Art = "Crataegus laevigata"
                        },
                        new
                        {
                            ID = 15,
                            Art = "Crataegus monogyna"
                        },
                        new
                        {
                            ID = 16,
                            Art = "Fagus sylvatica"
                        },
                        new
                        {
                            ID = 17,
                            Art = "Frangula alnus"
                        },
                        new
                        {
                            ID = 18,
                            Art = "Fraxinus excelsior"
                        },
                        new
                        {
                            ID = 19,
                            Art = "Fraxinus ornus"
                        },
                        new
                        {
                            ID = 20,
                            Art = "Ilex aquifolium"
                        },
                        new
                        {
                            ID = 21,
                            Art = "Juglans regia"
                        },
                        new
                        {
                            ID = 22,
                            Art = "Juniperus communis"
                        },
                        new
                        {
                            ID = 23,
                            Art = "Laburnum alpinum"
                        },
                        new
                        {
                            ID = 24,
                            Art = "Laburnum anagyroides"
                        },
                        new
                        {
                            ID = 25,
                            Art = "Larix decidua"
                        },
                        new
                        {
                            ID = 26,
                            Art = "Malus sylvestris"
                        },
                        new
                        {
                            ID = 27,
                            Art = "Ostrya carpinifolia"
                        },
                        new
                        {
                            ID = 28,
                            Art = "Picea abies"
                        },
                        new
                        {
                            ID = 29,
                            Art = "Pinus cembra"
                        },
                        new
                        {
                            ID = 30,
                            Art = "Pinus mugo ssp.uncinata"
                        },
                        new
                        {
                            ID = 31,
                            Art = "Pinus sylvestris"
                        },
                        new
                        {
                            ID = 32,
                            Art = "Populus alba"
                        },
                        new
                        {
                            ID = 33,
                            Art = "Populus nigra"
                        },
                        new
                        {
                            ID = 34,
                            Art = "Populus tremula"
                        },
                        new
                        {
                            ID = 35,
                            Art = "Prunus avium"
                        },
                        new
                        {
                            ID = 36,
                            Art = "Prunus padus"
                        },
                        new
                        {
                            ID = 37,
                            Art = "Prunus spinosa"
                        },
                        new
                        {
                            ID = 38,
                            Art = "Pyrus pyraster"
                        },
                        new
                        {
                            ID = 39,
                            Art = "Quercus petraea"
                        },
                        new
                        {
                            ID = 40,
                            Art = "Quercus pubescens"
                        },
                        new
                        {
                            ID = 41,
                            Art = "Quercus robur"
                        },
                        new
                        {
                            ID = 42,
                            Art = "Salix alba"
                        },
                        new
                        {
                            ID = 43,
                            Art = "Salix caprea"
                        },
                        new
                        {
                            ID = 44,
                            Art = "Sambucus nigra"
                        },
                        new
                        {
                            ID = 45,
                            Art = "Sorbus aria"
                        },
                        new
                        {
                            ID = 46,
                            Art = "Sorbus aucuparia"
                        },
                        new
                        {
                            ID = 47,
                            Art = "Sorbus domestica"
                        },
                        new
                        {
                            ID = 48,
                            Art = "Sorbus torminalis"
                        },
                        new
                        {
                            ID = 49,
                            Art = "Taxus baccata"
                        },
                        new
                        {
                            ID = 50,
                            Art = "Tilia cordata"
                        },
                        new
                        {
                            ID = 51,
                            Art = "Tilia platyphyllos"
                        },
                        new
                        {
                            ID = 52,
                            Art = "Ulmus glabra"
                        },
                        new
                        {
                            ID = 53,
                            Art = "Ulmus laevis"
                        },
                        new
                        {
                            ID = 54,
                            Art = "Ulmus minor"
                        });
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("BaumID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int?>("Alter")
                        .HasColumnType("integer");

                    b.Property<int?>("ArtID")
                        .HasColumnType("integer");

                    b.Property<double>("Baumhoehe")
                        .HasColumnType("double precision");

                    b.Property<string>("Bemerkung")
                        .HasColumnType("text");

                    b.Property<double?>("EllipsoidischeHoehe")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ErsteErfassung")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("FeldmitarbeiterID")
                        .HasColumnType("integer");

                    b.Property<double?>("HoeheMeterUeberMeer")
                        .HasColumnType("double precision");

                    b.Property<double>("LV95_EKoordinaten")
                        .HasColumnType("double precision");

                    b.Property<double>("LV95_NKoordinaten")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("LetzteBearbeitung")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("ParzellenNr")
                        .HasColumnType("integer");

                    b.Property<double>("Umfang")
                        .HasColumnType("double precision");

                    b.Property<double>("WGS84_XKoordinaten")
                        .HasColumnType("double precision");

                    b.Property<double>("WGS84_YKoordinaten")
                        .HasColumnType("double precision");

                    b.Property<int?>("WaldeigentuemerID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("ArtID");

                    b.HasIndex("FeldmitarbeiterID");

                    b.HasIndex("WaldeigentuemerID");

                    b.ToTable("Baum");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumMerkmalDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("BaumMerkmalID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Merkmal")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.ToTable("BaumMerkmal");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Merkmal = "Keine Strukturen "
                        },
                        new
                        {
                            ID = 2,
                            Merkmal = "mehrstämmig tiefer 1 m"
                        },
                        new
                        {
                            ID = 3,
                            Merkmal = "Zwiesel untere Stammhälfte"
                        },
                        new
                        {
                            ID = 4,
                            Merkmal = "Flötenbaum"
                        },
                        new
                        {
                            ID = 5,
                            Merkmal = "Spechthöhle gross"
                        },
                        new
                        {
                            ID = 6,
                            Merkmal = "kleine Spechthöhle/n"
                        },
                        new
                        {
                            ID = 7,
                            Merkmal = "Höhle/n in Totholz"
                        },
                        new
                        {
                            ID = 8,
                            Merkmal = "Frasshöhle in Totholz"
                        },
                        new
                        {
                            ID = 9,
                            Merkmal = "Mulmhöhle"
                        },
                        new
                        {
                            ID = 10,
                            Merkmal = "hohler Stammfuss"
                        },
                        new
                        {
                            ID = 11,
                            Merkmal = "andere Höhle/Vertiefung"
                        },
                        new
                        {
                            ID = 12,
                            Merkmal = "wassergefüllte Höhle (Dendrotelm)"
                        },
                        new
                        {
                            ID = 13,
                            Merkmal = "Riss/Rindenverletzung < 50 cm"
                        },
                        new
                        {
                            ID = 14,
                            Merkmal = "Riss/Rindenverletzung > 50 cm"
                        },
                        new
                        {
                            ID = 15,
                            Merkmal = "Riss/Rindenverletzung verheilt"
                        },
                        new
                        {
                            ID = 16,
                            Merkmal = "Astausbruch gross"
                        },
                        new
                        {
                            ID = 17,
                            Merkmal = "Kronenbruch"
                        },
                        new
                        {
                            ID = 18,
                            Merkmal = "Stammbruch"
                        },
                        new
                        {
                            ID = 19,
                            Merkmal = "Totholz in der Krone (> 10 cm)"
                        },
                        new
                        {
                            ID = 20,
                            Merkmal = "Hexenbesen"
                        },
                        new
                        {
                            ID = 21,
                            Merkmal = "Klebäste"
                        },
                        new
                        {
                            ID = 22,
                            Merkmal = "Wucherung (Maserknolle/Krebs)"
                        },
                        new
                        {
                            ID = 23,
                            Merkmal = "Baumpilze verholzt"
                        },
                        new
                        {
                            ID = 24,
                            Merkmal = "Baumpilze weich"
                        },
                        new
                        {
                            ID = 25,
                            Merkmal = "Moose  (> 10 % Stammfläche)"
                        },
                        new
                        {
                            ID = 26,
                            Merkmal = "Flechten (> 10 % Stammfläche)"
                        },
                        new
                        {
                            ID = 27,
                            Merkmal = "Efeu"
                        },
                        new
                        {
                            ID = 28,
                            Merkmal = "Waldrebe"
                        },
                        new
                        {
                            ID = 29,
                            Merkmal = "Farne"
                        },
                        new
                        {
                            ID = 30,
                            Merkmal = "Misteln"
                        },
                        new
                        {
                            ID = 31,
                            Merkmal = "Epiphyten (weitere)"
                        },
                        new
                        {
                            ID = 32,
                            Merkmal = "Harz-/Saftfluss"
                        },
                        new
                        {
                            ID = 33,
                            Merkmal = "Ameisenstrasse"
                        },
                        new
                        {
                            ID = 34,
                            Merkmal = "Frassgänge von Insekten"
                        },
                        new
                        {
                            ID = 35,
                            Merkmal = "Bienen-/Wespen-/Hornissennest"
                        },
                        new
                        {
                            ID = 36,
                            Merkmal = "Horst (> 30 cm)"
                        },
                        new
                        {
                            ID = 37,
                            Merkmal = "Nest (< 30 cm)"
                        },
                        new
                        {
                            ID = 38,
                            Merkmal = "Säugetierhöhle Stammfuss"
                        });
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumMerkmalRelationDTO", b =>
                {
                    b.Property<int>("MerkmalID")
                        .HasColumnType("integer");

                    b.Property<int>("BaumID")
                        .HasColumnType("integer");

                    b.HasKey("MerkmalID", "BaumID");

                    b.HasIndex("BaumID");

                    b.ToTable("BaumMerkmalRelation");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumZustandDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("BaumZustandID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Zustand")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.ToTable("BaumZustand");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Zustand = "tot"
                        },
                        new
                        {
                            ID = 2,
                            Zustand = "lebendig"
                        },
                        new
                        {
                            ID = 3,
                            Zustand = "stehend"
                        },
                        new
                        {
                            ID = 4,
                            Zustand = "liegend"
                        });
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumZustandRelationDTO", b =>
                {
                    b.Property<int>("ZustandID")
                        .HasColumnType("integer");

                    b.Property<int>("BaumID")
                        .HasColumnType("integer");

                    b.HasKey("ZustandID", "BaumID");

                    b.HasIndex("BaumID");

                    b.ToTable("BaumZustandRelation");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.FeldmitarbeiterDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("FeldmitarbeiterID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<bool>("ArbeitetNochInDerFirma")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<byte[]>("Profilbild")
                        .HasColumnType("bytea");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("ID");

                    b.ToTable("Feldmitarbeiter");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.FotoDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("FotoID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("BaumID")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Fotobytes")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.HasIndex("BaumID");

                    b.ToTable("Foto");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.UserDTO", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("text")
                        .HasColumnName("Loginname");

                    b.Property<bool>("IstAdminBerechtigt")
                        .HasColumnType("boolean");

                    b.Property<bool>("IstUserAktiv")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TokenErstelltAm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TokenLaeuftAbAm")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Login");

                    b.ToTable("User");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.WaldeigentuemerDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("WaldeigentuemerID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MobileNr")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Ort")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("PLZ")
                        .HasColumnType("integer");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("ID");

                    b.ToTable("Waldeigentuemer");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumDTO", b =>
                {
                    b.HasOne("deinBaum.DAL.Model.BaumArtDTO", "Art")
                        .WithMany()
                        .HasForeignKey("ArtID");

                    b.HasOne("deinBaum.DAL.Model.FeldmitarbeiterDTO", "Feldmitarbeiter")
                        .WithMany()
                        .HasForeignKey("FeldmitarbeiterID");

                    b.HasOne("deinBaum.DAL.Model.WaldeigentuemerDTO", "Waldeigentuemer")
                        .WithMany()
                        .HasForeignKey("WaldeigentuemerID");

                    b.Navigation("Art");

                    b.Navigation("Feldmitarbeiter");

                    b.Navigation("Waldeigentuemer");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumMerkmalRelationDTO", b =>
                {
                    b.HasOne("deinBaum.DAL.Model.BaumDTO", "Baum")
                        .WithMany("BaumMerkmalRelation")
                        .HasForeignKey("BaumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("deinBaum.DAL.Model.BaumMerkmalDTO", "Merkmal")
                        .WithMany("BaumMerkmalRelation")
                        .HasForeignKey("MerkmalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Baum");

                    b.Navigation("Merkmal");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumZustandRelationDTO", b =>
                {
                    b.HasOne("deinBaum.DAL.Model.BaumDTO", "Baum")
                        .WithMany("BaumZustandRelation")
                        .HasForeignKey("BaumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("deinBaum.DAL.Model.BaumZustandDTO", "Zustand")
                        .WithMany("BaumZustandRelation")
                        .HasForeignKey("ZustandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Baum");

                    b.Navigation("Zustand");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.FotoDTO", b =>
                {
                    b.HasOne("deinBaum.DAL.Model.BaumDTO", "Baum")
                        .WithMany("FotoListe")
                        .HasForeignKey("BaumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Baum");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumDTO", b =>
                {
                    b.Navigation("BaumMerkmalRelation");

                    b.Navigation("BaumZustandRelation");

                    b.Navigation("FotoListe");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumMerkmalDTO", b =>
                {
                    b.Navigation("BaumMerkmalRelation");
                });

            modelBuilder.Entity("deinBaum.DAL.Model.BaumZustandDTO", b =>
                {
                    b.Navigation("BaumZustandRelation");
                });
#pragma warning restore 612, 618
        }
    }
}