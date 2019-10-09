using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi1.Data;
using WebApi1.Models;
using WebApi1.Models.ViewModels;

namespace WebApi1.Hubs
{
    public class Chathub : Hub
    {
        private readonly IFriendRepository _friend;
        private readonly DataContext _context;

        public Chathub(IFriendRepository friend, DataContext context)
        {
            _friend = friend;
            _context = context;
        }

        List<FriendViewModel> friends = new List<FriendViewModel>();
        string userId;
        string username;

        public override async Task OnConnectedAsync()
        {
            userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            username = Context.User?.Identity?.Name;
            friends = await _friend.GetAllMyFriends(userId);
            foreach (FriendViewModel vm in friends)
            {
                await Clients.Group(vm.Username).SendAsync("FriendOnline", username);
            }
            _context.Users.FirstOrDefault(x => x.Id == userId).Online = true;
            await _context.SaveChangesAsync();

            await Clients.Client(Context.ConnectionId).SendAsync("Connected", "blabla");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            username = Context.User?.Identity?.Name;
            friends = await _friend.GetAllMyFriends(userId);
            foreach (FriendViewModel vm in friends)
            {
                await Clients.Group(vm.Username).SendAsync("FriendOffline", username);
            }
            _context.Users.FirstOrDefault(x => x.Id == userId).Online = false;
            await _context.SaveChangesAsync();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddToGroup(string token)
        {
            username = Context.User?.Identity?.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
        }

        public async Task SendMessage(Message message)
        {
            message.Sender = Context.User?.Identity?.Name;
            message.SenderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //await Clients.All.SendAsync("Message", message);
            await Clients.Group(message.Receiver).SendAsync("Message", message);
        }

        public class Message
        {
            public string Content { get; set; }

            public string Token { get; set; }

            public string Receiver { get; set; }

            public string ReceiverId { get; set; }
            public string SenderId { get; set; }

            public string Sender { get; set; }
        }
    }
}
