using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Hubs
{
    public class Chathub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.Client(Context.ConnectionId).SendAsync("Connected", "blabla");
            return base.OnConnectedAsync();
        }

        public async Task AddToGroup(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var userToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            string groupName = userToken.Claims.FirstOrDefault(x => x.Type == "unique_name").Value;
            
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessage(Message message)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var userToken = tokenHandler.ReadToken(message.Token) as JwtSecurityToken;
            message.Sender = userToken.Claims.FirstOrDefault(x => x.Type == "unique_name").Value;
            message.SenderId = userToken.Claims.FirstOrDefault(x => x.Type == "nameid").Value;
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
