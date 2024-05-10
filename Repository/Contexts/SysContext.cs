using Entity;
using Entity.Dtos.User;
using Entity.SysModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contexts
{
    public class SysContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=AHLTEK-ETUREMIS\SQLEXPRESS;database=SysDb;Trusted_Connection = true; TrustServerCertificate = true;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var enumConverter = new EnumToStringConverter<Unit>();

            modelBuilder.Entity<Product>()
                .Property(p => p.Unit)
                .HasConversion(enumConverter);

            var categories = new List<Category>
{
                new Category { Id = 1, Name = "Meyve" },
                new Category { Id = 2, Name = "Sebze" },
                new Category { Id = 3, Name = "Et Ürünleri" },
                new Category { Id = 4, Name = "Süt Ürünleri" },
                new Category { Id = 5, Name = "Tatlılar" },
                new Category { Id = 6, Name = "Kahvaltılıklar" },
                new Category { Id = 7, Name = "Deniz Ürünleri" },
                new Category { Id = 8, Name = "Kuru Yemişler" },
                new Category { Id = 9, Name = "İçecekler" },
                new Category { Id = 10, Name = "Baharatlar" },
                new Category { Id = 11, Name = "Saklama Kabı" },
                new Category { Id = 12, Name = "Kahve" },
                new Category { Id = 13, Name = "Çay" },
                new Category { Id = 14, Name = "Dondurma" },
                new Category { Id = 15, Name = "Kuruyemiş" },
                new Category { Id = 16, Name = "Atıştırmalık" },
                new Category { Id = 17, Name = "Un ve Unlu Mamüller" },
                new Category { Id = 18, Name = "Pasta" },
                new Category { Id = 19, Name = "Pilavlık ve Bulgur" },
                new Category { Id = 20, Name = "Konserve ve Salça" },
   
            };
            foreach (var category in categories)
            {
                modelBuilder.Entity<Category>().HasData(category);
            }


        }


    }
}
