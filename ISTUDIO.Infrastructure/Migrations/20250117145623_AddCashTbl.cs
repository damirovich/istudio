using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCashTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 109, DateTimeKind.Utc).AddTicks(8753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 578, DateTimeKind.Utc).AddTicks(2065));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 126, DateTimeKind.Utc).AddTicks(9650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 585, DateTimeKind.Utc).AddTicks(8520));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 96, DateTimeKind.Utc).AddTicks(7682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 565, DateTimeKind.Utc).AddTicks(5227));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 96, DateTimeKind.Utc).AddTicks(1692),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 565, DateTimeKind.Utc).AddTicks(508));

            migrationBuilder.CreateTable(
                name: "CashbackTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashbackTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashbackTransactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCashbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCashbacks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashbackTransactions_Id",
                table: "CashbackTransactions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashbackTransactions_OrderId",
                table: "CashbackTransactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCashbacks_Id",
                table: "UserCashbacks",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashbackTransactions");

            migrationBuilder.DropTable(
                name: "UserCashbacks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 578, DateTimeKind.Utc).AddTicks(2065),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 109, DateTimeKind.Utc).AddTicks(8753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 585, DateTimeKind.Utc).AddTicks(8520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 126, DateTimeKind.Utc).AddTicks(9650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 565, DateTimeKind.Utc).AddTicks(5227),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 96, DateTimeKind.Utc).AddTicks(7682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 14, 13, 27, 35, 565, DateTimeKind.Utc).AddTicks(508),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 56, 20, 96, DateTimeKind.Utc).AddTicks(1692));
        }
    }
}
