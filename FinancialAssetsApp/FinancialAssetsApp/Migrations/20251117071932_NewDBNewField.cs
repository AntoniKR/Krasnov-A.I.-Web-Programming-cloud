using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAssetsApp.Migrations
{
    /// <inheritdoc />
    public partial class NewDBNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityEstate",
                table: "RealEstates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityEstate",
                table: "RealEstates");
        }
    }
}
