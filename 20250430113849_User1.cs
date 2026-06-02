using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject_ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class User1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tblAdmin",
                columns: new[] { "Email", "Password" },
                values: new object[] { "sumansen268@gmail.com", "Suman@123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblAdmin",
                keyColumn: "Email",
                keyValue: "sumansen268@gmail.com");
        }
    }
}
