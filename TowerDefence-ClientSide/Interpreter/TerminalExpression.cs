using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Interpreter
{
    public class TerminalExpression : Expression
    {
        public virtual string Qualify(string input) => "";

        public override void Interpret(Context context)
        {
            if (context.Input.Length == 0 || context.CommandName != null) return;
            context.CommandName = Qualify(context.Input);
        }
    }
}
