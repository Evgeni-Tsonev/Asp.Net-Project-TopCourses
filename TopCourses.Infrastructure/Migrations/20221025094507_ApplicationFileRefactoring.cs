using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopCourses.Infrastructure.Migrations
{
    public partial class ApplicationFileRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Files");
        }
    }
}
