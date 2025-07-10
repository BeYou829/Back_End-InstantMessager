using Chat.Core.Application.Abtrasctions.Services;
using Microsoft.AspNetCore.SignalR;
using server.Abstractions;
using server.Models;

namespace server.Hubs
{
    public class ChatHub(IUserService userService) : Hub<IChatHub>
    {
        public async Task SendMessage(string message)
        {
            Guid id = Guid.Parse(Context.UserIdentifier);
            var userInfo = await userService.GetByIdAsync(id);
            ChatMessageResponse chatMessageResponse = new ChatMessageResponse
            {
                Id = id,
                Sender = userInfo.Username,
                Message = message
            };
            await Clients.All.ReceiveMessage(chatMessageResponse);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.JoinedUser("An user has joined");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.DisconnectedUser("An user has left");
        }
    }
}
