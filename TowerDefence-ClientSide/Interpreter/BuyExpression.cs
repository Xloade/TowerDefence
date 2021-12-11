using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefence_ClientSide.Interpreter
{
    public class BuyExpression : TerminalExpression
    {
        public override string Qualify(string input)
        {
            return input.StartsWith("buy") ? "buy" : "";
        }
    }
}
