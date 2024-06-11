using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class addFluentAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_addresses_addressID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_addressID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_addresses_UserID",
                table: "addresses");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserID",
                table: "addresses",
                column: "UserID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_addresses_UserID",
                table: "addresses");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_addressID",
                table: "AspNetUsers",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserID",
                table: "addresses",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_addresses_addressID",
                table: "AspNetUsers",
                column: "addressID",
                principalTable: "addresses",
                principalColumn: "Id");
        }
    }
}
