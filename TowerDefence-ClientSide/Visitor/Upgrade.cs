using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Visitor
{
    public class Upgrade : Element
    {
        public UpgradeType UpgradeType { get; set; }
        public string Message { get; set; }

        public Upgrade(string message)
        {
            Message = message;
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
