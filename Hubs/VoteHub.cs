using Microsoft.AspNetCore.SignalR;

namespace PollSignalR.Hubs
{
    public class VoteHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("A Client Connected: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("A client disconnected: " + Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}