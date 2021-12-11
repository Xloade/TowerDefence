using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Proxy
{
    public class Connection : IConnection
    {
        private readonly HubConnection HubConnection;
        private readonly List<Message> PendingMessages;

        public Connection(string serverUrl)
        {
            HubConnection = new HubConnectionBuilder().WithUrl(serverUrl).Build();
            PendingMessages = new List<Message>();

            HubConnection.Closed += async (error) =>
            {
                MyConsole.WriteLineWithCount("Proxy: Connection closed");
                await Task.Delay(500);
                await HubConnection.StartAsync();
            };
            HubConnection.Reconnected += connectionId =>
            {
                MyConsole.WriteLineWithCount("Proxy: Reconnected");
                for (var i = 0; i < PendingMessages.Count; i++)
                {
                    Send(PendingMessages[i]);
                    PendingMessages.RemoveAt(i);
                    i--;
                }
                return Task.CompletedTask;
            };
        }

        public void SendMessage(Message message)
        {
            try
            {
                Send(message);
            }
            catch (Exception)
            {
                PendingMessages.Add(message);
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
                case MessageType.TowerDelete:
                    var deleteTowerMessage = (TowerMessage)message;
                    HubConnection.SendAsync(deleteTowerMessage.Command, deleteTowerMessage.PlayerType);
                    break;
                case MessageType.Soldier:
                    var soldierMessage = (SoldierMessage)message;
                    HubConnection.SendAsync(soldierMessage.Command, soldierMessage.PlayerType, soldierMessage.SoldierType);
                    break;
                case MessageType.SoldierUpgrade:
                    var updaSoldierMessage = (SoldierMessage)message;
                    HubConnection.SendAsync(updaSoldierMessage.Command, updaSoldierMessage.PlayerType);
                    break;
                case MessageType.Map:
                    var mapMessage = (MapMessage)message;
                    HubConnection.SendAsync(mapMessage.Command, mapMessage.MapType);
                    break;
                case MessageType.Player:
                    var playerMessage = (PlayerMessage)message;
                    HubConnection.SendAsync(playerMessage.Command, playerMessage.PlayerType);
                    break;
                case MessageType.RestartGame:
                    var restartMessage = (PlayerMessage)message;
                    HubConnection.SendAsync(restartMessage.Command);
                    break;
            }
        }
    }
}
