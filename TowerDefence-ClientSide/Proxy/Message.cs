using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerDefence_ClientSide.Proxy
{
    public abstract class Message
    {
        public abstract string Command { get; set; }
        public abstract MessageType MessageType { get; set; }

        public Message(string command, MessageType messageType)
        {
            Command = command;
        }
    }
}
