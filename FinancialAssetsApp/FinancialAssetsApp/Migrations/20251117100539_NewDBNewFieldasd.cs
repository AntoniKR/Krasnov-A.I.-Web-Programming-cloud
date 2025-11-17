using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAssetsApp.Migrations
{
    /// <inheritdoc />
    public partial class NewDBNewFieldasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameEstate",
                table: "RealEstates",
                newName: "TypeEstate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeEstate",
                table: "RealEstates",
                newName: "NameEstate");
        }
    }
}
