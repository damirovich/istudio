using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodeOTP",
                table: "AppUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeOTP",
                table: "AppUsers");
        }
    }
}
