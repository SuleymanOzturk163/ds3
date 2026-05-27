using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ds3.Migrations
{
    /// <inheritdoc />
    public partial class RehberDetayEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UzunIcerik",
                table: "rehberim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "rehberim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UzunIcerik",
                table: "rehberim");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "rehberim");
        }
    }
}
