using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApi.Data.Mappings;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        //Table
        builder.ToTable("Category");

        //PrimaryKeyAttribute Key
        builder.HasKey(x => x.Id);

        //Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(); // IDENTITY (1,1)

        //Properties
        builder.Property(x => x.Name)
            .IsRequired() ///NOT NULL
            .HasColumnName("Name") //Unnecessary
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Slug)
            .IsRequired() ///NOT NULL
            .HasColumnName("Slug") //Unnecessary
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        //Indexes
        builder.HasIndex(x => x.Slug, "IX_Category_Slug")
            .IsUnique();
    }
}