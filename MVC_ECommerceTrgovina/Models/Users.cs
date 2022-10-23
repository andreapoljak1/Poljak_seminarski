using Microsoft.AspNetCore.Identity;
using MVC_ECommerceTrgovina.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ECommerceTrgovina.Models
{
    public class Users : ApplicationUser
    {
        [NotMapped]
        [DisplayName("Ime")]
        public string Name { get; set; }
        [NotMapped]
        [DisplayName("Tip korisnika")]
        public string Rola { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
       



    }
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

    }

}
