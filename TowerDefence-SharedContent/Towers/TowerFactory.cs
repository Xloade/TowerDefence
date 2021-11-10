using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class TowerFactory : GameElementFactory
    {
        public override Tower CreateTower(PlayerType playerType, TowerType towerType, Point coordinates)
        {
            MyConsole.WriteLineWithCount($"Abstract Factory: Create Tower {towerType}");
            return towerType switch
            {                
                TowerType.Minigun => new MiniGunTower(playerType, towerType, coordinates),
                TowerType.Rocket => new RocketTower(playerType, towerType, coordinates),
                TowerType.Laser => new LaserTower(playerType, towerType, coordinates),
                _ => null,
            };
        }

        public override ShootAlgorithm CreateAmmunition(Point towerCoordinates, AmmunitionType ammunitionType, int Power)
        {
            throw new NotImplementedException();
        }
    }
}
