using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefODetailsMagazine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Magazines_MagazineId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MagazineId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 387, DateTimeKind.Utc).AddTicks(7045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 531, DateTimeKind.Utc).AddTicks(2322));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 391, DateTimeKind.Utc).AddTicks(1220),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 549, DateTimeKind.Utc).AddTicks(6127));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 382, DateTimeKind.Utc).AddTicks(6879),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 506, DateTimeKind.Utc).AddTicks(1185));

            migrationBuilder.AddColumn<int>(
                name: "MagazineId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 380, DateTimeKind.Utc).AddTicks(3279),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 499, DateTimeKind.Utc).AddTicks(3083));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 380, DateTimeKind.Utc).AddTicks(830),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 498, DateTimeKind.Utc).AddTicks(7344));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MagazineId",
                table: "OrderDetails",
                column: "MagazineId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Magazines_MagazineId",
                table: "OrderDetails",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Magazines_MagazineId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_MagazineId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 531, DateTimeKind.Utc).AddTicks(2322),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 387, DateTimeKind.Utc).AddTicks(7045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 549, DateTimeKind.Utc).AddTicks(6127),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 391, DateTimeKind.Utc).AddTicks(1220));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 506, DateTimeKind.Utc).AddTicks(1185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 382, DateTimeKind.Utc).AddTicks(6879));

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
                defaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 499, DateTimeKind.Utc).AddTicks(3083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 380, DateTimeKind.Utc).AddTicks(3279));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 29, 13, 30, 54, 498, DateTimeKind.Utc).AddTicks(7344),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 5, 15, 34, 42, 380, DateTimeKind.Utc).AddTicks(830));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MagazineId",
                table: "Orders",
                column: "MagazineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Magazines_MagazineId",
                table: "Orders",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");
        }
    }
}
