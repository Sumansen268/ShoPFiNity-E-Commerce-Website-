using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject_ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class CartUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "tblCartItem",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_tblCartItem_ProductId",
                table: "tblCartItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCartItem_tblProducts_ProductId",
                table: "tblCartItem",
                column: "ProductId",
                principalTable: "tblProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCartItem_tblProducts_ProductId",
                table: "tblCartItem");

            migrationBuilder.DropIndex(
                name: "IX_tblCartItem_ProductId",
                table: "tblCartItem");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "tblCartItem",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
