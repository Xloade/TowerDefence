using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{ 
    public class DirectUpgrade
    {
        public PlayerType PlayerType { get; set; }
        public UpgradeType UpgradeType { get; set; }

        public DirectUpgrade(PlayerType playerType, UpgradeType upgradeType)
        {
            PlayerType = playerType;
            UpgradeType = upgradeType;
        }
    }
}
