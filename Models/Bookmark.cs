

namespace AppChiaSeCongThucNauAnBackend.Models;

public class Bookmark
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual Recipe Recipe { get; set; }
    public virtual User User { get; set; }
}
