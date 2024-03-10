using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTUDIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNikita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmsNikitaRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Test = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsNikitaRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsNikitaStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsNikitaStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsNikitaResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phones = table.Column<int>(type: "int", nullable: true),
                    SmsCount = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmsRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SmsStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsNikitaResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsNikitaResponses_SmsNikitaRequests_SmsRequestId",
                        column: x => x.SmsRequestId,
                        principalTable: "SmsNikitaRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmsNikitaResponses_SmsNikitaStatuses_SmsStatusId",
                        column: x => x.SmsStatusId,
                        principalTable: "SmsNikitaStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmsNikitaResponses_SmsRequestId",
                table: "SmsNikitaResponses",
                column: "SmsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsNikitaResponses_SmsStatusId",
                table: "SmsNikitaResponses",
                column: "SmsStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmsNikitaResponses");

            migrationBuilder.DropTable(
                name: "SmsNikitaRequests");

            migrationBuilder.DropTable(
                name: "SmsNikitaStatuses");
        }
    }
}
