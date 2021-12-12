using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;
using UpgradeType = TowerDefence_ClientSide.Visitor.UpgradeType;

namespace TowerDefence_ClientSide.Proxy
{
    public class UpgradeMessage : Message
    {
        public override string Command { get; set; }
        public override MessageType MessageType { get; set; }

        public UpgradeType UpgradeType { get; set; }
        public PlayerType PlayerType { get; set; }
        public UpgradeMessage(string command, MessageType messageType, UpgradeType upgradeType, PlayerType playerType) : base(command, messageType)
        {
            UpgradeType = upgradeType;
            PlayerType = playerType;
        }
    }
}
