using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColJsonInitReqFreedomPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PgAmount",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgAuthCode",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgCanReject",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgCaptured",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgCardPan",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgCurrency",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgDescription",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgNeedEmailNotification",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgNeedPhoneNotification",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgNetAmount",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgOrderId",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgPaymentDate",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgPaymentId",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgPaymentMethod",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgPsAmount",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgPsCurrency",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgPsFullAmount",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgReference",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgResult",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgSalt",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgSig",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgTestingMode",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgUserContactEmail",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "PgUserPhone",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FreedomInitPayRequests");

            migrationBuilder.DropColumn(
                name: "PgAmount",
                table: "FreedomInitPayRequests");

            migrationBuilder.DropColumn(
                name: "PgDescription",
                table: "FreedomInitPayRequests");

            migrationBuilder.DropColumn(
                name: "PgMerchantId",
                table: "FreedomInitPayRequests");

            migrationBuilder.DropColumn(
                name: "PgOrderId",
                table: "FreedomInitPayRequests");

            migrationBuilder.DropColumn(
                name: "PgSalt",
                table: "FreedomInitPayRequests");

            migrationBuilder.DropColumn(
                name: "PgSig",
                table: "FreedomInitPayRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 394, DateTimeKind.Utc).AddTicks(4984),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 850, DateTimeKind.Utc).AddTicks(320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 398, DateTimeKind.Utc).AddTicks(671),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 853, DateTimeKind.Utc).AddTicks(3912));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 389, DateTimeKind.Utc).AddTicks(4281),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 845, DateTimeKind.Utc).AddTicks(3580));

            migrationBuilder.AddColumn<string>(
                name: "JsonData",
                table: "FreedomPayResultRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JsonData",
                table: "FreedomInitPayRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 386, DateTimeKind.Utc).AddTicks(6414),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 842, DateTimeKind.Utc).AddTicks(8745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 386, DateTimeKind.Utc).AddTicks(3406),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 842, DateTimeKind.Utc).AddTicks(6513));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonData",
                table: "FreedomPayResultRequests");

            migrationBuilder.DropColumn(
                name: "JsonData",
                table: "FreedomInitPayRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 850, DateTimeKind.Utc).AddTicks(320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 394, DateTimeKind.Utc).AddTicks(4984));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "OrderStatusHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 853, DateTimeKind.Utc).AddTicks(3912),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 398, DateTimeKind.Utc).AddTicks(671));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 845, DateTimeKind.Utc).AddTicks(3580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 389, DateTimeKind.Utc).AddTicks(4281));

            migrationBuilder.AddColumn<decimal>(
                name: "PgAmount",
                table: "FreedomPayResultRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PgAuthCode",
                table: "FreedomPayResultRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PgCanReject",
                table: "FreedomPayResultRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PgCaptured",
                table: "FreedomPayResultRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PgCardPan",
                table: "FreedomPayResultRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PgCurrency",
                table: "FreedomPayResultRequests",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PgDescription",
                table: "FreedomPayResultRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "PgNeedEmailNotification",
                table: "FreedomPayResultRequests",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "PgNeedPhoneNotification",
                table: "FreedomPayResultRequests",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<decimal>(
                name: "PgNetAmount",
                table: "FreedomPayResultRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PgOrderId",
                table: "FreedomPayResultRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PgPaymentDate",
                table: "FreedomPayResultRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PgPaymentId",
                table: "FreedomPayResultRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PgPaymentMethod",
                table: "FreedomPayResultRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PgPsAmount",
                table: "FreedomPayResultRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PgPsCurrency",
                table: "FreedomPayResultRequests",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PgPsFullAmount",
                table: "FreedomPayResultRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PgReference",
                table: "FreedomPayResultRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PgResult",
                table: "FreedomPayResultRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PgSalt",
                table: "FreedomPayResultRequests",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PgSig",
                table: "FreedomPayResultRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PgTestingMode",
                table: "FreedomPayResultRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PgUserContactEmail",
                table: "FreedomPayResultRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PgUserPhone",
                table: "FreedomPayResultRequests",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FreedomInitPayRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PgAmount",
                table: "FreedomInitPayRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PgDescription",
                table: "FreedomInitPayRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PgMerchantId",
                table: "FreedomInitPayRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PgOrderId",
                table: "FreedomInitPayRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PgSalt",
                table: "FreedomInitPayRequests",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PgSig",
                table: "FreedomInitPayRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerImages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 842, DateTimeKind.Utc).AddTicks(8745),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 386, DateTimeKind.Utc).AddTicks(6414));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 11, 14, 55, 15, 842, DateTimeKind.Utc).AddTicks(6513),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 13, 49, 12, 386, DateTimeKind.Utc).AddTicks(3406));
        }
    }
}
