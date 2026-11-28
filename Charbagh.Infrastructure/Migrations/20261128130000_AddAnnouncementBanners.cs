using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charbagh.Infrastructure.Migrations;

[DbContext(typeof(CharbaghDbContext))]
[Migration("20261128130000_AddAnnouncementBanners")]
public partial class AddAnnouncementBanners : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AnnouncementBanners",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                LinkUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                ButtonText = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                StartsAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                EndsAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                SortOrder = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_AnnouncementBanners", x => x.Id));

        migrationBuilder.CreateIndex(
            name: "IX_AnnouncementBanners_Status_SortOrder",
            table: "AnnouncementBanners",
            columns: new[] { "Status", "SortOrder" });

        migrationBuilder.CreateIndex(
            name: "IX_AnnouncementBanners_StartsAt_EndsAt",
            table: "AnnouncementBanners",
            columns: new[] { "StartsAt", "EndsAt" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "AnnouncementBanners");
    }
}
