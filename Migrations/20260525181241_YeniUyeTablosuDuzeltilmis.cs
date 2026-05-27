using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ds3.Migrations
{
    /// <inheritdoc />
    public partial class YeniUyeTablosuDuzeltilmis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Uyeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uyeler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uyeler");

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
    }
}
