using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAssetsApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteStartups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlatformsStartups_Users_UserId",
                table: "PlatformsStartups");

            migrationBuilder.DropForeignKey(
                name: "FK_Startups_PlatformsStartups_PlatformStartupId",
                table: "Startups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlatformsStartups",
                table: "PlatformsStartups");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Startups");

            migrationBuilder.RenameTable(
                name: "PlatformsStartups",
                newName: "PlatformStartups");

            migrationBuilder.RenameIndex(
                name: "IX_PlatformsStartups_UserId",
                table: "PlatformStartups",
                newName: "IX_PlatformStartups_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "PlatformStartupId",
                table: "Startups",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlatformStartups",
                table: "PlatformStartups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformStartups_Users_UserId",
                table: "PlatformStartups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Startups_PlatformStartups_PlatformStartupId",
                table: "Startups",
                column: "PlatformStartupId",
                principalTable: "PlatformStartups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlatformStartups_Users_UserId",
                table: "PlatformStartups");

            migrationBuilder.DropForeignKey(
                name: "FK_Startups_PlatformStartups_PlatformStartupId",
                table: "Startups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlatformStartups",
                table: "PlatformStartups");

            migrationBuilder.RenameTable(
                name: "PlatformStartups",
                newName: "PlatformsStartups");

            migrationBuilder.RenameIndex(
                name: "IX_PlatformStartups_UserId",
                table: "PlatformsStartups",
                newName: "IX_PlatformsStartups_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "PlatformStartupId",
                table: "Startups",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Startups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlatformsStartups",
                table: "PlatformsStartups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformsStartups_Users_UserId",
                table: "PlatformsStartups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Startups_PlatformsStartups_PlatformStartupId",
                table: "Startups",
                column: "PlatformStartupId",
                principalTable: "PlatformsStartups",
                principalColumn: "Id");
        }
    }
}
