using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ds3.Migrations
{
    /// <inheritdoc />
    public partial class YeniUyeSistemi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bosslar",
                table: "Bosslar");

            migrationBuilder.RenameTable(
                name: "Bosslar",
                newName: "Boss");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boss",
                table: "Boss",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Boss",
                table: "Boss");

            migrationBuilder.RenameTable(
                name: "Boss",
                newName: "Bosslar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bosslar",
                table: "Bosslar",
                column: "Id");
        }
    }
}
