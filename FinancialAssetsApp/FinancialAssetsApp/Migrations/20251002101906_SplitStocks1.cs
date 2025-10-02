using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAssetsApp.Migrations
{
    /// <inheritdoc />
    public partial class SplitStocks1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockUSD_Users_UserId",
                table: "StockUSD");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockUSD",
                table: "StockUSD");

            migrationBuilder.RenameTable(
                name: "StockUSD",
                newName: "StocksUSD");

            migrationBuilder.RenameIndex(
                name: "IX_StockUSD_UserId",
                table: "StocksUSD",
                newName: "IX_StocksUSD_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StocksUSD",
                table: "StocksUSD",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StocksUSD_Users_UserId",
                table: "StocksUSD",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StocksUSD_Users_UserId",
                table: "StocksUSD");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StocksUSD",
                table: "StocksUSD");

            migrationBuilder.RenameTable(
                name: "StocksUSD",
                newName: "StockUSD");

            migrationBuilder.RenameIndex(
                name: "IX_StocksUSD_UserId",
                table: "StockUSD",
                newName: "IX_StockUSD_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockUSD",
                table: "StockUSD",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockUSD_Users_UserId",
                table: "StockUSD",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
