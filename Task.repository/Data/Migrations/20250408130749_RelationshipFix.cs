using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "addressBooks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_addressBooks_UserId",
                table: "addressBooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_addressBooks_AspNetUsers_UserId",
                table: "addressBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addressBooks_AspNetUsers_UserId",
                table: "addressBooks");

            migrationBuilder.DropIndex(
                name: "IX_addressBooks_UserId",
                table: "addressBooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "addressBooks");
        }
    }
}
