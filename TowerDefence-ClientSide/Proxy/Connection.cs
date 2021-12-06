using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;

namespace TowerDefence_ClientSide.Proxy
{
    public class Connection : IConnection
    {
        private HubConnection HubConnection;
        private List<Message> PendingMessages;

        public Connection(string serverUrl)
        {
            HubConnection = new HubConnectionBuilder().WithUrl(serverUrl).Build();
            PendingMessages = new List<Message>();
        }

        public void SendMessage(Message message)
        {
            Console.WriteLine("Connection state: " + HubConnection.State);
            switch (HubConnection.State)
            {
                case HubConnectionState.Connecting:
                    PendingMessages.Add(message);
                    break;
                case HubConnectionState.Disconnected:
                    PendingMessages.Add(message);
                    break;
                case HubConnectionState.Connected:
                    if (PendingMessages.Count > 0)
                    {
                        for (int i = 0; i < PendingMessages.Count; i++)
                        {
                            Send(PendingMessages[i]);
                            PendingMessages.RemoveAt(i);
                            i--;
                        }
                    }
                    else
                    {
                        Send(message);
                    }
                    break;
            }
        }

        public HubConnection GetConnection() => HubConnection;

        private void Send(Message message)
        {           
            switch (message.MessageType)
            {
                case MessageType.Tower:
                    var towerMessage = (TowerMessage)message;
                    HubConnection.SendAsync(towerMessage.Command, towerMessage.PlayerType, towerMessage.TowerType, towerMessage.Coordinates);
                    break;
                case MessageType.Soldier:
                    var soldierMessage = (SoldierMessage)message;
                    HubConnection.SendAsync(soldierMessage.Command, soldierMessage.SoldierType, soldierMessage.PlayerType);
                    break;
                case MessageType.Map:
                    var mapMessage = (MapMessage)message;
                    HubConnection.SendAsync(mapMessage.Command, mapMessage.MapType);
                    break;
                case MessageType.Player:
                    var playerMessage = (PlayerMessage)message;
                    HubConnection.SendAsync(playerMessage.Command, playerMessage.PlayerType);
                    break;
            }
        }
    }
}
