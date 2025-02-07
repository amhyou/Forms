using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forms.Data.Migrations
{
    /// <inheritdoc />
    public partial class editUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_AspNetUsers_AuthorId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_AspNetUsers_UserId",
                table: "Responses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Responses",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_UserId",
                table: "Responses",
                newName: "IX_Responses_AuthorId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Forms",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_AuthorId",
                table: "Forms",
                newName: "IX_Forms_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_AspNetUsers_CreatorId",
                table: "Forms",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_AspNetUsers_AuthorId",
                table: "Responses",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_AspNetUsers_CreatorId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_AspNetUsers_AuthorId",
                table: "Responses");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Responses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_AuthorId",
                table: "Responses",
                newName: "IX_Responses_UserId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Forms",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_CreatorId",
                table: "Forms",
                newName: "IX_Forms_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_AspNetUsers_AuthorId",
                table: "Forms",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_AspNetUsers_UserId",
                table: "Responses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
