using server.Models;

namespace server.Abstractions
{
    public interface IChatHub
    {
        Task ReceiveMessage(ChatMessageResponse chatMessageResponse);
        Task JoinedUser(string message);
        Task DisconnectedUser(string message);
    }
}
