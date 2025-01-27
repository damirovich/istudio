using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCashDelCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cashbacks_Products_ProductId",
                table: "Cashbacks");

            migrationBuilder.DropIndex(
                name: "IX_Cashbacks_ProductId",
                table: "Cashbacks");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Cashbacks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 384, DateTimeKind.Utc).AddTicks(9792),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 150, DateTimeKind.Utc).AddTicks(2975));

            migrationBuilder.AddColumn<int>(
                name: "CashbackId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 379, DateTimeKind.Utc).AddTicks(6998),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 145, DateTimeKind.Utc).AddTicks(2193));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 348, DateTimeKind.Utc).AddTicks(993),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 123, DateTimeKind.Utc).AddTicks(4224));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 347, DateTimeKind.Utc).AddTicks(4022),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 122, DateTimeKind.Utc).AddTicks(6588));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CashbackId",
                table: "Products",
                column: "CashbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Cashbacks_CashbackId",
                table: "Products",
                column: "CashbackId",
                principalTable: "Cashbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Cashbacks_CashbackId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CashbackId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CashbackId",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 150, DateTimeKind.Utc).AddTicks(2975),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 384, DateTimeKind.Utc).AddTicks(9792));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 145, DateTimeKind.Utc).AddTicks(2193),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 379, DateTimeKind.Utc).AddTicks(6998));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 123, DateTimeKind.Utc).AddTicks(4224),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 348, DateTimeKind.Utc).AddTicks(993));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Cashbacks",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 122, DateTimeKind.Utc).AddTicks(6588),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 347, DateTimeKind.Utc).AddTicks(4022));

            migrationBuilder.CreateIndex(
                name: "IX_Cashbacks_ProductId",
                table: "Cashbacks",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cashbacks_Products_ProductId",
                table: "Cashbacks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
