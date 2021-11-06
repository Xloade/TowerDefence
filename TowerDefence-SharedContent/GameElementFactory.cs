using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public abstract class GameElementFactory
    {
        public abstract Tower CreateTower(PlayerType playerType, TowerType towerType);
    }
}
