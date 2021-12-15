using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public abstract class GameElementFactory
    {
        public abstract Towers.Tower CreateTower(PlayerType playerType, TowerType towerType, Point coordinates);
        public abstract Ammunition.Ammunition CreateAmmunition(Point towerCoordinates, AmmunitionType ammunitionType, int power, PlayerType playerType);
    }
}
