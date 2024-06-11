using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class changeAdressRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_AspNetUsers_UserID",
                table: "addresses");

            migrationBuilder.DropIndex(
                name: "IX_addresses_UserID",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "addresses");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_addressID",
                table: "AspNetUsers",
                column: "addressID");

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_addresses_addressID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_addressID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "addresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserID",
                table: "addresses",
                column: "UserID",
                unique: true);

       
        }
    }
}
