using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Interpreter
{
    public class ElementExpression : NonTerminalExpression
    {
        public override List<string> Elements()
        {
            return new List<string>
            {
                "soldier",
            };
        }
    }
}
