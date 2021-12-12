using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Visitor
{
    public class GunUpgradeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            var upgrade = (Upgrade) element;

            upgrade.UpgradeType = UpgradeType.Gun;
        }
    }
}
