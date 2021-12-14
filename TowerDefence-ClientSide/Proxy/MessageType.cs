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
        Upgrade,
        Map,
        Player,
        RestartGame
    }
}
