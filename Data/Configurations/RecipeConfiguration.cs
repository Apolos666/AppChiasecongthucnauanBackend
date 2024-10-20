using AppChiaSeCongThucNauAnBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppChiaSeCongThucNauAnBackend.Data.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(r => r.Ingredients)
            .IsRequired();

        builder.Property(r => r.Instructions)
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(r => r.User)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.RecipeCategory)
            .WithMany(rc => rc.Recipes)
            .HasForeignKey(r => r.RecipeCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Comments)
            .WithOne(c => c.Recipe)
            .HasForeignKey(c => c.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Bookmarks)
            .WithOne(b => b.Recipe)
            .HasForeignKey(b => b.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.RecipeLikes)
            .WithOne(rl => rl.Recipe)
            .HasForeignKey(rl => rl.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.RecipeMedia)
            .WithOne(rm => rm.Recipe)
            .HasForeignKey(rm => rm.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
