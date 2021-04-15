using Microsoft.EntityFrameworkCore.Migrations;

namespace PaniMusic.Core.Migrations
{
    public partial class addfinalyfavoritemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.AddColumn<int>(
                name: "MusicVideoId",
                table: "MusicVideos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoriteAlbums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteAlbums_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteMusicVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MusicVideoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMusicVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteMusicVideos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteMusicVideos_MusicVideos_MusicVideoId",
                        column: x => x.MusicVideoId,
                        principalTable: "MusicVideos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteTracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteTracks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteTracks_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicVideos_MusicVideoId",
                table: "MusicVideos",
                column: "MusicVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAlbums_AlbumId",
                table: "FavoriteAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAlbums_UserId",
                table: "FavoriteAlbums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMusicVideos_MusicVideoId",
                table: "FavoriteMusicVideos",
                column: "MusicVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMusicVideos_UserId",
                table: "FavoriteMusicVideos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTracks_TrackId",
                table: "FavoriteTracks",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTracks_UserId",
                table: "FavoriteTracks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicVideos_MusicVideos_MusicVideoId",
                table: "MusicVideos",
                column: "MusicVideoId",
                principalTable: "MusicVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicVideos_MusicVideos_MusicVideoId",
                table: "MusicVideos");

            migrationBuilder.DropTable(
                name: "FavoriteAlbums");

            migrationBuilder.DropTable(
                name: "FavoriteMusicVideos");

            migrationBuilder.DropTable(
                name: "FavoriteTracks");

            migrationBuilder.DropIndex(
                name: "IX_MusicVideos_MusicVideoId",
                table: "MusicVideos");

            migrationBuilder.DropColumn(
                name: "MusicVideoId",
                table: "MusicVideos");

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(type: "int", nullable: true),
                    MusicVideoId = table.Column<int>(type: "int", nullable: true),
                    TrackId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorites_MusicVideos_MusicVideoId",
                        column: x => x.MusicVideoId,
                        principalTable: "MusicVideos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorites_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AlbumId",
                table: "Favorites",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_MusicVideoId",
                table: "Favorites",
                column: "MusicVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_TrackId",
                table: "Favorites",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");
        }
    }
}
