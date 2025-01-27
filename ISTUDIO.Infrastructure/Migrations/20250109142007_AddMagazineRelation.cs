using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMagazineRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Magazines_MagazineId",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 250, DateTimeKind.Utc).AddTicks(8153),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 346, DateTimeKind.Utc).AddTicks(5490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 254, DateTimeKind.Utc).AddTicks(6639),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 356, DateTimeKind.Utc).AddTicks(7698));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 245, DateTimeKind.Utc).AddTicks(5878),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 332, DateTimeKind.Utc).AddTicks(6311));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 242, DateTimeKind.Utc).AddTicks(8474),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 325, DateTimeKind.Utc).AddTicks(574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 242, DateTimeKind.Utc).AddTicks(5978),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 324, DateTimeKind.Utc).AddTicks(1900));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Magazines_MagazineId",
                table: "Products",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Magazines_MagazineId",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 346, DateTimeKind.Utc).AddTicks(5490),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 250, DateTimeKind.Utc).AddTicks(8153));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 356, DateTimeKind.Utc).AddTicks(7698),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 254, DateTimeKind.Utc).AddTicks(6639));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 332, DateTimeKind.Utc).AddTicks(6311),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 245, DateTimeKind.Utc).AddTicks(5878));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 325, DateTimeKind.Utc).AddTicks(574),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 242, DateTimeKind.Utc).AddTicks(8474));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 28, 9, 23, 34, 324, DateTimeKind.Utc).AddTicks(1900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 9, 14, 20, 5, 242, DateTimeKind.Utc).AddTicks(5978));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Magazines_MagazineId",
                table: "Products",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");
        }
    }
}
