using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Observers
{
    public interface ISoldierObserver
    {
        void Move();
        bool IsOutOfMap();
        void NotifyServer(HubConnection connection, string message);
    }
}
