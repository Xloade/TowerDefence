using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerDefence_ClientSide.Proxy
{
    public abstract class Message
    {
        public abstract string Command { get; set; }

        public Message(string command)
        {
            Command = command;
        }
    }
}
