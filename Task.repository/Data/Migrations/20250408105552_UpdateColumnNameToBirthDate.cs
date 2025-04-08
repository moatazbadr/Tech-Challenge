using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnNameToBirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOnly",
                table: "addressBooks",
                newName: "BirthDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "addressBooks",
                newName: "DateOnly");
        }
    }
}
