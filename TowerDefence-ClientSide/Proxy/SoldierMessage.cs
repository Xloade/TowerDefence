using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;

namespace TowerDefence_ClientSide.Proxy
{
    public class SoldierMessage : Message
    {
        public override string Command { get; set; }
        public override MessageType MessageType { get; set; }
        public PlayerType PlayerType { get; set; }
        public SoldierType SoldierType { get; set; }

        public SoldierMessage(string command, MessageType messageType, PlayerType playerType, SoldierType soldierType) : base(command, messageType)
        {
            PlayerType = playerType;
            SoldierType = soldierType;
        }

        public SoldierMessage(string command, MessageType messageType, PlayerType playerType) : base(command, messageType)
        {
            PlayerType = playerType;
        }
    }
}
