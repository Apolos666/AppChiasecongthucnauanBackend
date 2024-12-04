
namespace Controllers
{
    public class BookmarkCreateDto
    {
        public Guid RecipeId { get; internal set; }
        public Guid UserId { get; internal set; }
    }
}