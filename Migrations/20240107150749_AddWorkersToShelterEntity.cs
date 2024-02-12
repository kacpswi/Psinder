using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Psinder.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkersToShelterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shelters_AspNetUsers_CreatedById",
                table: "Shelters");

            migrationBuilder.DropIndex(
                name: "IX_Shelters_CreatedById",
                table: "Shelters");

            migrationBuilder.AddColumn<int>(
                name: "ShelterId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShelterId",
                table: "AspNetUsers",
                column: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Shelters_ShelterId",
                table: "AspNetUsers",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Shelters_ShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShelterId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_CreatedById",
                table: "Shelters",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Shelters_AspNetUsers_CreatedById",
                table: "Shelters",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
