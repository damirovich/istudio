using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addMagazinesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_MagazineEntity",
                table: "MagazineEntity");

            migrationBuilder.RenameTable(
                name: "MagazineEntity",
                newName: "Magazines");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 382, DateTimeKind.Utc).AddTicks(4618),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 602, DateTimeKind.Utc).AddTicks(5290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 385, DateTimeKind.Utc).AddTicks(8592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 606, DateTimeKind.Utc).AddTicks(1903));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 377, DateTimeKind.Utc).AddTicks(7191),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 597, DateTimeKind.Utc).AddTicks(1415));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 375, DateTimeKind.Utc).AddTicks(6165),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 594, DateTimeKind.Utc).AddTicks(7643));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 375, DateTimeKind.Utc).AddTicks(3955),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 594, DateTimeKind.Utc).AddTicks(5163));

            migrationBuilder.AlterColumn<string>(
                name: "PhotoLogoURL",
                table: "Magazines",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Magazines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Magazines",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Magazines",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Magazines",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Magazines",
                table: "Magazines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Magazines_Name",
                table: "Magazines",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Magazines_MagazineId",
                table: "Orders",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Magazines_MagazineId",
                table: "Products",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Magazines_MagazineId",
                table: "ShoppingCarts",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Magazines_MagazineId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Magazines_MagazineId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Magazines_MagazineId",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Magazines",
                table: "Magazines");

            migrationBuilder.DropIndex(
                name: "IX_Magazines_Name",
                table: "Magazines");

            migrationBuilder.RenameTable(
                name: "Magazines",
                newName: "MagazineEntity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 602, DateTimeKind.Utc).AddTicks(5290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 382, DateTimeKind.Utc).AddTicks(4618));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 606, DateTimeKind.Utc).AddTicks(1903),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 385, DateTimeKind.Utc).AddTicks(8592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 597, DateTimeKind.Utc).AddTicks(1415),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 377, DateTimeKind.Utc).AddTicks(7191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 594, DateTimeKind.Utc).AddTicks(7643),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 375, DateTimeKind.Utc).AddTicks(6165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 13, 34, 29, 594, DateTimeKind.Utc).AddTicks(5163),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 13, 37, 32, 375, DateTimeKind.Utc).AddTicks(3955));

            migrationBuilder.AlterColumn<string>(
                name: "PhotoLogoURL",
                table: "MagazineEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "MagazineEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MagazineEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MagazineEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "MagazineEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MagazineEntity",
                table: "MagazineEntity",
                column: "Id");

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
    }
}
