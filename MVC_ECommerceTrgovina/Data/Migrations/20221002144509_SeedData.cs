using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ECommerceTrgovina.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            
            migrationBuilder.AlterColumn<string>(
                name: "ZIPCode",
                table: "AspNetUsers",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

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
                values: new object[,]
                {
                    { 1, 1, "2D & 3D CAD program - odlična alternativa", null, 7000.00m, 2m, "ZWCAD", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 2, 1, "2D & 3D CAD program", null, 25000.00m, 2m, "AutoCAD", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 3, 1, "Obrada podataka nakon terenskog dijela posla, pohranjivanje i statistika stanja.", "programming.jpg", 5000.00m, 2m, "Program za obradu podataka", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 4, 2, "Više starih mobitela, za dijelove...", "history_of_mobile_phones.jpg", 1000.00m, 1m, "Stari mobiteli", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 5, 2, "6,1-inčni (dijagonalno) OLED zaslon od ruba do ruba, Dvojna kamera od 12 MP: ultraširokokutna i širokokutna", null, 8000.00m, 1m, "Iphone 12", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 6, 2, "6,1-inčni (dijagonalno) OLED zaslon od ruba do ruba, Dvojna kamera od 12 MP: ultraširokokutna i širokokutna", "iphone_13_review_2.jpg", 10000.00m, 1m, "Iphone 13", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 7, 2, "6.7'' Dynamic AMOLED 2X (2640 x 1080),10MP Prednja kamera Glavni zaslon", "zflip.jpg", 9000.00m, 1m, "Samsung ZFlip", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 8, 1, "Besplatna Office365 usluga i alati koji omogućuju suradnju i komunikaciju između svih sudionika u obrazovnom sustavu.", "office365.png", 0.00m, 10m, "Office 365 za škole", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 9, 1, "Alati koji omogućuju suradnju i komunikaciju između svih sudionika", "office365Basic.png", 100.00m, 10m, "Microsoft 365 Business Basic", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" },
                    { 10, 1, "Alati koji omogućuju suradnju i komunikaciju između svih sudionika, sadrži alate Bussiness Basic i Bussines Start paketa", "office365Premium.png", 300.00m, 1m, "Microsoft 365 Business Premium", "ee36e8b3-229a-4024-9960-8d39f5bdcf48" }
                });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_UserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Category_CategoryId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Author");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Publisher");

            migrationBuilder.RenameIndex(
                name: "IX_Items_UserId",
                table: "Author",
                newName: "IX_Author_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryId",
                table: "Author",
                newName: "IX_Author_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ZIPCode",
                table: "AspNetUsers",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

           
          
        }
    }
}
