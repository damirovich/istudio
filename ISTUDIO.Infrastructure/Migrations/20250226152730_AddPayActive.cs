using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPayActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 26, 15, 27, 29, 975, DateTimeKind.Utc).AddTicks(4877),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 9, 47, 11, 899, DateTimeKind.Utc).AddTicks(6764));

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "PaymentMethods",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTechnicalBreak",
                table: "PaymentMethods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 26, 15, 27, 29, 972, DateTimeKind.Utc).AddTicks(8642),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 9, 47, 11, 897, DateTimeKind.Utc).AddTicks(3341));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 2, 26, 15, 27, 29, 961, DateTimeKind.Utc).AddTicks(5471),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 2, 23, 9, 47, 11, 887, DateTimeKind.Utc).AddTicks(4817));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 26, 15, 27, 29, 961, DateTimeKind.Utc).AddTicks(3109),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 9, 47, 11, 887, DateTimeKind.Utc).AddTicks(2398));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "IsTechnicalBreak",
                table: "PaymentMethods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 9, 47, 11, 899, DateTimeKind.Utc).AddTicks(6764),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 26, 15, 27, 29, 975, DateTimeKind.Utc).AddTicks(4877));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 9, 47, 11, 897, DateTimeKind.Utc).AddTicks(3341),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 26, 15, 27, 29, 972, DateTimeKind.Utc).AddTicks(8642));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 2, 23, 9, 47, 11, 887, DateTimeKind.Utc).AddTicks(4817),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 2, 26, 15, 27, 29, 961, DateTimeKind.Utc).AddTicks(5471));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 9, 47, 11, 887, DateTimeKind.Utc).AddTicks(2398),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 26, 15, 27, 29, 961, DateTimeKind.Utc).AddTicks(3109));
        }
    }
}
