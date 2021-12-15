using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Proxy
{
    class UpgradeMessage : Message
    {
        public override string Command { get; set; }
        public override MessageType MessageType { get; set; }

        public List<IdableObject> Objects { get; set; }
        public PlayerType PlayerType { get; set; }
        public UpgradeMessage(string command, MessageType messageType, PlayerType playerType ,List<IdableObject> objects) : base(command, messageType)
        {
            Objects = objects;
            PlayerType = playerType;
        }
    }
}
