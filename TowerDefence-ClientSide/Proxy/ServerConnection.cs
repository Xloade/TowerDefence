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
        public event MessageTransferedHandler MessageTransferedHandler;

        public ServerConnection(string serverUrl)   
        {
            Connection = new Connection(serverUrl);
            Connection.MessageReceivedCallback += new MessageReceivedHandler(GetReceivedMessage);
        }

        private void GetReceivedMessage(string message)
        {
            MessageTransferedHandler(message);
        }

        public HubConnection GetConnection() => Connection.GetConnection();


        public void SubscribeToServer() => Connection.SubscribeToServer();

        public void UnsubscribeFromServer() => Connection.UnsubscribeFromServer();

        public void SendMessage(Message message) => Connection.SendMessage(message);
    }
}
