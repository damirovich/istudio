using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 12, 7, 43, 785, DateTimeKind.Utc).AddTicks(3725),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 29, 4, 9, 43, 923, DateTimeKind.Utc).AddTicks(4063));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 12, 7, 43, 780, DateTimeKind.Utc).AddTicks(1424),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 29, 4, 9, 43, 918, DateTimeKind.Utc).AddTicks(7389));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 9, 12, 7, 43, 777, DateTimeKind.Utc).AddTicks(7881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 12, 7, 43, 777, DateTimeKind.Utc).AddTicks(5434),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 29, 4, 9, 43, 916, DateTimeKind.Utc).AddTicks(4331));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CustomerImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 29, 4, 9, 43, 923, DateTimeKind.Utc).AddTicks(4063),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 12, 7, 43, 785, DateTimeKind.Utc).AddTicks(3725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 29, 4, 9, 43, 918, DateTimeKind.Utc).AddTicks(7389),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 12, 7, 43, 780, DateTimeKind.Utc).AddTicks(1424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 29, 4, 9, 43, 916, DateTimeKind.Utc).AddTicks(4331),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 12, 7, 43, 777, DateTimeKind.Utc).AddTicks(5434));
        }
    }
}
