using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TurretDefence_ServerSide
{ 
        public class GameHub : Hub
        {
        public async Task NewMessage(string name, string message)
        {
            await Clients.All.SendAsync(name, message);
        }
    }
    }
}
