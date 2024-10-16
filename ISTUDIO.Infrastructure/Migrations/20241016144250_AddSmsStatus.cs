using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSmsStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StatusSendSMS",
                table: "SmsNikitaRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 129, DateTimeKind.Utc).AddTicks(7776),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 98, DateTimeKind.Utc).AddTicks(5288));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 137, DateTimeKind.Utc).AddTicks(5018),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 107, DateTimeKind.Utc).AddTicks(3896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 119, DateTimeKind.Utc).AddTicks(23),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 87, DateTimeKind.Utc).AddTicks(1786));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 113, DateTimeKind.Utc).AddTicks(6957),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 81, DateTimeKind.Utc).AddTicks(8914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 113, DateTimeKind.Utc).AddTicks(1441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 81, DateTimeKind.Utc).AddTicks(2750));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusSendSMS",
                table: "SmsNikitaRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 98, DateTimeKind.Utc).AddTicks(5288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 129, DateTimeKind.Utc).AddTicks(7776));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 107, DateTimeKind.Utc).AddTicks(3896),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 137, DateTimeKind.Utc).AddTicks(5018));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 87, DateTimeKind.Utc).AddTicks(1786),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 119, DateTimeKind.Utc).AddTicks(23));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 81, DateTimeKind.Utc).AddTicks(8914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 113, DateTimeKind.Utc).AddTicks(6957));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 9, 54, 5, 81, DateTimeKind.Utc).AddTicks(2750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 16, 14, 42, 47, 113, DateTimeKind.Utc).AddTicks(1441));
        }
    }
}
