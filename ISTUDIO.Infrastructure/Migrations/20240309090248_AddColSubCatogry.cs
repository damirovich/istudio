using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColSubCatogry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "SubCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_SubCategoryId",
                table: "SubCategories",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_SubCategories_SubCategoryId",
                table: "SubCategories",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_SubCategories_SubCategoryId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_SubCategoryId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "SubCategories");
        }
    }
}
