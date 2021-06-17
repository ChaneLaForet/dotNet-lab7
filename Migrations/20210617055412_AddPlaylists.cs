using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab2.Migrations
{
    public partial class AddPlaylists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlaylistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: DateTime.UtcNow)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoviePlaylist",
                columns: table => new
                {
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    PlaylistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePlaylist", x => new { x.MoviesId, x.PlaylistsId });
                    table.ForeignKey(
                        name: "FK_MoviePlaylist_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviePlaylist_Playlists_PlaylistsId",
                        column: x => x.PlaylistsId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePlaylist_PlaylistsId",
                table: "MoviePlaylist",
                column: "PlaylistsId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_ApplicationUserId",
                table: "Playlists",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviePlaylist");

            migrationBuilder.DropTable(
                name: "Playlists");
        }
    }
}
