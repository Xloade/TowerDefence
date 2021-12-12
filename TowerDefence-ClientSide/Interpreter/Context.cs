using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Interpreter
{
    public class Context
    {
        public Context(string input)
        {
            Input = input;
        }
        public string Input { get; set; }
        public string CommandName { get; set; }
        public string ElementName { get; set; }
        public string ElementType { get; set; }

        public bool IsEmpty() => CommandName == null || ElementName == null || ElementType == null;
    }
}
