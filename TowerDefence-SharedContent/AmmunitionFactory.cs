using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class AmmunitionFactory : GameElementFactory
    {
        public override Tower CreateTower(PlayerType playerType, TowerType towerType)
        {            
            throw new NotImplementedException();
        }

        public override ShootAlgorithm CreateAmmunition(Point towerCoordinates, AmmunitionType ammunitionType)
        {
            return ammunitionType switch
            {
                AmmunitionType.Bullet => new Bullet(towerCoordinates, ammunitionType),
                AmmunitionType.Rocket => new Rocket(towerCoordinates, ammunitionType),
                AmmunitionType.Laser => new Laser(towerCoordinates, ammunitionType),
                _ => null,
            };
        }
    }
}
