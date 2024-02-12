using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Psinder.Migrations
{
    /// <inheritdoc />
    public partial class UserLikeBugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_Animals_AnimalId",
                table: "UserLike");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_AspNetUsers_UserId",
                table: "UserLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike");

            migrationBuilder.RenameTable(
                name: "UserLike",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_UserLike_AnimalId",
                table: "Likes",
                newName: "IX_Likes_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "UserId", "AnimalId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Animals_AnimalId",
                table: "Likes",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Animals_AnimalId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "UserLike");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_AnimalId",
                table: "UserLike",
                newName: "IX_UserLike_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike",
                columns: new[] { "UserId", "AnimalId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_Animals_AnimalId",
                table: "UserLike",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_AspNetUsers_UserId",
                table: "UserLike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
