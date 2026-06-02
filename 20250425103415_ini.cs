using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebProject_ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "tblProducts");

            migrationBuilder.InsertData(
                table: "tblAdmin",
                columns: new[] { "Email", "Password" },
                values: new object[] { "sumansen@gmail.com", "Suman@123" });

            migrationBuilder.InsertData(
                table: "tblCategory",
                columns: new[] { "CatId", "CatName" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Clothing" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblAdmin",
                keyColumn: "Email",
                keyValue: "sumansen@gmail.com");

            migrationBuilder.DeleteData(
                table: "tblCategory",
                keyColumn: "CatId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tblCategory",
                keyColumn: "CatId",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "tblProducts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
