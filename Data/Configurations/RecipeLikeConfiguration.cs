using AppChiaSeCongThucNauAnBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppChiaSeCongThucNauAnBackend.Data.Configurations;

public class RecipeLikeConfiguration : IEntityTypeConfiguration<RecipeLike>
{
    public void Configure(EntityTypeBuilder<RecipeLike> builder)
    {
        builder.HasKey(rl => new { rl.UserId, rl.RecipeId });

        builder.HasOne(rl => rl.User)
            .WithMany(u => u.RecipeLikes)
            .HasForeignKey(rl => rl.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rl => rl.Recipe)
            .WithMany(r => r.RecipeLikes)
            .HasForeignKey(rl => rl.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
