using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinancialAssetsApp.Migrations
{
    /// <inheritdoc />
    public partial class SplitStocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SumStocksToRuble",
                table: "Stocks");

            migrationBuilder.CreateTable(
                name: "StockUSD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ticker = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    NameCompany = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountStock = table.Column<int>(type: "integer", nullable: false),
                    SumStocks = table.Column<decimal>(type: "numeric", nullable: true),
                    SumStocksToRuble = table.Column<decimal>(type: "numeric", nullable: true),
                    DateAddStock = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockUSD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockUSD_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockUSD_UserId",
                table: "StockUSD",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockUSD");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Stocks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SumStocksToRuble",
                table: "Stocks",
                type: "numeric",
                nullable: true);
        }
    }
}
