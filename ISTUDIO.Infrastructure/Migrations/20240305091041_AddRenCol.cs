using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRenCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phones",
                table: "SmsNikitaRequests");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "SmsNikitaRequests");

            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "SmsNikitaRequests",
                newName: "SenderCompany");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "SmsNikitaRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Test",
                table: "SmsNikitaRequests",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhonesNumber",
                table: "SmsNikitaRequests",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextSms",
                table: "SmsNikitaRequests",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhonesNumber",
                table: "SmsNikitaRequests");

            migrationBuilder.DropColumn(
                name: "TextSms",
                table: "SmsNikitaRequests");

            migrationBuilder.RenameColumn(
                name: "SenderCompany",
                table: "SmsNikitaRequests",
                newName: "Sender");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "SmsNikitaRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Test",
                table: "SmsNikitaRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phones",
                table: "SmsNikitaRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "SmsNikitaRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
