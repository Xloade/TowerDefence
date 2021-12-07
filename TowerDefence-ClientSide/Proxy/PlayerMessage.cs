using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Proxy
{
    public class PlayerMessage : Message
    {
        public override string Command { get; set; }
        public override MessageType MessageType { get; set; }
        public PlayerType PlayerType { get; set; }

        public PlayerMessage(string command, MessageType messageType, PlayerType playerType) : base(command, messageType)
        {
            Command = command;
            MessageType = messageType;
            PlayerType = playerType;
        }

        public PlayerMessage(string command, MessageType messageType) : base(command, messageType) {}
    }
}
