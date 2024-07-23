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
                name: "AspNetRoleClaims",
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
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
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
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
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
                        name: "FK_Authors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "21c958ca-ddd0-4644-bab9-1c6aabc5c0e2", 0, "80d55535-89eb-4a37-aa05-4d60473f95e2", "johndoe@email.com", true, false, null, "JOHNDOE@EMAIL.COM", "JOHNDOE", "AQAAAAIAAYagAAAAEGqtxt6dsGeJzexSGPDLFoe4opiIAWNHej0ncSlZWL8h1M4mFY+BVz48n4wZV6gPXg==", null, false, "", false, "johndoe" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "Name", "NormalizedName", "PostId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("05a4d57d-03f3-417a-b167-4f8c48d0d3e7"), new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2322), null, "All things programming", "#Programming", "#PROGRAMMING", null, null },
                    { new Guid("560bb08d-f954-4d11-9731-5c7b4177c378"), new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2354), null, "Scientific discoveries and discussions", "#Science", "#SCIENCE", null, null },
                    { new Guid("57f63417-fcb6-4259-988c-863b243867e9"), new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2355), null, "ASP.NET related content", "#ASP.Net", "#ASP.NET", null, null },
                    { new Guid("580ad651-78da-4a01-abf7-f69c3530c3f7"), new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2352), null, "Software engineering topics", "#SoftwareEngineering", "#SOFTWAREENGINEERING", null, null }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "BlogId", "CreatedDate", "DeletedDate", "ProfilePictureImageURL", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("2e1d5e51-268e-4d57-9552-ecc4f96342fa"), "This is John Doe's biography.", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "https://example.com/profilepicture.jpg", null, "21c958ca-ddd0-4644-bab9-1c6aabc5c0e2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "AuthorId", "CreatedDate", "DeletedDate", "UpdatedDate" },
                values: new object[] { new Guid("5b6b8840-5345-4cb8-b12a-b4a6dff8c5de"), new Guid("2e1d5e51-268e-4d57-9552-ecc4f96342fa"), new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2303), null, null });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BlogId", "Content", "CoverImageURL", "CreatedDate", "DeletedDate", "IsPublic", "Slug", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("07d249e3-a64c-46f9-a1ed-c6f95ee9414f"), new Guid("5b6b8840-5345-4cb8-b12a-b4a6dff8c5de"), "A comprehensive guide to building web applications with ASP.NET Core MVC...", "https://example.com/cover4.jpg", new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2374), null, true, "asp.net-core-mvc-tutorial", "ASP.NET Core MVC Tutorial", null },
                    { new Guid("5177bc7b-9829-4d0c-9260-903facca2621"), new Guid("5b6b8840-5345-4cb8-b12a-b4a6dff8c5de"), "This post delves into advanced concepts in software engineering...", "https://example.com/cover2.jpg", new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2368), null, true, "advanced-software-engineering", "Advanced Software Engineering", null },
                    { new Guid("c651c5ab-f9ec-492c-8055-48d246f917ae"), new Guid("5b6b8840-5345-4cb8-b12a-b4a6dff8c5de"), "Discussing the best practices every programmer should follow...", "https://example.com/cover5.jpg", new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2376), null, true, "best-ractices-in-rogramming", "Best Practices in Programming", null },
                    { new Guid("c6ee3459-a669-48fe-b96a-a2613a10eeee"), new Guid("5b6b8840-5345-4cb8-b12a-b4a6dff8c5de"), "This post covers the basics of programming...", "https://example.com/cover1.jpg", new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2363), null, true, "introduction-to-programming", "Introduction to Programming", null },
                    { new Guid("ef33b9e4-9a05-4047-8a90-94b5c5f81bc6"), new Guid("5b6b8840-5345-4cb8-b12a-b4a6dff8c5de"), "Exploring the fascinating world of science...", "https://example.com/cover3.jpg", new DateTime(2024, 7, 22, 21, 35, 1, 230, DateTimeKind.Utc).AddTicks(2372), null, true, "th-Wonders-of-science", "The Wonders of Science", null }
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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
