using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Proxy
{
    public class MapMessage : Message
    {
        public override string Command { get; set; }
        public override MessageType MessageType { get; set; }
        public string MapType { get; set; }

        public MapMessage(string command, MessageType messageType, string mapType) : base(command, messageType)
        {
            Command = command;
            MessageType = messageType;
            MapType = mapType;
        }
    }
}
