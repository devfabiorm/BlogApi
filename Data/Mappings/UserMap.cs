using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApi.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //Table
        builder.ToTable("User");

        //Primary Key
        builder.HasKey(x => x.Id);

        //Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        //Propeties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(200);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.Bio)
            .IsRequired(false)
            .HasColumnType("TEXT");

        builder.Property(x => x.Image)
            .IsRequired(false)
            .HasColumnType("VARCHAR")
            .HasMaxLength(2000);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        //Indexes
        builder
            .HasIndex(x => x.Slug, "IX_User_Slug")
            .IsUnique();

        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_UserId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}