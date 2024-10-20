namespace AppChiaSeCongThucNauAnBackend.Models;

public class UserFollow
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User Follower { get; set; }
    public User Following { get; set; }
}
