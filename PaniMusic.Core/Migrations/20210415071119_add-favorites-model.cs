using Microsoft.EntityFrameworkCore.Migrations;

namespace PaniMusic.Core.Migrations
{
    public partial class addfavoritesmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Albums_AlbumId",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_AspNetUsers_UserId",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_MusicVideos_MusicVideoId",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Tracks_TrackId",
                table: "Favorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite");

            migrationBuilder.RenameTable(
                name: "Favorite",
                newName: "Favorites");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_UserId",
                table: "Favorites",
                newName: "IX_Favorites_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_TrackId",
                table: "Favorites",
                newName: "IX_Favorites_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_MusicVideoId",
                table: "Favorites",
                newName: "IX_Favorites_MusicVideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_AlbumId",
                table: "Favorites",
                newName: "IX_Favorites_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Albums_AlbumId",
                table: "Favorites",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_MusicVideos_MusicVideoId",
                table: "Favorites",
                column: "MusicVideoId",
                principalTable: "MusicVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Tracks_TrackId",
                table: "Favorites",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Albums_AlbumId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_MusicVideos_MusicVideoId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Tracks_TrackId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "Favorite");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_UserId",
                table: "Favorite",
                newName: "IX_Favorite_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_TrackId",
                table: "Favorite",
                newName: "IX_Favorite_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_MusicVideoId",
                table: "Favorite",
                newName: "IX_Favorite_MusicVideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_AlbumId",
                table: "Favorite",
                newName: "IX_Favorite_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Albums_AlbumId",
                table: "Favorite",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_AspNetUsers_UserId",
                table: "Favorite",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_MusicVideos_MusicVideoId",
                table: "Favorite",
                column: "MusicVideoId",
                principalTable: "MusicVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Tracks_TrackId",
                table: "Favorite",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
