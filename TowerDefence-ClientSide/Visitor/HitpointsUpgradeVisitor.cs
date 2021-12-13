using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Visitor
{
    public class HitpointsUpgradeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            var upgrade = (Upgrade)element;
            upgrade.UpgradeType = UpgradeType.Hitpoints;

            MyConsole.WriteLineWithCount("Visitor: HitpointsUpgradeVisitor");
        }
    }
}
