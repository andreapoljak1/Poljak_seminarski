﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_ECommerceTrgovina.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVC_ECommerceTrgovina.Data;

namespace MVC_ECommerceTrgovina.Data
{
    // Ručno dodana klasa za ubacivanje prilagođenih svojstava u tablicu AspNetUsers
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(5)]
        public string ZIPCode { get; set; }

        [StringLength(150)]
        public string City { get; set; }

        [StringLength(150)]
        public string Country { get; set; }
       
        [Column(TypeName = "nvarchar(500)")]
        public string? ImageName { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Items> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=MVCSeminarECommerce; Trusted_Connection=True; MultipleActiveResultSets=true; TrustServerCertificate = true;");
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder
                .Entity<Items>()
                .HasOne<Category>(o => o.Category)
                .WithMany(e => e.Item)
                .HasForeignKey(o => o.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            //Seedanje podataka
            builder.Entity<Category>().HasData(
                  new Category
                  {
                      Id = 1,
                      Title = "Programi",
                      Description = "Instalacijski programi",

                  },
                   new Category
                   {
                       Id = 2,
                       Title = "Mobiteli",
                       Description = "Rabljeni mobiteli",

                   }
              );

            builder.Entity<Items>().HasData(
                   new Items
                   {
                       Id = 1,
                       Title = "ZWCAD",
                       Description = "2D & 3D CAD program - odlična alternativa",
                       Quantity = 2,
                       Price=7000.00M,
                       ImageName =null,
                       CategoryId=1,
                       UserId= "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                   },
                    new Items
                    {
                        Id = 2,
                        Title = "AutoCAD",
                        Description = "2D & 3D CAD program",
                        Quantity = 2,
                        Price = 25000.00M,
                        ImageName = null,
                        CategoryId = 1,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
                    ,
                    new Items
                    {
                        Id = 3,
                        Title = "Program za obradu podataka",
                        Description = "Obrada podataka nakon terenskog dijela posla, pohranjivanje i statistika stanja.",
                        Quantity = 2,
                        Price = 5000.00M,
                        ImageName = "programming.jpg",
                        CategoryId = 1,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
                    ,
                    new Items
                    {
                        Id = 4,
                        Title = "Stari mobiteli",
                        Description = "Više starih mobitela, za dijelove...",
                        Quantity = 1,
                        Price = 1000.00M,
                        ImageName = "history_of_mobile_phones.jpg",
                        CategoryId = 2,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
                    ,
                    new Items
                    {
                        Id = 5,
                        Title = "Iphone 12",
                        Description = "6,1-inčni (dijagonalno) OLED zaslon od ruba do ruba, Dvojna kamera od 12 MP: ultraširokokutna i širokokutna",
                        Quantity = 1,
                        Price = 8000.00M,
                        ImageName = null,
                        CategoryId = 2,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
                    ,
                    new Items
                    {
                        Id = 6,
                        Title = "Iphone 13",
                        Description = "6,1-inčni (dijagonalno) OLED zaslon od ruba do ruba, Dvojna kamera od 12 MP: ultraširokokutna i širokokutna",
                        Quantity = 1,
                        Price = 10000.00M,
                        ImageName = "iphone_13_review_2.jpg",
                        CategoryId = 2,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
                    ,
                    new Items
                    {
                        Id = 7,
                        Title = "Samsung ZFlip",
                        Description = "6.7'' Dynamic AMOLED 2X (2640 x 1080),10MP Prednja kamera Glavni zaslon",
                        Quantity = 1,
                        Price = 9000.00M,
                        ImageName = "zflip.jpg",
                        CategoryId = 2,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
                     ,
                    new Items
                    {
                        Id = 8,
                        Title = "Office 365 za škole",
                        Description = "Besplatna Office365 usluga i alati koji omogućuju suradnju i komunikaciju između svih sudionika u obrazovnom sustavu.",
                        Quantity = 10,
                        Price = 0.00M,
                        ImageName = "office365.png",
                        CategoryId = 1,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
                     ,
                    new Items
                    {
                        Id = 9,
                        Title = "Microsoft 365 Business Basic",
                        Description = "Alati koji omogućuju suradnju i komunikaciju između svih sudionika",
                        Quantity = 10,
                        Price = 100.00M,
                        ImageName = "office365Basic.png",
                        CategoryId = 1,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
                     ,
                    new Items
                    {
                        Id = 10,
                        Title = "Microsoft 365 Business Premium",
                        Description = "Alati koji omogućuju suradnju i komunikaciju između svih sudionika, sadrži alate Bussiness Basic i Bussines Start paketa",
                        Quantity = 1,
                        Price = 300.00M,
                        ImageName = "office365Premium.png",
                        CategoryId = 1,
                        UserId = "ee36e8b3-229a-4024-9960-8d39f5bdcf48"

                    }
               );


        }



    }
}