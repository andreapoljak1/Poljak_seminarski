using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ECommerceTrgovina.Areas.Admin.Models
{
     public class Users
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string NormalizedUserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string NormalizedEmail { get; set; }
    
        public bool EmailConfirmed { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string SecurityStamp { get; set; }
        [Required]
        public string ConcurrencyStamp { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime LockoutEnd { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Name { get; set; }
        public string? ZIPCode { get; set; }
   
        [Column(TypeName = "nvarchar(500)")]
        public string? ImageName { get; set; }
        public string Rola { get; set; }
        public string RoleId { get; set; }
        

    }
}
