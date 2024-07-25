using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("28a31387-fa79-421a-9016-67f0dfda37f9"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("35bafb90-e882-4920-919d-540a5c70dd46"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("67f15e6c-ad89-4f96-8907-e064de214377"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("68b96564-2218-4489-89ba-7ddb9f608cea"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("954a3d4a-c7eb-485b-95cb-c24d66a2c000"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2d19c627-6a0f-4091-a0bd-6fd63234fd81");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f2056a8a-6d57-47ef-aaf7-3f755ccb2cd5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "fee93c2b-a6ad-4bfd-ad89-5236ecdd5739");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("040cc631-ee0b-40dc-8dd8-50a9370f5cdc"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4315be8d-daac-4063-a14d-ee2fda04ad26"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6bcd0f3b-9a27-4db5-a551-8444d6b58509"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("deca721e-c42e-4b65-b72e-434026ee8452"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2d19c627-6a0f-4091-a0bd-6fd63234fd81", "e94630d6-b26f-4f20-b27d-e1bdf3ddd232" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2056a8a-6d57-47ef-aaf7-3f755ccb2cd5", "e94630d6-b26f-4f20-b27d-e1bdf3ddd232" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fee93c2b-a6ad-4bfd-ad89-5236ecdd5739", "e94630d6-b26f-4f20-b27d-e1bdf3ddd232" });

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("1070a6fd-bb67-459d-8154-77d53fad104b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("51984c01-75fa-4b06-95eb-5dd5f09d6218"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "e94630d6-b26f-4f20-b27d-e1bdf3ddd232");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c793ddb-0952-4d75-aaec-641ef2b3b946", "4c793ddb-0952-4d75-aaec-641ef2b3b946", "Author", "AUTHOR" },
                    { "bcbd0ce6-22aa-4640-ad03-e274a3e707fa", "bcbd0ce6-22aa-4640-ad03-e274a3e707fa", "Admin", "ADMIN" },
                    { "f69382d4-bbb9-455c-8d44-c934e4797f6c", "f69382d4-bbb9-455c-8d44-c934e4797f6c", "Moderator", "MODERATOR" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "Name", "NormalizedName", "PostId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("568601ad-9bdd-4fdb-94de-c05ee1d85e20"), new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6415), null, "Scientific discoveries and discussions", "#Science", "#SCIENCE", null, null },
                    { new Guid("72f8d61f-83d0-4e15-9ae1-dff92de65b3b"), new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6396), null, "All things programming", "#Programming", "#PROGRAMMING", null, null },
                    { new Guid("76ada677-eaf5-4d7f-af6e-b87db0c8ba8e"), new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6418), null, "ASP.NET related content", "#ASP.Net", "#ASP.NET", null, null },
                    { new Guid("c35ccca6-f53a-4818-9c47-8af09dbdd1a6"), new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6413), null, "Software engineering topics", "#SoftwareEngineering", "#SOFTWAREENGINEERING", null, null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4c793ddb-0952-4d75-aaec-641ef2b3b946", "71d486c1-f9a5-43f6-b329-1a8e386b773b" },
                    { "bcbd0ce6-22aa-4640-ad03-e274a3e707fa", "71d486c1-f9a5-43f6-b329-1a8e386b773b" },
                    { "f69382d4-bbb9-455c-8d44-c934e4797f6c", "71d486c1-f9a5-43f6-b329-1a8e386b773b" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "71d486c1-f9a5-43f6-b329-1a8e386b773b", 0, "1485c4fe-773d-4742-a179-e45a9f32a92c", "johndoe@email.com", true, false, null, "JOHNDOE@EMAIL.COM", "JOHNDOE", "AQAAAAIAAYagAAAAEHKZrz9f7cdB+uE6gYyEXN2teUA9aBYG5EpNjbO4KatfhqVbwGJQPTI/WaH+IoqKEw==", null, false, "", false, "johndoe" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "BlogId", "CreatedDate", "DeletedDate", "ProfilePictureImageURL", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("1429bdd8-ad1e-4253-86ee-e5d015782465"), "This is John Doe's biography.", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "https://example.com/profilepicture.jpg", null, "71d486c1-f9a5-43f6-b329-1a8e386b773b" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "AuthorId", "CreatedDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new Guid("48d2c3d1-6b6a-44d5-a411-fa14c9ecc8e4"), new Guid("1429bdd8-ad1e-4253-86ee-e5d015782465"), new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6390), null, null });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BlogId", "Content", "CoverImageURL", "CreatedDate", "DeletedDate", "IsPublic", "Slug", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("59944914-30f8-48cb-96df-0c4d951ba429"), new Guid("48d2c3d1-6b6a-44d5-a411-fa14c9ecc8e4"), "A comprehensive guide to building web applications with ASP.NET Core MVC...", "https://example.com/cover4.jpg", new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6430), null, true, "asp.net-core-mvc-tutorial", "ASP.NET Core MVC Tutorial", null },
                    { new Guid("85c98ba7-e760-424f-b46c-dec33662fa67"), new Guid("48d2c3d1-6b6a-44d5-a411-fa14c9ecc8e4"), "This post delves into advanced concepts in software engineering...", "https://example.com/cover2.jpg", new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6426), null, true, "advanced-software-engineering", "Advanced Software Engineering", null },
                    { new Guid("878da7c3-014a-4bc6-910b-67dc46ab1f36"), new Guid("48d2c3d1-6b6a-44d5-a411-fa14c9ecc8e4"), "Discussing the best practices every programmer should follow...", "https://example.com/cover5.jpg", new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6432), null, true, "best-ractices-in-rogramming", "Best Practices in Programming", null },
                    { new Guid("a5d1792a-5b00-48fd-a9b3-5c20a10440e8"), new Guid("48d2c3d1-6b6a-44d5-a411-fa14c9ecc8e4"), "Exploring the fascinating world of science...", "https://example.com/cover3.jpg", new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6428), null, true, "th-Wonders-of-science", "The Wonders of Science", null },
                    { new Guid("efae1367-0049-442f-9601-a2663e3e99f0"), new Guid("48d2c3d1-6b6a-44d5-a411-fa14c9ecc8e4"), "This post covers the basics of programming...", "https://example.com/cover1.jpg", new DateTime(2024, 7, 25, 12, 2, 5, 49, DateTimeKind.Utc).AddTicks(6423), null, true, "introduction-to-programming", "Introduction to Programming", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("59944914-30f8-48cb-96df-0c4d951ba429"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("85c98ba7-e760-424f-b46c-dec33662fa67"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("878da7c3-014a-4bc6-910b-67dc46ab1f36"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a5d1792a-5b00-48fd-a9b3-5c20a10440e8"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("efae1367-0049-442f-9601-a2663e3e99f0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4c793ddb-0952-4d75-aaec-641ef2b3b946");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bcbd0ce6-22aa-4640-ad03-e274a3e707fa");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f69382d4-bbb9-455c-8d44-c934e4797f6c");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("568601ad-9bdd-4fdb-94de-c05ee1d85e20"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("72f8d61f-83d0-4e15-9ae1-dff92de65b3b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("76ada677-eaf5-4d7f-af6e-b87db0c8ba8e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c35ccca6-f53a-4818-9c47-8af09dbdd1a6"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4c793ddb-0952-4d75-aaec-641ef2b3b946", "71d486c1-f9a5-43f6-b329-1a8e386b773b" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bcbd0ce6-22aa-4640-ad03-e274a3e707fa", "71d486c1-f9a5-43f6-b329-1a8e386b773b" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f69382d4-bbb9-455c-8d44-c934e4797f6c", "71d486c1-f9a5-43f6-b329-1a8e386b773b" });

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("48d2c3d1-6b6a-44d5-a411-fa14c9ecc8e4"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("1429bdd8-ad1e-4253-86ee-e5d015782465"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "71d486c1-f9a5-43f6-b329-1a8e386b773b");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d19c627-6a0f-4091-a0bd-6fd63234fd81", "2d19c627-6a0f-4091-a0bd-6fd63234fd81", "Author", "AUTHOR" },
                    { "f2056a8a-6d57-47ef-aaf7-3f755ccb2cd5", "f2056a8a-6d57-47ef-aaf7-3f755ccb2cd5", "Admin", "ADMIN" },
                    { "fee93c2b-a6ad-4bfd-ad89-5236ecdd5739", "fee93c2b-a6ad-4bfd-ad89-5236ecdd5739", "Moderator", "MODERATOR" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "Name", "NormalizedName", "PostId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("040cc631-ee0b-40dc-8dd8-50a9370f5cdc"), new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9623), null, "Scientific discoveries and discussions", "#Science", "#SCIENCE", null, null },
                    { new Guid("4315be8d-daac-4063-a14d-ee2fda04ad26"), new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9593), null, "All things programming", "#Programming", "#PROGRAMMING", null, null },
                    { new Guid("6bcd0f3b-9a27-4db5-a551-8444d6b58509"), new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9625), null, "ASP.NET related content", "#ASP.Net", "#ASP.NET", null, null },
                    { new Guid("deca721e-c42e-4b65-b72e-434026ee8452"), new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9602), null, "Software engineering topics", "#SoftwareEngineering", "#SOFTWAREENGINEERING", null, null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2d19c627-6a0f-4091-a0bd-6fd63234fd81", "e94630d6-b26f-4f20-b27d-e1bdf3ddd232" },
                    { "f2056a8a-6d57-47ef-aaf7-3f755ccb2cd5", "e94630d6-b26f-4f20-b27d-e1bdf3ddd232" },
                    { "fee93c2b-a6ad-4bfd-ad89-5236ecdd5739", "e94630d6-b26f-4f20-b27d-e1bdf3ddd232" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e94630d6-b26f-4f20-b27d-e1bdf3ddd232", 0, "c0c19093-45be-494f-a3c5-a5a9a12e67f9", "johndoe@email.com", true, false, null, "JOHNDOE@EMAIL.COM", "JOHNDOE", "AQAAAAIAAYagAAAAELwyHzMpKMA0Xq+4f3IdoFdP0IYxhS7y8Is38ErMUxMPKTpdl4mHUQ16GOtihCl+rw==", null, false, "", false, "johndoe" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "BlogId", "CreatedDate", "DeletedDate", "ProfilePictureImageURL", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("51984c01-75fa-4b06-95eb-5dd5f09d6218"), "This is John Doe's biography.", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "https://example.com/profilepicture.jpg", null, "e94630d6-b26f-4f20-b27d-e1bdf3ddd232" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "AuthorId", "CreatedDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new Guid("1070a6fd-bb67-459d-8154-77d53fad104b"), new Guid("51984c01-75fa-4b06-95eb-5dd5f09d6218"), new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9585), null, null });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BlogId", "Content", "CoverImageURL", "CreatedDate", "DeletedDate", "IsPublic", "Slug", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("28a31387-fa79-421a-9016-67f0dfda37f9"), new Guid("1070a6fd-bb67-459d-8154-77d53fad104b"), "Discussing the best practices every programmer should follow...", "https://example.com/cover5.jpg", new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9645), null, true, "best-ractices-in-rogramming", "Best Practices in Programming", null },
                    { new Guid("35bafb90-e882-4920-919d-540a5c70dd46"), new Guid("1070a6fd-bb67-459d-8154-77d53fad104b"), "This post delves into advanced concepts in software engineering...", "https://example.com/cover2.jpg", new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9639), null, true, "advanced-software-engineering", "Advanced Software Engineering", null },
                    { new Guid("67f15e6c-ad89-4f96-8907-e064de214377"), new Guid("1070a6fd-bb67-459d-8154-77d53fad104b"), "This post covers the basics of programming...", "https://example.com/cover1.jpg", new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9634), null, true, "introduction-to-programming", "Introduction to Programming", null },
                    { new Guid("68b96564-2218-4489-89ba-7ddb9f608cea"), new Guid("1070a6fd-bb67-459d-8154-77d53fad104b"), "A comprehensive guide to building web applications with ASP.NET Core MVC...", "https://example.com/cover4.jpg", new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9643), null, true, "asp.net-core-mvc-tutorial", "ASP.NET Core MVC Tutorial", null },
                    { new Guid("954a3d4a-c7eb-485b-95cb-c24d66a2c000"), new Guid("1070a6fd-bb67-459d-8154-77d53fad104b"), "Exploring the fascinating world of science...", "https://example.com/cover3.jpg", new DateTime(2024, 7, 24, 0, 29, 9, 726, DateTimeKind.Utc).AddTicks(9641), null, true, "th-Wonders-of-science", "The Wonders of Science", null }
                });
        }
    }
}
