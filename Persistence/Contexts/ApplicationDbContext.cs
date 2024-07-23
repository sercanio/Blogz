﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BlogZ.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        // Identity DbSet properties
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
        public DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
        public DbSet<IdentityUserToken<string>> UserTokens { get; set; }
        public DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions, IConfiguration configuration)
            : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Identity table configurations
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("AspNetUsers"); // Change table name if needed
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("AspNetRoles"); // Change table name if needed
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("AspNetUserRoles"); // Change table name if needed
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("AspNetUserClaims"); // Change table name if needed
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("AspNetUserLogins"); // Change table name if needed);
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("AspNetUserTokens"); // Change table name if needed
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("AspNetRoleClaims"); // Change table name if needed
            });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var userId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<IdentityUser>();

            // Seed IdentityUser
            var user = new IdentityUser
            {
                Id = userId,
                UserName = "johndoe",
                NormalizedUserName = "JOHNDOE",
                Email = "johndoe@email.com",
                NormalizedEmail = "JOHNDOE@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Passw0rd!"),
                SecurityStamp = string.Empty
            };

            // Seed Author
            var authorId = Guid.NewGuid();
            var author = new Author
            {
                Id = authorId,
                UserId = userId,
                Biography = "This is John Doe's biography.",
                ProfileImageURL = "https://example.com/profilepicture.jpg"
            };

            // Seed Blog
            var blogId = Guid.NewGuid();
            var blog = new Blog
            {
                Id = blogId,
                AuthorId = authorId,
                CreatedDate = DateTime.UtcNow
            };

            // Seed Tags
            var tags = new List<Tag>
            {
                new(){ Id = Guid.NewGuid(), Name = "#Programming", NormalizedName = "#PROGRAMMING", Description = "All things programming", CreatedDate = DateTime.UtcNow },
                new(){ Id = Guid.NewGuid(), Name = "#SoftwareEngineering", NormalizedName = "#SOFTWAREENGINEERING", Description = "Software engineering topics", CreatedDate = DateTime.UtcNow },
                new(){ Id = Guid.NewGuid(), Name = "#Science", NormalizedName = "#SCIENCE", Description = "Scientific discoveries and discussions", CreatedDate = DateTime.UtcNow },
                new(){ Id = Guid.NewGuid(), Name = "#ASP.Net", NormalizedName = "#ASP.NET", Description = "ASP.NET related content" ,CreatedDate = DateTime.UtcNow }
            };

            // Seed Posts
            var posts = new List<Post>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "Introduction to Programming",
                    Slug = "introduction-to-programming",
                    Content = "This post covers the basics of programming...",
                    IsPublic = true,
                    CoverImageURL = "https://example.com/cover1.jpg",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "Advanced Software Engineering",
                    Slug = "advanced-software-engineering",
                    Content = "This post delves into advanced concepts in software engineering...",
                    IsPublic = true,
                    CoverImageURL = "https://example.com/cover2.jpg",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "The Wonders of Science",
                    Slug = "th-Wonders-of-science",
                    Content = "Exploring the fascinating world of science...",
                    IsPublic = true,
                    CoverImageURL = "https://example.com/cover3.jpg",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "ASP.NET Core MVC Tutorial",
                    Slug = "asp.net-core-mvc-tutorial",
                    Content = "A comprehensive guide to building web applications with ASP.NET Core MVC...",
                    IsPublic = true,
                    CoverImageURL = "https://example.com/cover4.jpg",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "Best Practices in Programming",
                    Slug = "best-ractices-in-rogramming",
                    Content = "Discussing the best practices every programmer should follow...",
                    IsPublic = true,
                    CoverImageURL = "https://example.com/cover5.jpg",
                    CreatedDate = DateTime.UtcNow
                }
            };

            modelBuilder.Entity<IdentityUser>().HasData(user);
            modelBuilder.Entity<Author>().HasData(author);
            modelBuilder.Entity<Blog>().HasData(blog);
            modelBuilder.Entity<Tag>().HasData(tags);
            modelBuilder.Entity<Post>().HasData(posts);
        }
    }
}
