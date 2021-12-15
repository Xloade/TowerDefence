using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    interface IUpgradable
    {
        public void Upgrade();
        public bool isUpgrable { get; }
        public int UpgradePrice { get; }
    }
}
