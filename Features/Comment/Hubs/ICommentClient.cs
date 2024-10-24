namespace AppChiaSeCongThucNauAnBackend.Features.Comment.Hubs;

public interface ICommentClient
{
    Task ReceiveComment(string userId, string userName, string content);
}
