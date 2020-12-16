using E_Ticaret.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Contexts
{
    public class Context: DbContext

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.; database=ETicaret; Integrated Security=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().HasMany(I => I.UrunKategoriler).WithOne(I => I.Urun).HasForeignKey(I => I.UrunId);
            modelBuilder.Entity<Kategori>().HasMany(I => I.UrunKategoriler).WithOne(I => I.Kategori).HasForeignKey(I => I.KategoriId);

            modelBuilder.Entity<UrunKategori>().HasIndex(I => new
            {
                I.KategoriId,
                I.UrunId
            }).IsUnique();
        }

        public DbSet<UrunKategori> UrunKategoriler { get; set; }
        public DbSet<Urun>Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
    }
}
