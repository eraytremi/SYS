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
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Stock> StockStatuses { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesDetails> SalesDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var enumConverter = new EnumToStringConverter<Unit>();
         
            modelBuilder.Entity<Product>()
                .Property(p => p.Unit)
                .HasConversion(enumConverter);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            var categories = new List<Category>
{
                new Category { Id = 1, Name = "Meyve",Picture="" },
                new Category { Id = 2, Name = "Sebze",Picture="" },
                new Category { Id = 3, Name = "Et Ürünleri",Picture=""  },
                new Category { Id = 4, Name = "Süt Ürünleri",Picture=""  },
                new Category { Id = 5, Name = "Tatlılar",Picture=""  },
                new Category { Id = 6, Name = "Kahvaltılıklar",Picture=""  },
                new Category { Id = 7, Name = "Deniz Ürünleri",Picture=""  },
                new Category { Id = 8, Name = "Kuru Yemişler",Picture=""  },
                new Category { Id = 9, Name = "İçecekler",Picture=""  },
                new Category { Id = 10, Name = "Baharatlar",Picture=""  },
                new Category { Id = 11, Name = "Saklama Kabı",Picture=""  },
                new Category { Id = 12, Name = "Kahve",Picture=""  },
                new Category { Id = 13, Name = "Çay",Picture=""  },
                new Category { Id = 14, Name = "Dondurma" ,Picture="" },
                new Category { Id = 15, Name = "Kuruyemiş",Picture=""  },
                new Category { Id = 16, Name = "Atıştırmalık",Picture=""  },
                new Category { Id = 17, Name = "Un ve Unlu Mamüller",Picture=""  },
                new Category { Id = 18, Name = "Pasta" , Picture = ""},
                new Category { Id = 19, Name = "Pilavlık ve Bulgur",Picture=""  },
                new Category { Id = 20, Name = "Konserve ve Salça" , Picture = ""},
   
            };
            foreach (var category in categories)
            {
                modelBuilder.Entity<Category>().HasData(category);
            }


        }


    }
}
