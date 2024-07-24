using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfilePictureImageURL = table.Column<string>(type: "text", nullable: true),
                    Biography = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BlogId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    CoverImageURL = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Comment_AuthorId_Index",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "Comment_PostId_Index",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostId",
                table: "Tags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "Tag_NormalizedName_UK",
                table: "Tags",
                column: "NormalizedName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
