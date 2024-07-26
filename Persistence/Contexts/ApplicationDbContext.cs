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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            var superAdminRoleId = Guid.NewGuid();
            var moderatorRoleId = Guid.NewGuid();
            var authorRoleId = Guid.NewGuid();
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = superAdminRoleId.ToString(),
                    Name = "Admin",
                    NormalizedName= "ADMIN",
                    ConcurrencyStamp = superAdminRoleId.ToString()
                },
                new IdentityRole
                {
                    Id = moderatorRoleId.ToString(),
                    Name = "Moderator",
                    NormalizedName= "MODERATOR",
                    ConcurrencyStamp = moderatorRoleId.ToString()
                },
                new IdentityRole
                {
                    Id = authorRoleId.ToString(),
                    Name = "Author",
                    NormalizedName= "AUTHOR",
                    ConcurrencyStamp = authorRoleId.ToString()
                }
            };

            // Seed SuperAdmin user
            var superAdminId = Guid.NewGuid();
            var hasher = new PasswordHasher<IdentityUser>();
            var superAdmin = new IdentityUser
            {
                Id = superAdminId.ToString(),
                UserName = "johndoe",
                NormalizedUserName = "JOHNDOE",
                Email = "johndoe@email.com",
                NormalizedEmail = "JOHNDOE@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Passw0rd!"),
                SecurityStamp = string.Empty
            };

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId.ToString(),
                    UserId = superAdminId.ToString()
                },
                new IdentityUserRole<string>()
                {
                    RoleId = moderatorRoleId.ToString(),
                    UserId = superAdminId.ToString()
                },
                new IdentityUserRole<string>()
                {
                    RoleId = authorRoleId.ToString(),
                    UserId = superAdminId.ToString()
                }
            };

            // Seed Author
            var authorId = Guid.NewGuid();
            var author = new Author
            {
                Id = authorId,
                UserId = superAdminId.ToString(),
                Biography = "This is John Doe's biography.",
                ProfileImageURL = "https://picsum.photos/100/100"
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
                    Slug = $"introduction-to-programming--{blogId}",
                    Content = "<p>Programming is the art of instructing a computer to perform specific tasks through written code. At its core, it&rsquo;s about solving problems by creating algorithms and translating them into a language the computer understands. The journey into programming often begins with learning a programming language, such as Python, JavaScript, or C++. Each language has its syntax and rules, but the fundamental principles of programming, like variables, loops, and conditionals, are shared among them.</p>\r\n<p>One of the first concepts in programming is understanding variables, which act as storage locations for data. Think of variables as containers that hold information which can be used and manipulated throughout your code. This could be anything from a number representing a user&rsquo;s age to a string containing their name. Variables are essential for dynamic and flexible coding, allowing your programs to handle a range of inputs and outputs.</p>\r\n<p>Control structures are another key aspect of programming. These include loops, which allow a set of instructions to be repeated multiple times, and conditionals, which enable your program to make decisions based on certain conditions. For example, a loop might be used to process each item in a list, while a conditional statement might determine whether a user is allowed to access a particular feature based on their credentials.</p>\r\n<p>Functions are a critical component of organizing and managing code. They enable you to group related instructions into reusable blocks. This not only helps to make your code more modular and easier to manage but also allows for code reuse, which can greatly improve efficiency. Functions can be as simple as a block of code that adds two numbers together or as complex as a complete system for handling user authentication.</p>\r\n<p>As you delve deeper into programming, you'll encounter various paradigms and methodologies, such as object-oriented programming (OOP) and functional programming. OOP focuses on organizing code around objects and classes, which can help in modeling real-world entities and their interactions. Functional programming, on the other hand, emphasizes the use of functions as first-class citizens and immutable data.</p>\r\n<p>Finally, programming is an ever-evolving field, with new languages, tools, and best practices emerging regularly. Staying updated with industry trends and continually practicing your skills are crucial for growth. Whether you&rsquo;re developing a web application, a mobile app, or a complex algorithm, the principles of programming will guide you in creating efficient and effective solutions.</p>",
                    IsPublic = true,
                    CoverImageURL = "http://res.cloudinary.com/dkhrns74w/image/upload/v1721991075/jugi65ddx6d8t503vq3v.jpg",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "Advanced Software Engineering",
                    Slug = $"advanced-software-engineering--{blogId}",
                    Content = "<p>Advanced software engineering encompasses the techniques and practices that go beyond basic software development. It focuses on complex aspects of building robust, scalable, and maintainable software systems. As technology evolves and systems grow in complexity, mastering these advanced concepts becomes essential for software engineers aiming to excel in their field.</p>\r\n<p>One crucial area in advanced software engineering is architectural design. This involves creating high-level structures for software systems, defining how components interact, and ensuring scalability and performance. Common architectural patterns include microservices, which break down applications into smaller, loosely coupled services, and event-driven architectures, which use events to drive communication between different parts of a system. Choosing the right architecture can significantly impact the system's ability to handle growth and change.</p>\r\n<p>Another significant aspect is the use of design patterns. These are well-established solutions to common problems in software design. Patterns like Singleton, Observer, and Factory provide reusable templates that help in managing complexity and ensuring code maintainability. Understanding and applying these patterns can help in designing software that is both flexible and robust, making it easier to manage and extend.</p>\r\n<p>Advanced software engineering also involves rigorous testing and quality assurance practices. This includes not only unit testing but also integration testing, system testing, and acceptance testing. Techniques such as Test-Driven Development (TDD) and Behavior-Driven Development (BDD) are often employed to ensure that the software meets the desired quality standards and behaves as expected in various scenarios.</p>\r\n<p>DevOps practices play a crucial role in advanced software engineering. DevOps integrates development and operations teams to streamline the software delivery process. This involves continuous integration (CI) and continuous deployment (CD) practices, which automate the build, test, and deployment processes. By embracing DevOps, teams can achieve faster releases, improved collaboration, and higher software quality.</p>\r\n<p>Performance optimization is another key focus. As applications scale, performance issues can arise, making it essential to employ strategies for optimizing code, database queries, and system resources. Techniques such as caching, load balancing, and performance profiling are critical for ensuring that applications run efficiently and handle increasing loads effectively.</p>\r\n<p>Lastly, security is a paramount concern in advanced software engineering. As threats evolve, incorporating security best practices into the development lifecycle is crucial. This includes regular security assessments, implementing secure coding practices, and staying updated with the latest security vulnerabilities and mitigation strategies. Ensuring that software is secure helps protect data and maintain user trust in increasingly interconnected systems.</p>\r\n<p>In summary, advanced software engineering involves mastering complex concepts such as architectural design, design patterns, testing practices, DevOps, performance optimization, and security. By focusing on these areas, software engineers can build systems that are not only functional but also resilient, scalable, and secure.</p>",
                    IsPublic = true,
                    CoverImageURL = "http://res.cloudinary.com/dkhrns74w/image/upload/v1721991271/qfdi5fq1efuxji8pq73f.webp",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "The Wonders of Science",
                    Slug = $"wonders-of-science--{blogId}",
                    Content = "<p>Science is a remarkable journey of discovery that has fundamentally transformed our understanding of the universe and our place within it. From the smallest particles to the vast expanses of space, science unravels the mysteries of existence through rigorous experimentation, observation, and analysis. It&rsquo;s a testament to human curiosity and ingenuity, driving progress and enhancing our quality of life in ways that were once unimaginable.</p>\r\n<p>One of the most awe-inspiring aspects of science is its ability to reveal the intricate workings of nature. The study of physics has unveiled the fundamental forces that govern the universe, from gravity to electromagnetism. Quantum mechanics, for instance, explores the behavior of particles at the smallest scales, challenging our perceptions of reality and leading to groundbreaking technologies such as quantum computing and advanced materials.</p>\r\n<p>Biology, on the other hand, delves into the complexities of life itself. From the discovery of the DNA double helix to the mapping of the human genome, biology has provided profound insights into the mechanisms of heredity, evolution, and disease. This knowledge has paved the way for advances in medicine, such as personalized treatments and gene therapies, which hold the promise of curing previously untreatable conditions.</p>\r\n<p>The field of astronomy extends our gaze beyond Earth, exploring the cosmos and our place within it. The development of powerful telescopes has allowed us to observe distant galaxies, black holes, and exoplanets. These discoveries not only satisfy our cosmic curiosity but also inform our understanding of the origins and evolution of the universe, and potentially the existence of life beyond our planet.</p>\r\n<p>Environmental science, another vital area, focuses on understanding and mitigating the impact of human activities on the planet. Research into climate change, biodiversity loss, and sustainability has led to increased awareness and action towards protecting our environment. Innovations in renewable energy, conservation efforts, and sustainable practices are crucial for ensuring a healthier planet for future generations.</p>\r\n<p>Science also plays a significant role in technology, driving innovations that shape our daily lives. The advent of the internet, smartphones, and artificial intelligence are all products of scientific research and development. These technologies not only enhance communication and convenience but also open up new possibilities for learning, creativity, and problem-solving.</p>\r\n<p>Ultimately, the wonders of science are not just about the discoveries themselves but also about the spirit of exploration and the quest for knowledge. Science encourages us to question, explore, and innovate, fostering a sense of wonder and inspiring future generations to push the boundaries of what is possible. It&rsquo;s a continuous journey that enriches our understanding of the world and empowers us to create a better future.</p>",
                    IsPublic = true,
                    CoverImageURL = "http://res.cloudinary.com/dkhrns74w/image/upload/v1721991420/gvmv3qwakrlx8op6adkc.png",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "ASP.NET Core MVC Tutorial",
                    Slug = $"asp-net-core-mvc-tutorial--{blogId}",
                    Content = "<p>ASP.NET Core MVC is a powerful framework for building web applications and APIs, offering a structured approach to developing modern, scalable, and maintainable solutions. This tutorial provides an overview of the key concepts and steps to get started with ASP.NET Core MVC, helping you build a robust web application from scratch.</p>\r\n<p><strong>1. Understanding the Basics</strong></p>\r\n<p>ASP.NET Core MVC follows the Model-View-Controller (MVC) pattern, which separates the application into three main components: Models, Views, and Controllers. Models represent the data and business logic, Views handle the user interface, and Controllers manage the user input and interactions. This separation of concerns helps in organizing code efficiently and improving maintainability.</p>\r\n<p><strong>2. Setting Up the Development Environment</strong></p>\r\n<p>To start building with ASP.NET Core MVC, you need to set up your development environment. Install the .NET SDK from the <a href=\"https://dotnet.microsoft.com/download\" target=\"_new\" rel=\"noreferrer\">official .NET website</a> and choose an integrated development environment (IDE) like Visual Studio or Visual Studio Code. Both IDEs offer excellent support for ASP.NET Core MVC development, including project templates, debugging tools, and code completion.</p>\r\n<p><strong>3. Creating a New ASP.NET Core MVC Project</strong></p>\r\n<p>Open your IDE and create a new ASP.NET Core MVC project. In Visual Studio, you can select the \"ASP.NET Core Web Application\" template and choose \"Web Application (Model-View-Controller)\" during the project setup. For Visual Studio Code, you can use the .NET CLI command <code>dotnet new mvc</code> to scaffold a new MVC application. This generates a project with the essential files and folders, including Controllers, Views, and Models.</p>\r\n<p><strong>4. Defining Models</strong></p>\r\n<p>Models are the classes that represent the data of your application. Define model classes in the <code>Models</code> folder, typically using plain C# classes. For instance, you might create a <code>Product</code> model with properties like <code>Id</code>, <code>Name</code>, and <code>Price</code>. Models are often used to interact with databases via Entity Framework Core, which provides an Object-Relational Mapping (ORM) layer to simplify data access.</p>\r\n<p><strong>5. Creating Controllers</strong></p>\r\n<p>Controllers handle user requests and return responses. In the <code>Controllers</code> folder, create a new controller class, such as <code>ProductController</code>. Controllers contain action methods that correspond to different routes in your application. For example, an <code>Index</code> action method might retrieve a list of products and pass them to a view for rendering. Controllers also handle user input and interactions, such as form submissions.</p>\r\n<p><strong>6. Designing Views</strong></p>\r\n<p>Views are responsible for rendering the user interface. Located in the <code>Views</code> folder, views are typically written using Razor syntax, which combines HTML with C# code. Create Razor view files with <code>.cshtml</code> extensions for different pages of your application. For example, you might have an <code>Index.cshtml</code> view that displays a list of products. Views can use data passed from controllers to dynamically generate content.</p>\r\n<p><strong>7. Routing and Navigation</strong></p>\r\n<p>ASP.NET Core MVC uses routing to map incoming requests to the appropriate controller actions. By default, routes are defined in the <code>Startup.cs</code> file, but you can customize them to fit your application&rsquo;s needs. Use attribute routing to specify routes directly on action methods, or configure conventional routes to define patterns for your URLs.</p>\r\n<p><strong>8. Implementing CRUD Operations</strong></p>\r\n<p>A common use case for ASP.NET Core MVC is to implement CRUD (Create, Read, Update, Delete) operations. For instance, you can create actions in your controller to handle creating new products, displaying product details, editing existing products, and deleting products. Each action method interacts with the model and views to perform these operations.</p>\r\n<p><strong>9. Adding Validation and Error Handling</strong></p>\r\n<p>To ensure data integrity, implement validation in your models using data annotations or FluentValidation. This helps in validating user input before it reaches the database. Additionally, configure global error handling in your application to handle exceptions gracefully and provide meaningful error messages to users.</p>\r\n<p><strong>10. Deploying Your Application</strong></p>\r\n<p>Once your application is complete, you need to deploy it to a web server. ASP.NET Core MVC applications can be hosted on various platforms, including IIS, Azure, and Linux servers. Use the built-in publishing tools in Visual Studio or the .NET CLI to package and deploy your application to your chosen environment.</p>\r\n<p>In summary, ASP.NET Core MVC provides a robust framework for developing web applications by adhering to the MVC pattern. By following this tutorial, you can create a well-structured, scalable, and maintainable application, leveraging the full potential of ASP.NET Core MVC.</p>",
                    IsPublic = true,
                    CoverImageURL = "http://res.cloudinary.com/dkhrns74w/image/upload/v1721991561/lqsfmeuv4hsqdmizyxyj.jpg",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = "Best Practices in Programming",
                    Slug = $"best-practices-in-programming--{blogId}",
                    Content = "<p>Programming is not just about writing code that works; it's about crafting code that is efficient, maintainable, and scalable. Adhering to best practices in programming can greatly enhance the quality of your code and the effectiveness of your development process. Here are some key best practices to follow:</p>\r\n<p><strong>1. Write Readable and Understandable Code</strong></p>\r\n<p>Readable code is crucial for maintainability and collaboration. Use meaningful names for variables, functions, and classes to make your code self-documenting. Avoid complex logic and nested structures where possible; instead, strive for simplicity and clarity. Consistent formatting and adherence to a coding style guide also make your code easier to read and understand.</p>\r\n<p><strong>2. Follow the DRY Principle</strong></p>\r\n<p>The DRY (Don&rsquo;t Repeat Yourself) principle emphasizes the importance of reducing code duplication. Repeating code can lead to inconsistencies and make maintenance more difficult. Instead, encapsulate reusable logic in functions or classes, and use inheritance or composition to share behavior across different parts of your application. This promotes code reuse and reduces the risk of bugs.</p>\r\n<p><strong>3. Implement Proper Error Handling</strong></p>\r\n<p>Robust error handling is essential for creating reliable software. Anticipate potential issues and handle exceptions gracefully to prevent crashes and unpredictable behavior. Implement logging to capture errors and diagnose problems effectively. Ensure that your application provides meaningful error messages to users, and avoid exposing internal details that could pose security risks.</p>\r\n<p><strong>4. Write Unit Tests</strong></p>\r\n<p>Unit testing helps ensure that individual components of your code work as expected. By writing automated tests, you can verify the correctness of your code and catch regressions early in the development process. Use testing frameworks relevant to your programming language and strive for high test coverage to improve the reliability and robustness of your application.</p>\r\n<p><strong>5. Use Version Control</strong></p>\r\n<p>Version control systems like Git are indispensable tools for managing changes to your codebase. They allow you to track modifications, collaborate with other developers, and revert changes if necessary. Regularly commit your code and use meaningful commit messages to document the purpose of each change. Branching and merging strategies help manage different features and bug fixes efficiently.</p>\r\n<p><strong>6. Optimize Performance</strong></p>\r\n<p>Performance optimization is crucial for creating responsive and efficient applications. Profile your code to identify bottlenecks and areas for improvement. Use efficient algorithms and data structures to handle large datasets and complex operations. Additionally, consider optimizing database queries, minimizing network requests, and caching frequently accessed data to enhance performance.</p>\r\n<p><strong>7. Adhere to Security Best Practices</strong></p>\r\n<p>Security is a critical aspect of programming. Follow best practices to protect your application from common vulnerabilities such as SQL injection, cross-site scripting (XSS), and cross-site request forgery (CSRF). Validate and sanitize user input, use encryption for sensitive data, and apply security patches and updates to your dependencies regularly.</p>\r\n<p><strong>8. Document Your Code</strong></p>\r\n<p>Proper documentation is vital for maintaining and understanding your codebase. Write comments to explain the purpose of complex logic and provide context for future developers. Maintain updated documentation for your API, architecture, and configuration settings. Good documentation helps onboard new team members and ensures that everyone understands the codebase.</p>\r\n<p><strong>9. Refactor Code Regularly</strong></p>\r\n<p>Refactoring is the process of improving the structure of your code without changing its functionality. Regularly review and refactor your code to enhance readability, reduce complexity, and eliminate technical debt. Refactoring helps in keeping your codebase clean and maintainable, making it easier to implement new features and fix bugs.</p>\r\n<p><strong>10. Keep Learning and Adapting</strong></p>\r\n<p>The field of programming is constantly evolving with new technologies, languages, and best practices. Stay current with industry trends and continuously improve your skills. Participate in coding communities, read relevant literature, and experiment with new tools and techniques. Embracing a growth mindset will help you adapt to changes and advance your programming expertise.</p>\r\n<p>Incorporating these best practices into your programming workflow can significantly enhance the quality, maintainability, and security of your code. By focusing on readability, efficiency, and continuous improvement, you can build robust applications and become a more effective developer.</p>",
                    IsPublic = true,
                    CoverImageURL = "http://res.cloudinary.com/dkhrns74w/image/upload/v1721991697/h2qtk4nf68zhtq9qp5tv.png",
                    CreatedDate = DateTime.UtcNow
                }
            };


            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.Entity<IdentityUser>().HasData(superAdmin);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
            modelBuilder.Entity<Author>().HasData(author);
            modelBuilder.Entity<Blog>().HasData(blog);
            modelBuilder.Entity<Tag>().HasData(tags);
            modelBuilder.Entity<Post>().HasData(posts);
        }
    }
}
