using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DelMagIdShopping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Magazines_MagazineId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_MagazineId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "ShoppingCarts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 98, DateTimeKind.Utc).AddTicks(5288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 387, DateTimeKind.Utc).AddTicks(7045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 107, DateTimeKind.Utc).AddTicks(3896),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 391, DateTimeKind.Utc).AddTicks(1220));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 87, DateTimeKind.Utc).AddTicks(1786),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 382, DateTimeKind.Utc).AddTicks(6879));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 81, DateTimeKind.Utc).AddTicks(8914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 380, DateTimeKind.Utc).AddTicks(3279));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 81, DateTimeKind.Utc).AddTicks(2750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 380, DateTimeKind.Utc).AddTicks(830));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MagazineId",
                table: "ShoppingCarts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 387, DateTimeKind.Utc).AddTicks(7045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 98, DateTimeKind.Utc).AddTicks(5288));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 391, DateTimeKind.Utc).AddTicks(1220),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 107, DateTimeKind.Utc).AddTicks(3896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 382, DateTimeKind.Utc).AddTicks(6879),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 87, DateTimeKind.Utc).AddTicks(1786));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 380, DateTimeKind.Utc).AddTicks(3279),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 81, DateTimeKind.Utc).AddTicks(8914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 380, DateTimeKind.Utc).AddTicks(830),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 81, DateTimeKind.Utc).AddTicks(2750));

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_MagazineId",
                table: "ShoppingCarts",
                column: "MagazineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Magazines_MagazineId",
                table: "ShoppingCarts",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");
        }
    }
}
