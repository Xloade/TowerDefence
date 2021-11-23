using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerDefence_ClientSide.Proxy
{
    public class ServerConnection : IConnection
    {
        private Connection Connection;

        public ServerConnection(string serverUrl)
        {
            Connection = new Connection(serverUrl);
        }
        public HubConnection GetConnection() => Connection.GetConnection();

        public void SendMessage(Message message) => Connection.SendMessage(message);
    }
}
