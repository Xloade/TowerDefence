using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;

namespace TowerDefence_ClientSide.Proxy
{
    public class Connection : IConnection
    {
        private HubConnection HubConnection;
        private List<Message> PendingMessages;
        public event MessageReceivedHandler MessageReceivedCallback;

        public Connection(string serverUrl)
        {
            HubConnection = new HubConnectionBuilder().WithUrl(serverUrl).Build();
            PendingMessages = new List<Message>();
        }

        public void SendMessage(Message message)
        {
            switch(HubConnection.State)
            {
                case HubConnectionState.Connected:
                    if(PendingMessages.Count > 0)
                    {
                       for(int i = 0; i < PendingMessages.Count; i++)
                       {
                            Send(PendingMessages[i]);
                            PendingMessages.RemoveAt(i);
                            i--;
                       }
                    } else
                    {
                        Send(message);
                    }
                    break;
                case HubConnectionState.Disconnected:
                    PendingMessages.Add(message);
                    break;
            }           
        }

        public HubConnection GetConnection() => HubConnection;

        private void Send(Message message)
        {
            switch (message.MessageType)
            {
                case MessageType.Tower:
                    var towerMessage = message as TowerMessage;
                    HubConnection.SendAsync(towerMessage.Command, towerMessage.PlayerType, towerMessage.TowerType, towerMessage.Coordinates);
                    break;
            }
        }

        public void SubscribeToServer()
        {
            HubConnection.On<string>("ReceiveMessage", (message) => {
                MessageReceivedCallback(message);
            });
            HubConnection.StartAsync();
        }

        public void UnsubscribeFromServer()
        {
            HubConnection.StopAsync();
        }
    }
}
