using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Interpreter
{
    public class ElementTypeExpression : NonTerminalExpression
    {
        public override List<string> ElementTypes()
        {
            return new List<string>
            {
                "hitpoints",
                "speed",
                "minigun",
                "laser",
                "rocket",
            };
        }
    }
}
