using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main()
        {
            SetupDatabase();

            using (var db = new BloggingContext())
            {
                #region Query
                var postCounts = db.BlogPostCounts.ToList();

                foreach (var postCount in postCounts)
                {
                    Console.WriteLine($"{postCount.BlogName} has {postCount.PostCount} posts.");
                    Console.WriteLine();
                }
                #endregion
            }
            Console.ReadLine();
        }

        private static void SetupDatabase()
        {
            using (var db = new BloggingContext())
            {
                db.Database.EnsureDeleted();
                if (db.Database.EnsureCreated())
                {
                    db.Blogs.Add(
                        new Blog
                        {
                            Name = "Fish Blog",
                            Url = "http://sample.com/blogs/fish",
                            Posts = new List<Post>
                            {
                        new Post { Title = "Fish care 101" },
                        new Post { Title = "Caring for tropical fish" },
                        new Post { Title = "Types of ornamental fish" }
                            }
                        });

                    db.Blogs.Add(
                        new Blog
                        {
                            Name = "Cats Blog",
                            Url = "http://sample.com/blogs/cats",
                            Posts = new List<Post>
                            {
                        new Post { Title = "Cat care 101" },
                        new Post { Title = "Caring for tropical cats" },
                        new Post { Title = "Types of ornamental cats" }
                            }
                        });

                    db.Blogs.Add(
                        new Blog
                        {
                            Name = "Catfish Blog",
                            Url = "http://sample.com/blogs/catfish",
                            Posts = new List<Post>
                            {
                        new Post { Title = "Catfish care 101" }, new Post { Title = "History of the catfish name" }
                            }
                        });

                    db.SaveChanges();

                    var currentDir = Directory.GetCurrentDirectory();

                    #region Functions
                    string[] fnList = Directory.GetFiles(Path.Combine(currentDir, "Scripts", "Function"), "*.sql", SearchOption.TopDirectoryOnly);
                    ExecuteScriptInDB(db, fnList);
                    #endregion

                    #region Views
                    string[] views = Directory.GetFiles(Path.Combine(currentDir, "Scripts", "Views"), "*.sql", SearchOption.TopDirectoryOnly);
                    ExecuteScriptInDB(db, views);
                    #endregion

                    #region SPs
                    string[] sps = Directory.GetFiles(Path.Combine(currentDir, "Scripts", "SP"), "*.sql", SearchOption.TopDirectoryOnly);
                    ExecuteScriptInDB(db, sps);
                    #endregion
                }
            }

            static void ExecuteScriptInDB(BloggingContext db, string[] scripts)
            {
                foreach (var script in scripts)
                {
                    if (!String.IsNullOrWhiteSpace(script))
                    {
                        var readScript = File.ReadAllText(script);
                        db.Database.ExecuteSqlRaw(readScript);
                    }
                }
            }
        }
    }

    public class BloggingContext : DbContext
    {
        private static readonly ILoggerFactory _loggerFactory
            = LoggerFactory.Create(
                builder => builder.AddConsole().AddFilter((c, l) => l == LogLevel.Information && !c.EndsWith("Connection")));

        #region DbSet
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<BlogPostsCount> BlogPostCounts { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=SHOHAG-PC;Database=SqlViewSP_DB;User ID=sa;Password=SqlDev2019;TrustServerCertificate=true;Connection Timeout=3600;")
                          .UseLoggerFactory(_loggerFactory);
        }
        #region Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPostsCount>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.ToView("View_BlogPostCounts");
                        eb.Property(v => v.BlogName).HasColumnName("Name");
                    });
        }
        #endregion
    }
}
