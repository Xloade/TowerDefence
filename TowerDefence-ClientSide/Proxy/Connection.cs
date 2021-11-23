using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerDefence_ClientSide.Proxy
{
    public class Connection : IConnection
    {
        private HubConnection HubConnection;
        private List<Message> Messages;
        private ConnectionState ConnectionState;

        public Connection(string serverUrl)
        {
            HubConnection = new HubConnectionBuilder().WithUrl(serverUrl).Build();
            Messages = new List<Message>();
            ConnectionState = ConnectionState.Connected;
        }

        public void SendMessage(Message message)
        {
            switch(ConnectionState)
            {
                case ConnectionState.Connected:
                    break;
                case ConnectionState.Disconnected:
                    break;
            }
        }

        public HubConnection GetConnection() => HubConnection;     
    }
}
