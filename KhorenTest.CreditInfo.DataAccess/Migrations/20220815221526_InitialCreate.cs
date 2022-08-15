using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KhorenTest.CreditInfo.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "National",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_National", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractCode = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfCreate = table.Column<DateTime>(nullable: false),
                    OrigialAmountValue = table.Column<decimal>(nullable: false),
                    OrigialAmountCurrencyId = table.Column<int>(nullable: false),
                    InstallmentAmountValue = table.Column<decimal>(nullable: false),
                    InstallmentAmountCurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ContractCode);
                    table.ForeignKey(
                        name: "FK_Contract_InstallmentCurrency",
                        column: x => x.InstallmentAmountCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_OrigialCurrency",
                        column: x => x.OrigialAmountCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerCode = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerCode);
                    table.ForeignKey(
                        name: "FK_Customer_National",
                        column: x => x.NationalId,
                        principalTable: "National",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_InstallmentAmountCurrencyId",
                table: "Contract",
                column: "InstallmentAmountCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_OrigialAmountCurrencyId",
                table: "Contract",
                column: "OrigialAmountCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_NationalId",
                table: "Customer",
                column: "NationalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "National");
        }
    }
}
