using Microsoft.EntityFrameworkCore.Migrations;

namespace PaniMusic.Core.Migrations
{
    public partial class addfavoritemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TrackId = table.Column<int>(type: "int", nullable: true),
                    AlbumId = table.Column<int>(type: "int", nullable: true),
                    MusicVideoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorite_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorite_MusicVideos_MusicVideoId",
                        column: x => x.MusicVideoId,
                        principalTable: "MusicVideos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorite_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_AlbumId",
                table: "Favorite",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_MusicVideoId",
                table: "Favorite",
                column: "MusicVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_TrackId",
                table: "Favorite",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_UserId",
                table: "Favorite",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorite");
        }
    }
}
