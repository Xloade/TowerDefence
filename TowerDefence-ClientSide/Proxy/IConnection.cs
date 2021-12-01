using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerDefence_ClientSide.Proxy
{
    public interface IConnection
    {
        void SendMessage(Message message);
        HubConnection GetConnection();
    }
}
