using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAssetsApp.Migrations
{
    /// <inheritdoc />
    public partial class newfieldinStartups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Startups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlatformStartupId",
                table: "Startups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Startups_PlatformStartupId",
                table: "Startups",
                column: "PlatformStartupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Startups_PlatformsStartups_PlatformStartupId",
                table: "Startups",
                column: "PlatformStartupId",
                principalTable: "PlatformsStartups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Startups_PlatformsStartups_PlatformStartupId",
                table: "Startups");

            migrationBuilder.DropIndex(
                name: "IX_Startups_PlatformStartupId",
                table: "Startups");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Startups");

            migrationBuilder.DropColumn(
                name: "PlatformStartupId",
                table: "Startups");
        }
    }
}
