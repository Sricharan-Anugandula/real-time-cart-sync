using Microsoft.AspNetCore.SignalR;

namespace Realtimecart.Hubs
{
    public class CartHub:Hub
    {
        public async Task JoinCart(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        public async Task LeaveCart(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }
    }
}
