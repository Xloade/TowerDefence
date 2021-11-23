using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide.Proxy
{
    public class TowerMessage : Message
    {
        public override string Command { get; set; }
        public PlayerType PlayerType { get; set; }
        public TowerType TowerType { get; set; }
        public Point Coordinates { get; set; }

        public TowerMessage(string command, PlayerType playerType, TowerType towerType, Point coordinates) : base(command)
        {
            PlayerType = playerType;
            TowerType = towerType;
            Coordinates = coordinates;
        }
    }
}
