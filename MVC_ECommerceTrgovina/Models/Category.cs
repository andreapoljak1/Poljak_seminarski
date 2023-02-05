using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ECommerceTrgovina.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [DisplayName("Naziv")]
        public string Title { get; set; }
        [DisplayName("Opis")]
        public string? Description { get; set; }
        [NotMapped]
        public ICollection<Items>? Item { get; set; }
    }
}
