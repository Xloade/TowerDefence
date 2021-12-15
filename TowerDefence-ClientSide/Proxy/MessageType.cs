using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Proxy
{
    public enum MessageType
    {
        Tower,
        TowerDelete,
        Soldier,
        Map,
        Player,
        RestartGame,
        Upgrade
    }
}
