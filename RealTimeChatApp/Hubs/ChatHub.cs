using Microsoft.AspNetCore.SignalR;

namespace RealTimeChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private static List<string> _connectedUsers = new List<string>();

        public override Task OnConnectedAsync()
        {
            _connectedUsers.Add(Context.User.Identity.Name);

            Clients.All.SendAsync("UpdateUserList", _connectedUsers);

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectedUsers.Remove(Context.User.Identity.Name);

            Clients.All.SendAsync("UpdateUserList", _connectedUsers);

            return base.OnDisconnectedAsync(exception);
        }
        
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendTypingStatus(string userName)
        {
            await Clients.Others.SendAsync("UserIsTyping", userName);
        }

        public async Task StopTypingStatus(string userName)
        {
            await Clients.Others.SendAsync("UserStoppedTyping", userName);
        }
    }
}
