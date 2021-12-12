using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Visitor
{
    public abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
    }
}
