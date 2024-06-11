using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class changeRelationAddressAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "addressID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_addressID",
                table: "AspNetUsers",
                column: "addressID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_addresses_addressID",
                table: "AspNetUsers",
                column: "addressID",
                principalTable: "addresses",
                principalColumn: "Id");
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

            migrationBuilder.DropColumn(
                name: "addressID",
                table: "AspNetUsers");
        }
    }
}
