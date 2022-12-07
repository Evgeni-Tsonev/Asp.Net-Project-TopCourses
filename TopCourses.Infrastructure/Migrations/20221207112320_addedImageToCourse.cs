using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopCourses.Infrastructure.Migrations
{
    public partial class addedImageToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageId",
                table: "Courses",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Files_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Files_ImageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Courses");
        }
    }
}
