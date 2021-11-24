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
        public override MessageType MessageType { get; set; }
        public PlayerType PlayerType { get; set; }
        public TowerType TowerType { get; set; }
        public Point Coordinates { get; set; }

        public TowerMessage(string command, PlayerType playerType, TowerType towerType, Point coordinates, MessageType messageType) : base(command, messageType)
        {
            PlayerType = playerType;
            TowerType = towerType;
            Coordinates = coordinates;
        }
    }
}
