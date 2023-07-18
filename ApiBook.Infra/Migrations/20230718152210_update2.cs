using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBook.Infra.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookCover",
                table: "Book",
                newName: "Cover");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cover",
                table: "Book",
                newName: "BookCover");
        }
    }
}
