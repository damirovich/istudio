using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCashProductTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 150, DateTimeKind.Utc).AddTicks(2975),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 109, DateTimeKind.Utc).AddTicks(8753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 145, DateTimeKind.Utc).AddTicks(2193),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 126, DateTimeKind.Utc).AddTicks(9650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 123, DateTimeKind.Utc).AddTicks(4224),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 96, DateTimeKind.Utc).AddTicks(7682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 122, DateTimeKind.Utc).AddTicks(6588),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 96, DateTimeKind.Utc).AddTicks(1692));

            migrationBuilder.CreateTable(
                name: "Cashbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashbackPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cashbacks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCashbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MaxBonusPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCashbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCashbacks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cashbacks_ProductId",
                table: "Cashbacks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCashbacks_ProductId",
                table: "ProductCashbacks",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cashbacks");

            migrationBuilder.DropTable(
                name: "ProductCashbacks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 109, DateTimeKind.Utc).AddTicks(8753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 150, DateTimeKind.Utc).AddTicks(2975));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 126, DateTimeKind.Utc).AddTicks(9650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 145, DateTimeKind.Utc).AddTicks(2193));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 96, DateTimeKind.Utc).AddTicks(7682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 123, DateTimeKind.Utc).AddTicks(4224));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 96, DateTimeKind.Utc).AddTicks(1692),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 20, 14, 46, 42, 122, DateTimeKind.Utc).AddTicks(6588));
        }
    }
}
