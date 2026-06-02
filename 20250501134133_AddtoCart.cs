using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject_ECommerce.Migrations
{

    public partial class AddtoCart : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "tblCartItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblCartItem",
                table: "tblCartItem",
                column: "CartItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblCartItem",
                table: "tblCartItem");

            migrationBuilder.RenameTable(
                name: "tblCartItem",
                newName: "CartItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "CartItemId");
        }
    }
}
