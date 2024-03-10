using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DelTableSubCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubCategories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_Name",
                table: "SubCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_SubCategoryId",
                table: "SubCategories",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");
        }
    }
}
