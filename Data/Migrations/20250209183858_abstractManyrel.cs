using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forms.Data.Migrations
{
    /// <inheritdoc />
    public partial class abstractManyrel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedUsers");

            migrationBuilder.DropTable(
                name: "TemplateTags");

            migrationBuilder.CreateTable(
                name: "Template_Tags",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "integer", nullable: false),
                    TemplatesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template_Tags", x => new { x.TagsId, x.TemplatesId });
                    table.ForeignKey(
                        name: "FK_Template_Tags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Template_Tags_Templates_TemplatesId",
                        column: x => x.TemplatesId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Template_Users",
                columns: table => new
                {
                    AllowedUsersId = table.Column<string>(type: "text", nullable: false),
                    TemplatesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template_Users", x => new { x.AllowedUsersId, x.TemplatesId });
                    table.ForeignKey(
                        name: "FK_Template_Users_AspNetUsers_AllowedUsersId",
                        column: x => x.AllowedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Template_Users_Templates_TemplatesId",
                        column: x => x.TemplatesId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Template_Tags_TemplatesId",
                table: "Template_Tags",
                column: "TemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_Users_TemplatesId",
                table: "Template_Users",
                column: "TemplatesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template_Tags");

            migrationBuilder.DropTable(
                name: "Template_Users");

            migrationBuilder.CreateTable(
                name: "AllowedUsers",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedUsers", x => new { x.TemplateId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AllowedUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllowedUsers_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateTags",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateTags", x => new { x.TemplateId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TemplateTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateTags_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowedUsers_UserId",
                table: "AllowedUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateTags_TagId",
                table: "TemplateTags",
                column: "TagId");
        }
    }
}
