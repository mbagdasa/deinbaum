using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using deinBaum.DAL.Model;
using deinBaum.Lib.BaumStruktur;
using Microsoft.EntityFrameworkCore;

namespace deinBaum.DAL
{
    public class AppDbContext : DbContext
    {
        //TODO PORT àndern
        //private const string connectionString = @"postgres://postgres:postgrespw@localhost:55000";
        
        private string connectionString = $@"Host={Environment.GetEnvironmentVariable("DB_HOST")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Username=postgres;Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";
        
        //private string connectionString = $@"Host=localhost;Database=deinbaumDB;Port=5432;Username=postgres;Password=postgrespw";
        //private const string connectionString = @"Host=localhost;Database=deinbaumDB;Port=5432;Username=postgres;Password=postgrespw";
        //private const string connectionString = @"Host=localhost;Database=postgres-db-test;Port=5432;Username=postgres;Password=postgres";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
            
        }

        public DbSet<BaumDTO> Baum { get; set; }
        public DbSet<BaumArtDTO> BaumArt { get; set; }
        public DbSet<BaumZustandDTO> BaumZustand { get; set; }
        public DbSet<BaumMerkmalDTO> BaumMerkmal { get; set; }
        public DbSet<FotoDTO> Foto { get; set; }
        public DbSet<FeldmitarbeiterDTO> Feldmitarbeiter { get; set; }
        public DbSet<WaldeigentuemerDTO> Waldeigentuemer { get; set; }
        public DbSet<UserDTO> User { get; set; }

        public DbSet<BaumMerkmalRelationDTO> BaumMerkmalRelation { get; set; }
        public DbSet<BaumZustandRelationDTO> BaumZustandRelation { get; set; }

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //List<BaumMerkmalDTO> listeMerkmal = new List<BaumMerkmalDTO>();
            //List<BaumArtDTO> listeArt = new List<BaumArtDTO>();
            //List<BaumZustandDTO> listeZustand = new List<BaumZustandDTO>();

            modelBuilder.Entity<BaumMerkmalRelationDTO>()
                .HasKey(t => new { t.MerkmalID, t.BaumID });

            modelBuilder.Entity<BaumMerkmalRelationDTO>()
                .HasOne(pt => pt.Baum)
                .WithMany(p => p.BaumMerkmalRelation)
                .HasForeignKey(pt => pt.BaumID);

            modelBuilder.Entity<BaumMerkmalRelationDTO>()
                .HasOne(pt => pt.Merkmal)
                .WithMany(p => p.BaumMerkmalRelation)
                .HasForeignKey(pt => pt.MerkmalID);

            modelBuilder.Entity<BaumZustandRelationDTO>()
                .HasKey(t => new { t.ZustandID, t.BaumID });

            modelBuilder.Entity<BaumZustandRelationDTO>()
                .HasOne(pt => pt.Baum)
                .WithMany(p => p.BaumZustandRelation)
                .HasForeignKey(pt => pt.BaumID);

            modelBuilder.Entity<BaumZustandRelationDTO>()
                .HasOne(pt => pt.Zustand)
                .WithMany(p => p.BaumZustandRelation)
                .HasForeignKey(pt => pt.ZustandID);

