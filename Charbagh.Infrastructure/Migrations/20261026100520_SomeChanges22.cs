using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charbagh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");
        }
    }
}
