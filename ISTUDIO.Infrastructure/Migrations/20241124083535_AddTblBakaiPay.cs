using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTblBakaiPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 568, DateTimeKind.Utc).AddTicks(5763),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 385, DateTimeKind.Utc).AddTicks(7661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 579, DateTimeKind.Utc).AddTicks(6156),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 394, DateTimeKind.Utc).AddTicks(5611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 551, DateTimeKind.Utc).AddTicks(4627),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 372, DateTimeKind.Utc).AddTicks(4713));

            migrationBuilder.AlterColumn<string>(
                name: "ResultUrl",
                table: "FreedomInitPayResponses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "RedirectUrlType",
                table: "FreedomInitPayResponses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RedirectUrl",
                table: "FreedomInitPayResponses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 543, DateTimeKind.Utc).AddTicks(7737),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 365, DateTimeKind.Utc).AddTicks(3203));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 543, DateTimeKind.Utc).AddTicks(954),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 364, DateTimeKind.Utc).AddTicks(8003));

            migrationBuilder.CreateTable(
                name: "BakaiCheckStatusResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTranId = table.Column<int>(type: "int", nullable: false),
                    PaymentCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ConfirmedAt = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ErrMsg = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakaiCheckStatusRes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakaiConfirmTranReq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTranId = table.Column<int>(type: "int", nullable: false),
                    OTPCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakaiConfirmTranReq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakaiConfirmTranRes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTranId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakaiConfirmTranRes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakaiCreateTranReq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakaiCreateTranReq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakaiCreateTranRes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreateId = table.Column<int>(type: "int", nullable: false),
                    PaymentCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakaiCreateTranRes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BakaiCheckStatusRes_Id",
                table: "BakaiCheckStatusResponse",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BakaiConfirmTranReq_Id",
                table: "BakaiConfirmTranReq",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BakaiConfirmTranRes_Id",
                table: "BakaiConfirmTranRes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BakaiCreateTranReq_Id",
                table: "BakaiCreateTranReq",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BakaiCreateTranRes_Id",
                table: "BakaiCreateTranRes",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BakaiCheckStatusResponse");

            migrationBuilder.DropTable(
                name: "BakaiConfirmTranReq");

            migrationBuilder.DropTable(
                name: "BakaiConfirmTranRes");

            migrationBuilder.DropTable(
                name: "BakaiCreateTranReq");

            migrationBuilder.DropTable(
                name: "BakaiCreateTranRes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 385, DateTimeKind.Utc).AddTicks(7661),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 568, DateTimeKind.Utc).AddTicks(5763));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 394, DateTimeKind.Utc).AddTicks(5611),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 579, DateTimeKind.Utc).AddTicks(6156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 372, DateTimeKind.Utc).AddTicks(4713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 551, DateTimeKind.Utc).AddTicks(4627));

            migrationBuilder.AlterColumn<string>(
                name: "ResultUrl",
                table: "FreedomInitPayResponses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedirectUrlType",
                table: "FreedomInitPayResponses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedirectUrl",
                table: "FreedomInitPayResponses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 365, DateTimeKind.Utc).AddTicks(3203),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 543, DateTimeKind.Utc).AddTicks(7737));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 10, 59, 364, DateTimeKind.Utc).AddTicks(8003),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 24, 8, 35, 31, 543, DateTimeKind.Utc).AddTicks(954));
        }
    }
}
