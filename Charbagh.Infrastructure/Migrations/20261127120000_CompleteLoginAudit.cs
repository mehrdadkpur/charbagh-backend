using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charbagh.Infrastructure.Migrations;

[DbContext(typeof(CharbaghDbContext))]
[Migration("20261127120000_CompleteLoginAudit")]
public partial class CompleteLoginAudit : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<Guid>(
            name: "UserId",
            table: "LoginHistories",
            type: "uniqueidentifier",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier");

        migrationBuilder.AddColumn<string>(
            name: "AttemptedMobile",
            table: "LoginHistories",
            type: "nvarchar(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<Guid>(
            name: "LoginHistoryId",
            table: "RefreshTokens",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_RefreshTokens_LoginHistoryId",
            table: "RefreshTokens",
            column: "LoginHistoryId");

        migrationBuilder.AddForeignKey(
            name: "FK_RefreshTokens_LoginHistories_LoginHistoryId",
            table: "RefreshTokens",
            column: "LoginHistoryId",
            principalTable: "LoginHistories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE FROM [LoginHistories] WHERE [UserId] IS NULL;");

        migrationBuilder.DropForeignKey(
            name: "FK_RefreshTokens_LoginHistories_LoginHistoryId",
            table: "RefreshTokens");

        migrationBuilder.DropIndex(
            name: "IX_RefreshTokens_LoginHistoryId",
            table: "RefreshTokens");

        migrationBuilder.DropColumn(
            name: "LoginHistoryId",
            table: "RefreshTokens");

        migrationBuilder.DropColumn(
            name: "AttemptedMobile",
            table: "LoginHistories");

        migrationBuilder.AlterColumn<Guid>(
            name: "UserId",
            table: "LoginHistories",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier",
            oldNullable: true);
    }
}
