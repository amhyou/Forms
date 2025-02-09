using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forms.Data.Migrations
{
    /// <inheritdoc />
    public partial class addOrderToQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Templates");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Templates",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Templates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
