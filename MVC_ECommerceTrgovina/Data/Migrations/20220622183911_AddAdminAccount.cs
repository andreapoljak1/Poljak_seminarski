using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace MVC_ECommerceTrgovina.Data.Migrations
{
    public partial class AddAdminAccount : Migration
    {
        const string ADMIN_USER_GUID = "ee36e8b3-229a-4024-9960-8d39f5bdcf48";
        const string ADMIN_ROLE_GUID = "8a23fc46-03e8-45c2-bc7b-a6ced2d367c3";

        const string UREDNIK_ROLE_GUID = "fc6e60a5-f7a6-4b0c-ba79-5a792cec43ad";
        const string KORISNIK_ROLE_GUID = "e76a81dd-cdb5-41b7-ab2f-0aa588843f0b";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            // Lozinka za admin korisnički račun
            var passwordHash = hasher.HashPassword(null, "Password12345"); // Napomena: U praksi postoje bolji načini da se zaštiti ova lozinka

            StringBuilder sb = new StringBuilder();

            // Kreiranje INSERT INTO querya
            sb.AppendLine("INSERT INTO AspNetUsers(Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled," +
                "LockoutEnd,LockoutEnabled,AccessFailedCount,Address,City,Country,Discriminator,FirstName,Image,LastName,ZIPCode)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{ADMIN_USER_GUID}'");//Id
            sb.AppendLine(",'admin@admin.com'");//UserName
            sb.AppendLine(",'ADMIN@ADMIN.COM'");//NormalizedUserName
            sb.AppendLine(",'admin@admin.com'");//Email
            sb.AppendLine(",'admin@admin.com'");//NormalizedEmail
            sb.AppendLine(",0");//EmailConfirmed
            sb.AppendLine($", '{passwordHash}'");//PasswordHash
            sb.AppendLine(", ''");//SecurityStamp
            sb.AppendLine(", ''");//ConcurrencyStamp
            sb.AppendLine(", 0");//PhoneNumber
            sb.AppendLine(", 0");//PhoneNumberConfirmed
            sb.AppendLine(", 0");//TwoFactorEnabled
            sb.AppendLine(", ''");//LockoutEnd
            sb.AppendLine(", 0");//LockoutEnabled
            sb.AppendLine(", 0");//AccessFailedCount
            sb.AppendLine(", ''");//Address
            sb.AppendLine(", ''");//City
            sb.AppendLine(", ''");//Country
            sb.AppendLine(", ''");//Discriminator
            sb.AppendLine(",'Admin'");//FirstName
            sb.AppendLine(", ''");//Image
            sb.AppendLine(", ''");//LastName
            sb.AppendLine(", ''");//ZIPCode
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}','Admin','ADMIN')");

            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}','{ADMIN_ROLE_GUID}')");

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{UREDNIK_ROLE_GUID}','Urednik','UREDNIK')");
            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{KORISNIK_ROLE_GUID}','Korisnik','KORISNIK')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{ADMIN_USER_GUID}' AND RoleId = '{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{ADMIN_USER_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ADMIN_ROLE_GUID}'");
        }
    }
}
