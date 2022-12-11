using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopCourses.Infrastructure.Migrations
{
    public partial class addedPropsToImageFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CourseId",
                table: "Images",
                column: "CourseId",
                unique: true,
                filter: "[CourseId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Courses_CourseId",
                table: "Images",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Courses_CourseId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_CourseId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageId",
                table: "Courses",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
