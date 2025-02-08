using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebForum.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class LinkUserToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PostLikes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PostComments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Post",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_CreatedBy",
                table: "PostLikes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_CreatedBy",
                table: "PostComments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CreatedBy",
                table: "Post",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_CreatedBy",
                table: "Post",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_AspNetUsers_CreatedBy",
                table: "PostComments",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_AspNetUsers_CreatedBy",
                table: "PostLikes",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_CreatedBy",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_AspNetUsers_CreatedBy",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_AspNetUsers_CreatedBy",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostLikes_CreatedBy",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_CreatedBy",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_Post_CreatedBy",
                table: "Post");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PostLikes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PostComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
