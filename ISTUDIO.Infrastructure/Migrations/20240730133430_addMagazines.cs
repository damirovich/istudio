using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addMagazines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 602, DateTimeKind.Utc).AddTicks(5290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 451, DateTimeKind.Utc).AddTicks(4609));

            migrationBuilder.AddColumn<int>(
                name: "MagazineId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 606, DateTimeKind.Utc).AddTicks(1903),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 455, DateTimeKind.Utc).AddTicks(4271));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 597, DateTimeKind.Utc).AddTicks(1415),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 445, DateTimeKind.Utc).AddTicks(8097));

            migrationBuilder.AddColumn<int>(
                name: "MagazineId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 594, DateTimeKind.Utc).AddTicks(7643),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 443, DateTimeKind.Utc).AddTicks(3556));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 594, DateTimeKind.Utc).AddTicks(5163),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 443, DateTimeKind.Utc).AddTicks(890));

            migrationBuilder.CreateTable(
                name: "MagazineEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoLogoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagazineEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_MagazineId",
                table: "ShoppingCarts",
                column: "MagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MagazineId",
                table: "Products",
                column: "MagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MagazineId",
                table: "Orders",
                column: "MagazineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MagazineEntity_MagazineId",
                table: "Orders",
                column: "MagazineId",
                principalTable: "MagazineEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MagazineEntity_MagazineId",
                table: "Products",
                column: "MagazineId",
                principalTable: "MagazineEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_MagazineEntity_MagazineId",
                table: "ShoppingCarts",
                column: "MagazineId",
                principalTable: "MagazineEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MagazineEntity_MagazineId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MagazineEntity_MagazineId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_MagazineEntity_MagazineId",
                table: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "MagazineEntity");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_MagazineId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_Products_MagazineId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MagazineId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 451, DateTimeKind.Utc).AddTicks(4609),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 602, DateTimeKind.Utc).AddTicks(5290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 455, DateTimeKind.Utc).AddTicks(4271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 606, DateTimeKind.Utc).AddTicks(1903));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 445, DateTimeKind.Utc).AddTicks(8097),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 597, DateTimeKind.Utc).AddTicks(1415));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 443, DateTimeKind.Utc).AddTicks(3556),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 594, DateTimeKind.Utc).AddTicks(7643));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 17, 12, 20, 31, 443, DateTimeKind.Utc).AddTicks(890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 594, DateTimeKind.Utc).AddTicks(5163));
        }
    }
}
