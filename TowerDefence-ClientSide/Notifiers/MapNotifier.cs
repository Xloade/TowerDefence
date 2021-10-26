using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Notifiers
{
    public class MapNotifier 
    {
        public void SendMessage(HubConnection connection, string message, PlayerType playerType)
        {
            connection.SendAsync(message, playerType);
        }
    }
}
