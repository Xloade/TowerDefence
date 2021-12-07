using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class RocketTower : Tower
    {
        public RocketTower(PlayerType playerType, TowerType towerType, Point coordinates) : base(playerType, towerType, coordinates)
        {
            Price = new int[] { 25, 50, 75 };
            Range = new int[] { 150, 250, 300 };
            Power = new int[] { 15, 20, 25 };
            RateOfFire = new double[] { 5, 6, 8 };
            TowerType = towerType;
            CanShootAlgorithm = new CanRocketShoot(Level, Coordinates, Range);
        }

        public RocketTower(int level, int[] price, Point coordinates, int[] range, int[] power, double[] rateOfFire,
            string sprite, List<Ammunition> ammunition, TowerType towerType, int shootingCooldown, PlayerType playerType) : base(level, price, coordinates, range, power, rateOfFire,
            sprite, ammunition, towerType, shootingCooldown, playerType)
        {
        }
    }
}
