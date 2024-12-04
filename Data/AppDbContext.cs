using System.Reflection;
using AppChiaSeCongThucNauAnBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<RecipeLike> RecipeLikes { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }
    public DbSet<RecipeMedia> RecipeMedia { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserConversation> UserConversations { get; set; }
    public DbSet<UserFollow> UserFollows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Bookmark>()
            .HasOne(b => b.Recipe)
            .WithMany(r => r.Bookmarks)
            .HasForeignKey(b => b.RecipeId);
        
        modelBuilder.Entity<Bookmark>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookmarks)
            .HasForeignKey(b => b.UserId);
    }
}
