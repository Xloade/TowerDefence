using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Interpreter
{
    public class NonTerminalExpression : Expression
    {
        public virtual List<string> Elements() => new List<string>();
        public virtual List<string> ElementTypes() => new List<string>();
        public override void Interpret(Context context)
        {
            if (Elements().Count > 0)
            {
                Elements().ForEach(element =>
                {
                    if (context.Input.Contains(element))
                    {
                        context.ElementName = element;
                    }
                });
            }

            if (ElementTypes().Count > 0)
            {
                ElementTypes().ForEach(type =>
                {
                    if (context.Input.Contains(type))
                    {
                        context.ElementType = type;
                    }
                });
            }
        }
    }
}
