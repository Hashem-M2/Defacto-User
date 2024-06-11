using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class updateCategorymodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "mainGategory",
                table: "categories",
                newName: "subCategory");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "subCategory",
                table: "categories",
                newName: "mainGategory");
        }
    }
}
