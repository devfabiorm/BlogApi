using BlogApi.Data.Mappings;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data;

public class BlogDataContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PostWithTagsCount> PostWithTagsCount { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true");
        options.LogTo(Console.WriteLine); ///Enable logging in console
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new PostMap());

        modelBuilder.Entity<PostWithTagsCount>(x =>
        {
            x.HasNoKey();
            x.ToSqlQuery(@"
                SELECT
                    [Title] AS [Name],
                    (SELECT COUNT([Id]) FROM [Tags] INNER JOIN [PostTag] ON [PostTag].[PostId] = [Post].[Id])
                        AS [Count]
                FROM
                    [Post]");
        });
    }
}