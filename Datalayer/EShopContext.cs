using Datalayer.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer
{
    public class EShopContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Produkt>? Produkts { get; set; }
        public DbSet<UserProdukt>? UserProdukts { get; set; }
        public DbSet<Brand>? Brands { get; set; }
        public DbSet<Types>? Types { get; set; }

        public EShopContext(DbContextOptions<EShopContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProdukt>()
                .HasKey(s => new { s.UserId, s.ProduktId });

            //modelBuilder.Entity<ShoppingCart>()
            //     .HasMany(x => x.Produkts)
            //     .WithMany(x => x.ShoppingCart)
            //     .




            modelBuilder.Entity<Produkt>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            
            modelBuilder.Entity<Produkt>()
                .Property(p => p.IsSoftDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .Property(u => u.IsSoftDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Produkt>()
                .Property(u => u.ImageURL)
                .HasDefaultValue(@"Placeholder\istockphoto-1332167985-170667a.jpg");

            modelBuilder.Entity<Brand>().HasData( new Brand { BrandId = 1, BrandName = "Nvidia" },
                                                  new Brand { BrandId = 2, BrandName = "Logitech"},
                                                  new Brand { BrandId = 3, BrandName = "Asus" },
                                                  new Brand { BrandId = 4, BrandName = "AMD"},
                                                  new Brand { BrandId = 100, BrandName = "Andet"});
                     

            modelBuilder.Entity<Types>().HasData( new Types { TypesId = 1, TypeName = "GraphicsCard"},
                                                  new Types { TypesId = 2, TypeName = "Keyboard"},
                                                  new Types { TypesId = 3, TypeName = "Motherboard" },
                                                  new Types { TypesId = 4, TypeName = "CPU"},
                                                  new Types { TypesId = 100, TypeName = "Andet"});

            modelBuilder.Entity<Produkt>().HasData( new Produkt { ProduktId = 1, ProduktName = "4090 RTX Nvidia", Price = 10099, TypesId = 1, BrandId = 1 },
                                                    new Produkt { ProduktId = 2, ProduktName = "3090 RTX Nvida", Price = 15999, TypesId = 1, BrandId = 1 },
                                                    new Produkt { ProduktId = 3, ProduktName = "LogiTech Meistro Keyboard", Price = 1599, TypesId = 2, BrandId = 2 },
                                                    new Produkt { ProduktId = 4, ProduktName = "Asus Motherboard 3000x", Price = 2599, TypesId = 3, BrandId = 3 },
                                                    new Produkt { ProduktId = 5, ProduktName = "AMD ThredRipper 9999x", Price = 50000, TypesId = 4 , BrandId = 4 });;

            modelBuilder.Entity<User>().HasData( new User { UserId = 1, UserName = "René", Password ="1234", Email ="rene@email.dk", RoleId = 1, Address = "testvej 21", ZipCode = 2000},
                                                 new User { UserId = 2, UserName = "Benjamin", Password = "1234", Email = "Frederik@email.dk", RoleId = 2, Address = "testvej 11", ZipCode = 3000 },
                                                 new User { UserId = 3, UserName = "Frederik", Password = "1234", Email = "Benjamin@email.dk", RoleId = 3, Address = "testvej 11", ZipCode = 3000 });
           
            modelBuilder.Entity<Role>().HasData( new Role { RoleId = 1, RoleName = "Admin" },
                                                 new Role { RoleId = 2, RoleName = "SalesRep" },
                                                 new Role { RoleId = 3, RoleName = "Customer" });


        }
    }
}
