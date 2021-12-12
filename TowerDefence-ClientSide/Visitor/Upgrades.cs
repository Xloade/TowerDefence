using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.Visitor
{
    public class Upgrades
    {
        public readonly List<Upgrade> upgrades = new List<Upgrade> {new SoldierUpgrade(), new TowerUpgrade()};
        public void Accept(IVisitor visitor)
        {
            Upgrade upgrade;
            if (visitor is GunUpgradeVisitor || visitor is RateOfFireUpgradeVisitor)
            {
                upgrade = upgrades.Find(upgr => upgr is TowerUpgrade);
            }
            else
            {
                upgrade = upgrades.Find(upgr => upgr is SoldierUpgrade);
            }

            upgrade?.Accept(visitor);
        }

    }
}
