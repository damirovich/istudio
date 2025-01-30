using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColOrderPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptPhoto",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 30, 14, 25, 5, 707, DateTimeKind.Utc).AddTicks(5349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 384, DateTimeKind.Utc).AddTicks(9792));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 30, 14, 25, 5, 691, DateTimeKind.Utc).AddTicks(8893),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 379, DateTimeKind.Utc).AddTicks(6998));

            migrationBuilder.AddColumn<string>(
                name: "ReceiptPhoto",
                table: "OrderPayments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 30, 14, 25, 5, 663, DateTimeKind.Utc).AddTicks(721),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 348, DateTimeKind.Utc).AddTicks(993));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 30, 14, 25, 5, 662, DateTimeKind.Utc).AddTicks(1209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 347, DateTimeKind.Utc).AddTicks(4022));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptPhoto",
                table: "OrderPayments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 384, DateTimeKind.Utc).AddTicks(9792),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 30, 14, 25, 5, 707, DateTimeKind.Utc).AddTicks(5349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 379, DateTimeKind.Utc).AddTicks(6998),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 30, 14, 25, 5, 691, DateTimeKind.Utc).AddTicks(8893));

            migrationBuilder.AddColumn<string>(
                name: "ReceiptPhoto",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 348, DateTimeKind.Utc).AddTicks(993),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 30, 14, 25, 5, 663, DateTimeKind.Utc).AddTicks(721));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 15, 31, 36, 347, DateTimeKind.Utc).AddTicks(4022),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 30, 14, 25, 5, 662, DateTimeKind.Utc).AddTicks(1209));
        }
    }
}