            modelBuilder.Entity<UserDTO>(entity =>
            {
                entity.HasKey(e => e.Login);
            });
            modelBuilder.Entity<FeldmitarbeiterDTO>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).UseIdentityColumn();


            });
            modelBuilder.Entity<WaldeigentuemerDTO>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).UseIdentityColumn();
            });

            modelBuilder.Entity<BaumArtDTO>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).UseIdentityColumn();

            });
            modelBuilder.Entity<BaumArtDTO>().HasData(new List<BaumArtDTO>() 
            {
                new BaumArtDTO{ ID= 1, Art="Abies alba" },
                new BaumArtDTO{ ID= 2, Art="Acer campestris" },
                new BaumArtDTO{ ID= 3, Art="Acer opalus" },
                new BaumArtDTO{ ID= 4, Art="Acer platanoides" },
                new BaumArtDTO{ ID= 5, Art="Acer pseudoplatanus" },
                new BaumArtDTO{ ID= 6, Art="Alnus glutinosa" },
                new BaumArtDTO{ ID= 7, Art="Alnus incana" },
                new BaumArtDTO{ ID= 8, Art="Betula pendula" },
                new BaumArtDTO{ ID= 9, Art="Betula pubescens" },
                new BaumArtDTO{ ID= 10,Art= "Carpinus betulus" },
                new BaumArtDTO{ ID= 11,Art= "Castanea sativa" },
                new BaumArtDTO{ ID= 12,Art= "Cornus mas" },
                new BaumArtDTO{ ID= 13,Art= "Corylus avellana" },
                new BaumArtDTO{ ID= 14,Art= "Crataegus laevigata" },
                new BaumArtDTO{ ID= 15,Art= "Crataegus monogyna" },
                new BaumArtDTO{ ID= 16,Art= "Fagus sylvatica" },
                new BaumArtDTO{ ID= 17,Art= "Frangula alnus" },
                new BaumArtDTO{ ID= 18,Art= "Fraxinus excelsior" },
                new BaumArtDTO{ ID= 19,Art= "Fraxinus ornus" },
                new BaumArtDTO{ ID= 20,Art= "Ilex aquifolium" },
                new BaumArtDTO{ ID= 21,Art= "Juglans regia" },
                new BaumArtDTO{ ID= 22,Art= "Juniperus communis" },
                new BaumArtDTO{ ID= 23,Art= "Laburnum alpinum" },
                new BaumArtDTO{ ID= 24,Art= "Laburnum anagyroides" },
                new BaumArtDTO{ ID= 25,Art= "Larix decidua" },
                new BaumArtDTO{ ID= 26,Art= "Malus sylvestris" },
                new BaumArtDTO{ ID= 27,Art= "Ostrya carpinifolia" },
                new BaumArtDTO{ ID= 28,Art= "Picea abies" },
                new BaumArtDTO{ ID= 29,Art= "Pinus cembra" },
                new BaumArtDTO{ ID= 30,Art= "Pinus mugo ssp.uncinata" },
                new BaumArtDTO{ ID= 31,Art= "Pinus sylvestris" },
                new BaumArtDTO{ ID= 32,Art= "Populus alba" },
                new BaumArtDTO{ ID= 33,Art= "Populus nigra" },
                new BaumArtDTO{ ID= 34,Art= "Populus tremula" },
                new BaumArtDTO{ ID= 35,Art= "Prunus avium" },
                new BaumArtDTO{ ID= 36,Art= "Prunus padus" },
                new BaumArtDTO{ ID= 37,Art= "Prunus spinosa" },
                new BaumArtDTO{ ID= 38,Art= "Pyrus pyraster" },
                new BaumArtDTO{ ID= 39,Art= "Quercus petraea" },
                new BaumArtDTO{ ID= 40,Art= "Quercus pubescens" },
                new BaumArtDTO{ ID= 41,Art= "Quercus robur" },
                new BaumArtDTO{ ID= 42,Art= "Salix alba" },
                new BaumArtDTO{ ID= 43,Art= "Salix caprea" },
                new BaumArtDTO{ ID= 44,Art= "Sambucus nigra" },
                new BaumArtDTO{ ID= 45,Art= "Sorbus aria" },
                new BaumArtDTO{ ID= 46,Art= "Sorbus aucuparia" },
                new BaumArtDTO{ ID= 47,Art= "Sorbus domestica" },
                new BaumArtDTO{ ID= 48,Art= "Sorbus torminalis" },
                new BaumArtDTO{ ID= 49,Art= "Taxus baccata" },
                new BaumArtDTO{ ID= 50,Art= "Tilia cordata" },
                new BaumArtDTO{ ID= 51,Art= "Tilia platyphyllos" },
                new BaumArtDTO{ ID= 52,Art= "Ulmus glabra" },
                new BaumArtDTO{ ID= 53,Art= "Ulmus laevis" },
                new BaumArtDTO{ ID= 54,Art= "Ulmus minor" }

            });


            modelBuilder.Entity<BaumMerkmalDTO>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).UseIdentityColumn();


            });
            modelBuilder.Entity<BaumMerkmalDTO>().HasData( new List<BaumMerkmalDTO>() 
            {
new BaumMerkmalDTO  {ID= 1 , Merkmal="Keine Strukturen " },
new BaumMerkmalDTO { ID= 2 , Merkmal="mehrstämmig tiefer 1 m" },
new BaumMerkmalDTO { ID= 3 , Merkmal="Zwiesel untere Stammhälfte" },
new BaumMerkmalDTO { ID= 4 , Merkmal="Flötenbaum" },
new BaumMerkmalDTO { ID= 5 , Merkmal="Spechthöhle gross" },
new BaumMerkmalDTO { ID= 6 , Merkmal="kleine Spechthöhle/n" },
new BaumMerkmalDTO { ID= 7 , Merkmal="Höhle/n in Totholz" },
new BaumMerkmalDTO { ID= 8 , Merkmal="Frasshöhle in Totholz" },
new BaumMerkmalDTO { ID= 9 , Merkmal="Mulmhöhle" },
new BaumMerkmalDTO { ID= 10 ,Merkmal= "hohler Stammfuss" },
new BaumMerkmalDTO { ID= 11 ,Merkmal= "andere Höhle/Vertiefung" },
new BaumMerkmalDTO { ID= 12 ,Merkmal= "wassergefüllte Höhle (Dendrotelm)" },
new BaumMerkmalDTO { ID= 13 ,Merkmal= "Riss/Rindenverletzung < 50 cm" },
new BaumMerkmalDTO { ID= 14 ,Merkmal= "Riss/Rindenverletzung > 50 cm" },
new BaumMerkmalDTO { ID= 15 ,Merkmal= "Riss/Rindenverletzung verheilt" },
new BaumMerkmalDTO { ID= 16 ,Merkmal= "Astausbruch gross" },
new BaumMerkmalDTO { ID= 17 ,Merkmal= "Kronenbruch" },
new BaumMerkmalDTO { ID= 18 ,Merkmal= "Stammbruch" },
new BaumMerkmalDTO { ID= 19 ,Merkmal= "Totholz in der Krone (> 10 cm)" },
new BaumMerkmalDTO { ID= 20 ,Merkmal= "Hexenbesen" },
new BaumMerkmalDTO { ID= 21 ,Merkmal= "Klebäste" },
new BaumMerkmalDTO { ID= 22 ,Merkmal= "Wucherung (Maserknolle/Krebs)" },
new BaumMerkmalDTO { ID= 23 ,Merkmal= "Baumpilze verholzt" },
new BaumMerkmalDTO { ID= 24 ,Merkmal= "Baumpilze weich" },
new BaumMerkmalDTO { ID= 25 ,Merkmal= "Moose  (> 10 % Stammfläche)" },
new BaumMerkmalDTO { ID= 26 ,Merkmal= "Flechten (> 10 % Stammfläche)" },
new BaumMerkmalDTO { ID= 27 ,Merkmal= "Efeu" },
new BaumMerkmalDTO { ID= 28 ,Merkmal= "Waldrebe" },
new BaumMerkmalDTO { ID= 29 ,Merkmal= "Farne" },
new BaumMerkmalDTO { ID= 30 ,Merkmal= "Misteln" },
new BaumMerkmalDTO { ID= 31 ,Merkmal= "Epiphyten (weitere)" },
new BaumMerkmalDTO { ID= 32 ,Merkmal= "Harz-/Saftfluss" },
new BaumMerkmalDTO { ID= 33 ,Merkmal= "Ameisenstrasse" },
new BaumMerkmalDTO { ID= 34 ,Merkmal= "Frassgänge von Insekten" },
new BaumMerkmalDTO { ID= 35 ,Merkmal= "Bienen-/Wespen-/Hornissennest" },
new BaumMerkmalDTO { ID= 36 ,Merkmal= "Horst (> 30 cm)" },
new BaumMerkmalDTO { ID= 37 ,Merkmal= "Nest (< 30 cm)" },
new BaumMerkmalDTO { ID= 38 ,Merkmal= "Säugetierhöhle Stammfuss" }


            });


            modelBuilder.Entity<BaumZustandDTO>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).UseIdentityColumn();


            });
            modelBuilder.Entity<BaumZustandDTO>().HasData( new List<BaumZustandDTO>() 
            {
                new BaumZustandDTO{ ID= 1,Zustand= "tot" },
                new BaumZustandDTO{ ID= 2,Zustand= "lebendig" },
                new BaumZustandDTO{ ID= 3,Zustand= "stehend" },
                new BaumZustandDTO{ ID= 4,Zustand= "liegend" }
            });

         
            base.OnModelCreating(modelBuilder);

        }
    }
}
