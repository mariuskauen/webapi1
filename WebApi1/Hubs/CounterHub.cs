using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Hubs
{
    public class CounterHub : Hub
    {
        public async Task UpdateCounter(int i)
        {
            await Clients.All.SendAsync("UpdateCounter", i);
        }
    }
}
