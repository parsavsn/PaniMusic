using Microsoft.EntityFrameworkCore.Migrations;

namespace PaniMusic.Core.Migrations
{
    public partial class Add_NewsLetter_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qulity480",
                table: "MusicVideos",
                newName: "Quality480");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quality480",
                table: "MusicVideos",
                newName: "Qulity480");
        }
    }
}
