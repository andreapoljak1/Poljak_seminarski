using MVC_ECommerceTrgovina.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ECommerceTrgovina.Models
{
    public class Items
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [DisplayName("Naziv")]
        public string Title { get; set; }
        [DisplayName("Opis")]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        [DisplayName("Količina")]
        public decimal Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(9,2)")]
        [DisplayName("Cijena")]
        public decimal Price { get; set; }
    
        [Column(TypeName = "nvarchar(500)")]
        [DisplayName("Slika")]
        public string? ImageName { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [DisplayName("Kategorija")]
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        [NotMapped]
        [DisplayName("Kategorija")]
        public string? CategoryName { get; set; }

    }
}
