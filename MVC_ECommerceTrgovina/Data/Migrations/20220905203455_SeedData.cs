using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ECommerceTrgovina.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 1, "Instalacijski programi", "Programi" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 2, "Rabljeni mobiteli", "Mobiteli" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Description", "ImageName", "Price", "Quantity", "Title", "UserId" },
                values: new object[] { 1, 1, "2D & 3D CAD program - odlična alternativa", null, 7000.00m, 2m, "ZWCAD", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Description", "ImageName", "Price", "Quantity", "Title", "UserId" },
                values: new object[] { 2, 1, "2D & 3D CAD program", null, 25000.00m, 2m, "AutoCAD", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" });

        }

        
    }
}
