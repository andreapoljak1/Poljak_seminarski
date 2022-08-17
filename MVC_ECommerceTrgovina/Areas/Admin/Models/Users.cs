using Microsoft.AspNetCore.Identity;
using MVC_ECommerceTrgovina.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ECommerceTrgovina.Areas.Admin.Models
{
    public class Users
    {
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Rola { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public int Id { get; set; }


    }
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
       
    }

}
