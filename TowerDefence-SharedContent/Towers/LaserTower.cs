using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class LaserTower : Tower
    {
        public LaserTower(PlayerType playerType, TowerType towerType, Point coordinates) : base(playerType, towerType, coordinates)
        {
            Price = new int[] { 50, 80, 110 };
            Range = new int[] { 500, 600, 700 };
            Power = new int[] { 2, 3, 5 };
            RateOfFire = new double[] { 20, 20, 20 };
            TowerType = towerType;
            canShootAlgorithm = new CanLaserShoot();
        }

        public LaserTower(int level, int[] price, Point coordinates, int[] range, int[] power, double[] rateOfFire,
            string sprite, List<Ammunition> ammunition, TowerType towerType, int shootingCooldown, PlayerType playerType) : base(level, price, coordinates, range, power, rateOfFire,
            sprite, ammunition, towerType, shootingCooldown, playerType)
        {
        }
    }
}
