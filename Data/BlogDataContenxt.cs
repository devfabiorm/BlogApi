using BlogApi.Data.Mappings;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data;

public class BlogDataContext : DbContext
{
    public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PostWithTagsCount> PostWithTagsCount { get; set; }

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