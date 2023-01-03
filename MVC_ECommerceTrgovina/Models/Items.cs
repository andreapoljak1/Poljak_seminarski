using MVC_ECommerceTrgovina.Data;
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
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
    
        [Column(TypeName = "nvarchar(500)")]
        public string? ImageName { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }

    }
}
