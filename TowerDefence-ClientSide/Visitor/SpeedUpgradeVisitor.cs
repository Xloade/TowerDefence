using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Visitor
{
    public class SpeedUpgradeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            var upgrade = (Upgrade)element;

            upgrade.UpgradeType = UpgradeType.Speed;
        }
    }
}
