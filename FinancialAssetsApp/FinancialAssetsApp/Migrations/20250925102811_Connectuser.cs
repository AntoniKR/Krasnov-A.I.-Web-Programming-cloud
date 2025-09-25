using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAssetsApp.Migrations
{
    /// <inheritdoc />
    public partial class Connectuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Users_UserId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_UserId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stocks");
        }
    }
}
