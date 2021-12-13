using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Visitor
{
    public class SpeedUpgradeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            var upgrade = (Upgrade)element;
            upgrade.UpgradeType = UpgradeType.Speed;

            MyConsole.WriteLineWithCount("Visitor: SpeedUpgradeVisitor");
        }
    }
}
