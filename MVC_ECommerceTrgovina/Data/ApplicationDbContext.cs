using Microsoft.AspNetCore.Identity;
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
               );


        }

      
    }
}