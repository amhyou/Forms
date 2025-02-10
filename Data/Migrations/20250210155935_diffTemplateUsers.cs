using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forms.Data.Migrations
{
    /// <inheritdoc />
    public partial class diffTemplateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_Users_Templates_TemplatesId",
                table: "Template_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Template_Users",
                table: "Template_Users");

            migrationBuilder.DropIndex(
                name: "IX_Template_Users_TemplatesId",
                table: "Template_Users");

            migrationBuilder.RenameColumn(
                name: "TemplatesId",
                table: "Template_Users",
                newName: "AllowedTemplatesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Template_Users",
                table: "Template_Users",
                columns: new[] { "AllowedTemplatesId", "AllowedUsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_Template_Users_AllowedUsersId",
                table: "Template_Users",
                column: "AllowedUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_Users_Templates_AllowedTemplatesId",
                table: "Template_Users",
                column: "AllowedTemplatesId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_Users_Templates_AllowedTemplatesId",
                table: "Template_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Template_Users",
                table: "Template_Users");

            migrationBuilder.DropIndex(
                name: "IX_Template_Users_AllowedUsersId",
                table: "Template_Users");

            migrationBuilder.RenameColumn(
                name: "AllowedTemplatesId",
                table: "Template_Users",
                newName: "TemplatesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Template_Users",
                table: "Template_Users",
                columns: new[] { "AllowedUsersId", "TemplatesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Template_Users_TemplatesId",
                table: "Template_Users",
                column: "TemplatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_Users_Templates_TemplatesId",
                table: "Template_Users",
                column: "TemplatesId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
