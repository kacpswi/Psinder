using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Psinder.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByIdToShelter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Shelters",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shelters_AspNetUsers_CreatedById",
                table: "Shelters");

            migrationBuilder.DropIndex(
                name: "IX_Shelters_CreatedById",
                table: "Shelters");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Shelters");
        }
    }
}
