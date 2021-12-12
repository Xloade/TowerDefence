using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Interpreter
{
    public abstract class Expression
    {
        public abstract void Interpret(Context context);
    }
}
