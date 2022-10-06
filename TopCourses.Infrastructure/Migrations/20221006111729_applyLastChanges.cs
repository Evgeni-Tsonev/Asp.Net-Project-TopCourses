using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopCourses.Infrastructure.Migrations
{
    public partial class applyLastChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentID",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentID",
                table: "Categories",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentID",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Categories",
                newName: "ParentID");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                newName: "IX_Categories_ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentID",
                table: "Categories",
                column: "ParentID",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
