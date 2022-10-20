using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopCourses.Infrastructure.Migrations
{
    public partial class addShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseApplicationUser_AspNetUsers_StudentId",
                table: "CourseApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseApplicationUser_Courses_CourseId",
                table: "CourseApplicationUser");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseShoppingCart",
                columns: table => new
                {
                    ShoppingCartCoursesId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseShoppingCart", x => new { x.ShoppingCartCoursesId, x.ShoppingCartId });
                    table.ForeignKey(
                        name: "FK_CourseShoppingCart_Courses_ShoppingCartCoursesId",
                        column: x => x.ShoppingCartCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseShoppingCart_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseShoppingCart_ShoppingCartId",
                table: "CourseShoppingCart",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseApplicationUser_AspNetUsers_StudentId",
                table: "CourseApplicationUser",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseApplicationUser_Courses_CourseId",
                table: "CourseApplicationUser",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseApplicationUser_AspNetUsers_StudentId",
                table: "CourseApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseApplicationUser_Courses_CourseId",
                table: "CourseApplicationUser");

            migrationBuilder.DropTable(
                name: "CourseShoppingCart");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseApplicationUser_AspNetUsers_StudentId",
                table: "CourseApplicationUser",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseApplicationUser_Courses_CourseId",
                table: "CourseApplicationUser",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
