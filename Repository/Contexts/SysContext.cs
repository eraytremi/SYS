using Entity;
using Entity.Dtos.User;
using Entity.SysModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;

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
        public DbSet<GroupChat> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                new Category { Id = 1, Name = "Meyve", Picture="" , IsActive=true},
                new Category { Id = 2, Name = "Sebze", Picture="" , IsActive=true},
                new Category { Id = 3, Name = "Et Ürünleri", Picture="" , IsActive=true},
                new Category { Id = 4, Name = "Süt Ürünleri", Picture="" , IsActive=true},
                new Category { Id = 5, Name = "Tatlılar", Picture="", IsActive=true },
                new Category { Id = 6, Name = "Kahvaltılıklar", Picture="", IsActive=true },
                new Category { Id = 7, Name = "Deniz Ürünleri", Picture="" , IsActive=true},
                new Category { Id = 8, Name = "Kuru Yemişler", Picture="", IsActive=true },
                new Category { Id = 9, Name = "İçecekler", Picture="" , IsActive=true},
                new Category { Id = 10, Name = "Baharatlar", Picture="" , IsActive=true},
                new Category { Id = 11, Name = "Saklama Kabı", Picture="", IsActive=true },
                new Category { Id = 12, Name = "Kahve", Picture="", IsActive=true },
                new Category { Id = 13, Name = "Çay", Picture="", IsActive=true },
                new Category { Id = 14, Name = "Dondurma", Picture="", IsActive=true },
                new Category { Id = 15, Name = "Kuruyemiş", Picture="" , IsActive=true},
                new Category { Id = 16, Name = "Atıştırmalık", Picture="" , IsActive=true},
                new Category { Id = 17, Name = "Un ve Unlu Mamüller", Picture="" , IsActive=true},
                new Category { Id = 18, Name = "Pasta", Picture="" , IsActive=true},
                new Category { Id = 19, Name = "Pilavlık ve Bulgur", Picture="" , IsActive=true},
                new Category { Id = 20, Name = "Konserve ve Salça", Picture="", IsActive=true },
            };

            foreach (var category in categories)
            {
                modelBuilder.Entity<Category>().HasData(category);
            }

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<GroupChat>().HasKey(g => g.Id);
            modelBuilder.Entity<GroupMember>().HasKey(gm => gm.Id);
       

            modelBuilder.Entity<GroupMember>()
                 .HasOne(gm => gm.Group)
                 .WithMany(g => g.GroupMembers)
                 .HasForeignKey(gm => gm.GroupId);

            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.GroupMembers)
                .HasForeignKey(gm => gm.UserId);

           

            // PrivateMessage için kaskad silme/güncelleme davranışı belirtme
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(pm => pm.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(pm => pm.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict kullan

            modelBuilder.Entity<PrivateMessage>()
                .HasOne(pm => pm.Recipient)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(pm => pm.RecipientId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict kullan

            // GroupMessage için kaskad silme/güncelleme davranışı belirtme
            modelBuilder.Entity<GroupMessage>()
                .HasOne(gm => gm.Sender)
                .WithMany()
                .HasForeignKey(gm => gm.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict kullan

            modelBuilder.Entity<GroupMessage>()
                .HasOne(gm => gm.Group)
                .WithMany()
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict kullan
        }
    }
}
