using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]

public partial class modifiedthearticle : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Articles_Users_AuthorId",
            table: "Articles");

        migrationBuilder.DropForeignKey(
            name: "FK_Comments_Users_AuthorId",
            table: "Comments");

        migrationBuilder.DropIndex(
            name: "IX_Comments_AuthorId",
            table: "Comments");

        migrationBuilder.DropIndex(
            name: "IX_Articles_AuthorId",
            table: "Articles");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_Comments_AuthorId",
            table: "Comments",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_Articles_AuthorId",
            table: "Articles",
            column: "AuthorId");

        migrationBuilder.AddForeignKey(
            name: "FK_Articles_Users_AuthorId",
            table: "Articles",
            column: "AuthorId",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Comments_Users_AuthorId",
            table: "Comments",
            column: "AuthorId",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
