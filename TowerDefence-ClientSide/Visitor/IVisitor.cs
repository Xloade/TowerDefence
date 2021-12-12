using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Visitor
{
    public interface IVisitor
    {
        void Visit(Element element);
    }
}