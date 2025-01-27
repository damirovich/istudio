using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDlDn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentMethods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 578, DateTimeKind.Utc).AddTicks(2065),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 13, 14, 36, 4, 757, DateTimeKind.Utc).AddTicks(5311));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 585, DateTimeKind.Utc).AddTicks(8520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 13, 14, 36, 4, 769, DateTimeKind.Utc).AddTicks(5753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 565, DateTimeKind.Utc).AddTicks(5227),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 13, 14, 36, 4, 742, DateTimeKind.Utc).AddTicks(443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 565, DateTimeKind.Utc).AddTicks(508),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 13, 14, 36, 4, 741, DateTimeKind.Utc).AddTicks(4612));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7854d4909e2c42d69c91f0d1245e2dad",
                column: "Name",
                value: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 13, 14, 36, 4, 757, DateTimeKind.Utc).AddTicks(5311),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 578, DateTimeKind.Utc).AddTicks(2065));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 13, 14, 36, 4, 769, DateTimeKind.Utc).AddTicks(5753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 585, DateTimeKind.Utc).AddTicks(8520));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 13, 14, 36, 4, 742, DateTimeKind.Utc).AddTicks(443),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 565, DateTimeKind.Utc).AddTicks(5227));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 13, 14, 36, 4, 741, DateTimeKind.Utc).AddTicks(4612),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 565, DateTimeKind.Utc).AddTicks(508));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7854d4909e2c42d69c91f0d1245e2dad",
                column: "Name",
                value: null);
        }
    }
}
