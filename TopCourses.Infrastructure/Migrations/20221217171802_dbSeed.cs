using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopCourses.Infrastructure.Migrations
{
    public partial class dbSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd2e6c29-be4d-45ea-9fee-a0cffdf530d4", "bd2e6c29-be4d-45ea-9fee-a0cffdf530d4", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImage", "SecurityStamp", "ShoppingCartId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "b0cb7f2c-184a-41aa-a746-567d9ab4ec09", "user@abv.bg", false, "User", false, "User", false, null, "USER@ABV.BG", "REGULARUSER", "AQAAAAEAACcQAAAAEJK3Mlg+EEVlwsLV55waZQm/qW+JwB/yzn1gIdO6KyhsOnIc9ZXnvmUcbtGB/2TXMQ==", null, false, new byte[0], "4fb23e14-1f8e-4f0d-876a-d11c00647a9d", null, false, "regularUser" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "dbd1b43e-be5b-4d39-a191-99d971f70457", "admin@abv.bg", false, "Admin", false, "Admin", false, null, "ADMIN@ABV.BG", "ADMIN", "AQAAAAEAACcQAAAAEEWEm8K2RwIuw+/AnPvfGKkzQuwCep6B1+yYJ1QObKTQghXaol+QKf42nWG4kYqi8A==", null, false, new byte[0], "1ca9ad8c-2db6-449e-b7de-a77c6fc032be", null, false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDeleted", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, false, null, "Development" },
                    { 2, false, null, "IT and Software" },
                    { 3, false, null, "Photography and video" },
                    { 4, false, null, "Design" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, false, "English" },
                    { 2, false, "Bulgarian" },
                    { 3, false, "French" },
                    { 4, false, "Spanish" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bd2e6c29-be4d-45ea-9fee-a0cffdf530d4", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDeleted", "ParentId", "Title" },
                values: new object[,]
                {
                    { 5, false, 1, "Web Development" },
                    { 6, false, 1, "Data Science" },
                    { 7, false, 1, "Mobile Development" },
                    { 8, false, 2, "Hardware" },
                    { 9, false, 2, "Network and Securiry" },
                    { 10, false, 2, "Operating Systems and Servers" },
                    { 11, false, 3, "Photography" },
                    { 12, false, 3, "Video Design" },
                    { 13, false, 4, "Web Design" },
                    { 14, false, 4, "Game Design" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "CreatorId", "Description", "Goals", "Image", "IsApproved", "IsDeleted", "LanguageId", "LastUpdate", "Level", "OrderId", "Price", "Requirements", "SubCategoryId", "Subtitle", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 17, 19, 18, 2, 135, DateTimeKind.Local).AddTicks(1327), "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "this is very long description.....", "ikasncialsuhcasuicvauivavkajsnvanasc", new byte[0], true, false, 1, null, 0, null, 99.99m, "ikasncialsuhcasuicvauivavkajsnvanasc", 5, "The Complete C# Web Devolopment Bootcamp", "The Complete Web Devolopment Bootcamp" },
                    { 2, 1, new DateTime(2022, 12, 17, 19, 18, 2, 135, DateTimeKind.Local).AddTicks(1367), "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "this is very long description.....", "ikasncialsuhcasuicvauivavkajsnvanasc", new byte[0], true, false, 1, null, 0, null, 75.99m, "ikasncialsuhcasuicvauivavkajsnvanasc", 5, "The Best React Course", "The Complete React Course" },
                    { 3, 1, new DateTime(2022, 12, 17, 19, 18, 2, 135, DateTimeKind.Local).AddTicks(1371), "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "this is very long description.....", "ikasncialsuhcasuicvauivavkajsnvanasc", new byte[0], true, false, 2, null, 0, null, 75.99m, "ikasncialsuhcasuicvauivavkajsnvanasc", 6, "The Best Angular Course", "The Complete Angular Course" },
                    { 4, 1, new DateTime(2022, 12, 17, 19, 18, 2, 135, DateTimeKind.Local).AddTicks(1374), "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "this is very long description.....", "ikasncialsuhcasuicvauivavkajsnvanasc", new byte[0], true, false, 2, null, 0, null, 75.99m, "ikasncialsuhcasuicvauivavkajsnvanasc", 6, "The Best Data Structures Course", "The Complete Data Structures Course" },
                    { 5, 1, new DateTime(2022, 12, 17, 19, 18, 2, 135, DateTimeKind.Local).AddTicks(1378), "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "this is very long description.....", "ikasncialsuhcasuicvauivavkajsnvanasc", new byte[0], true, false, 2, null, 0, null, 75.99m, "ikasncialsuhcasuicvauivavkajsnvanasc", 6, "The Best Mobile Development Course", "The Complete Mobile Development Course" },
                    { 6, 2, new DateTime(2022, 12, 17, 19, 18, 2, 135, DateTimeKind.Local).AddTicks(1383), "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "this is very long description.....", "ikasncialsuhcasuicvauivavkajsnvanasc", new byte[0], true, false, 1, null, 0, null, 75.99m, "ikasncialsuhcasuicvauivavkajsnvanasc", 8, "The Best Mobile Development Course", "The Complete Mobile Development Course" },
                    { 7, 2, new DateTime(2022, 12, 17, 19, 18, 2, 135, DateTimeKind.Local).AddTicks(1386), "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "this is very long description.....", "ikasncialsuhcasuicvauivavkajsnvanasc", new byte[0], true, false, 1, null, 0, null, 75.99m, "ikasncialsuhcasuicvauivavkajsnvanasc", 9, "The Best Mobile Development Course", "The Complete Mobile Development Course" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CourseId", "Description", "IsDeleted", "ResourceId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "very long description...", false, null, "Introduction" },
                    { 2, 1, "very long description...", false, null, "First Topic" },
                    { 3, 2, "very long description...", false, null, "Introduction" },
                    { 4, 2, "very long description...", false, null, "First Topic" },
                    { 5, 3, "very long description...", false, null, "Introduction" },
                    { 6, 3, "very long description...", false, null, "First Topic" },
                    { 7, 4, "very long description...", false, null, "Introduction" },
                    { 8, 4, "very long description...", false, null, "First Topic" },
                    { 9, 5, "very long description...", false, null, "Introduction" },
                    { 10, 5, "very long description...", false, null, "First Topic" },
                    { 11, 6, "very long description...", false, null, "Introduction" },
                    { 12, 6, "very long description...", false, null, "First Topic" },
                    { 13, 7, "very long description...", false, null, "Introduction" },
                    { 14, 7, "very long description...", false, null, "First Topic" }
                });

            migrationBuilder.InsertData(
                table: "Video",
                columns: new[] { "Id", "IsDeleted", "Title", "TopicId", "Url" },
                values: new object[,]
                {
                    { 1, false, "Intro", 1, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 2, false, "First Video", 1, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 3, false, "Other Video", 1, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 4, false, "Intro to Topic", 2, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 5, false, "First Video of topic", 2, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 6, false, "Other Video", 2, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 7, false, "Intro", 3, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 8, false, "First Video", 3, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 9, false, "Other Video", 3, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 10, false, "Intro to Topic", 4, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 11, false, "First Video of topic", 4, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 12, false, "Other Video", 4, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 13, false, "Intro", 5, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 14, false, "First Video", 5, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 15, false, "Other Video", 5, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 16, false, "Intro to Topic", 6, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 17, false, "First Video of topic", 6, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 18, false, "Other Video", 6, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 19, false, "Intro", 7, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 20, false, "First Video", 7, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 21, false, "Other Video", 7, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 22, false, "Intro to Topic", 8, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 23, false, "First Video of topic", 8, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 24, false, "Other Video", 8, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 25, false, "Intro", 9, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 26, false, "First Video", 9, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 27, false, "Other Video", 9, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 28, false, "Intro to Topic", 10, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 29, false, "First Video of topic", 10, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 30, false, "Other Video", 10, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 31, false, "Intro", 9, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 32, false, "First Video", 9, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 33, false, "Other Video", 9, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 34, false, "Intro to Topic", 10, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 35, false, "First Video of topic", 10, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 36, false, "Other Video", 10, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 37, false, "Intro", 11, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 38, false, "First Video", 11, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 39, false, "Other Video", 11, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 40, false, "Intro to Topic", 12, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 41, false, "First Video of topic", 12, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" },
                    { 42, false, "Other Video", 12, "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bd2e6c29-be4d-45ea-9fee-a0cffdf530d4", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Video",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd2e6c29-be4d-45ea-9fee-a0cffdf530d4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);
        }
    }
}
