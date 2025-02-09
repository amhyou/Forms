using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace forms.Data.Migrations
{
    /// <inheritdoc />
    public partial class includeTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowedUsers_Forms_FormId",
                table: "AllowedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Forms_FormId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_AspNetUsers_CreatorId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Topics_TopicId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Forms_FormId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_AspNetUsers_AuthorId",
                table: "Responses");

            migrationBuilder.DropTable(
                name: "FormTags");

            migrationBuilder.DropTable(
                name: "ResponseAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Responses_AuthorId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Forms_TopicId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Forms");

            migrationBuilder.RenameColumn(
                name: "FormId",
                table: "Questions",
                newName: "TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_FormId",
                table: "Questions",
                newName: "IX_Questions_TemplateId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Forms",
                newName: "TemplateId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Forms",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Forms",
                newName: "SubmittedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_CreatorId",
                table: "Forms",
                newName: "IX_Forms_AuthorId");

            migrationBuilder.RenameColumn(
                name: "FormId",
                table: "Comments",
                newName: "TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_FormId",
                table: "Comments",
                newName: "IX_Comments_TemplateId");

            migrationBuilder.RenameColumn(
                name: "FormId",
                table: "AllowedUsers",
                newName: "TemplateId");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Responses",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Responses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    TopicId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatorId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Templates_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
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
                name: "IX_Responses_QuestionId",
                table: "Responses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_TemplateId",
                table: "Forms",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreatorId",
                table: "Templates",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TopicId",
                table: "Templates",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateTags_TagId",
                table: "TemplateTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllowedUsers_Templates_TemplateId",
                table: "AllowedUsers",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Templates_TemplateId",
                table: "Comments",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_AspNetUsers_AuthorId",
                table: "Forms",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Templates_TemplateId",
                table: "Forms",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Templates_TemplateId",
                table: "Questions",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Questions_QuestionId",
                table: "Responses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowedUsers_Templates_TemplateId",
                table: "AllowedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Templates_TemplateId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_AspNetUsers_AuthorId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Templates_TemplateId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Templates_TemplateId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Questions_QuestionId",
                table: "Responses");

            migrationBuilder.DropTable(
                name: "TemplateTags");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Responses_QuestionId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Forms_TemplateId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Responses");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "Questions",
                newName: "FormId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_TemplateId",
                table: "Questions",
                newName: "IX_Questions_FormId");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "Forms",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "Forms",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Forms",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_AuthorId",
                table: "Forms",
                newName: "IX_Forms_CreatorId");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "Comments",
                newName: "FormId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TemplateId",
                table: "Comments",
                newName: "IX_Comments_FormId");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "AllowedUsers",
                newName: "FormId");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Responses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "Responses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Forms",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Forms",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Forms",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Forms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FormTags",
                columns: table => new
                {
                    FormId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTags", x => new { x.FormId, x.TagId });
                    table.ForeignKey(
                        name: "FK_FormTags_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponseAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    ResponseId = table.Column<int>(type: "integer", nullable: false),
                    Answer = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponseAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponseAnswers_Responses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Responses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Responses_AuthorId",
                table: "Responses",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_TopicId",
                table: "Forms",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTags_TagId",
                table: "FormTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseAnswers_QuestionId",
                table: "ResponseAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseAnswers_ResponseId",
                table: "ResponseAnswers",
                column: "ResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllowedUsers_Forms_FormId",
                table: "AllowedUsers",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Forms_FormId",
                table: "Comments",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_AspNetUsers_CreatorId",
                table: "Forms",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Topics_TopicId",
                table: "Forms",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Forms_FormId",
                table: "Questions",
                column: "FormId",
                principalTable: "Forms",
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
    }
}
