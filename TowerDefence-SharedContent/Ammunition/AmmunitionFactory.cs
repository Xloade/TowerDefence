using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class AmmunitionFactory : GameElementFactory
    {
        public override Towers.Tower CreateTower(PlayerType playerType, TowerType towerType, Point point)
        {            
            throw new NotImplementedException();
        }

        public override Ammunition CreateAmmunition(Point towerCoordinates, AmmunitionType ammunitionType, int power, PlayerType playerType)
        {
            return ammunitionType switch
            {
                AmmunitionType.Bullet => new Bullet(towerCoordinates, ammunitionType, power, playerType),
                AmmunitionType.Rocket => new Rocket(towerCoordinates, ammunitionType, power, playerType),
                AmmunitionType.Laser => new Laser(towerCoordinates, ammunitionType, power, playerType),
                _ => null,
            };
        }
    }
}
