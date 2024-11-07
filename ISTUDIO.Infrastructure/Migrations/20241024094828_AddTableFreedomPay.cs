using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableFreedomPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 376, DateTimeKind.Utc).AddTicks(741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 290, DateTimeKind.Utc).AddTicks(2774));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 381, DateTimeKind.Utc).AddTicks(5854),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 298, DateTimeKind.Utc).AddTicks(2676));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 368, DateTimeKind.Utc).AddTicks(4071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 279, DateTimeKind.Utc).AddTicks(4915));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 364, DateTimeKind.Utc).AddTicks(4155),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 273, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 364, DateTimeKind.Utc).AddTicks(370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 272, DateTimeKind.Utc).AddTicks(7442));

            migrationBuilder.CreateTable(
                name: "FreedomInitPayRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PgOrderId = table.Column<int>(type: "int", nullable: false),
                    PgMerchantId = table.Column<int>(type: "int", nullable: false),
                    PgAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PgDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PgSalt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PgSig = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreedomInitPayRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FreedomInitPayResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RedirectUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RedirectUrlType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sig = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ResultUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreedomInitPayResponses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FreedomPayResultRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PgOrderId = table.Column<int>(type: "int", nullable: false),
                    PgPaymentId = table.Column<int>(type: "int", nullable: false),
                    PgAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PgCurrency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PgNetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PgPsAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PgPsFullAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PgPsCurrency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PgDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PgResult = table.Column<int>(type: "int", nullable: false),
                    PgPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PgCanReject = table.Column<int>(type: "int", nullable: false),
                    PgUserPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PgUserContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PgTestingMode = table.Column<int>(type: "int", nullable: false),
                    PgCaptured = table.Column<int>(type: "int", nullable: false),
                    PgReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PgCardPan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PgSalt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PgSig = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PgPaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreedomPayResultRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FreedomPayResultResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Sig = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreedomPayResultResponses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreedomInitPayRequests");

            migrationBuilder.DropTable(
                name: "FreedomInitPayResponses");

            migrationBuilder.DropTable(
                name: "FreedomPayResultRequests");

            migrationBuilder.DropTable(
                name: "FreedomPayResultResponses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 290, DateTimeKind.Utc).AddTicks(2774),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 376, DateTimeKind.Utc).AddTicks(741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 298, DateTimeKind.Utc).AddTicks(2676),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 381, DateTimeKind.Utc).AddTicks(5854));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 279, DateTimeKind.Utc).AddTicks(4915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 368, DateTimeKind.Utc).AddTicks(4071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 273, DateTimeKind.Utc).AddTicks(7465),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 364, DateTimeKind.Utc).AddTicks(4155));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 20, 7, 49, 36, 272, DateTimeKind.Utc).AddTicks(7442),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 24, 9, 48, 26, 364, DateTimeKind.Utc).AddTicks(370));
        }
    }
}
