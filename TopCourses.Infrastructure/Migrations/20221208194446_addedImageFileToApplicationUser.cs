using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopCourses.Infrastructure.Migrations
{
    public partial class addedImageFileToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ImageFile_ImageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageFile",
                table: "ImageFile");

            migrationBuilder.RenameTable(
                name: "ImageFile",
                newName: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageId",
                table: "Courses",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ImageFile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageFile",
                table: "ImageFile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageId",
                table: "Courses",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ImageFile_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "ImageFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
