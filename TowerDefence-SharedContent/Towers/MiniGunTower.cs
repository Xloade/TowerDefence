using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class MiniGunTower : Tower
    {
        public MiniGunTower(PlayerType playerType, TowerType towerType, Point coordinates) : base(playerType, towerType, coordinates)
        {
            Price = new int[] { 10, 25, 40 };
            Range = new int[] { 400, 400, 400 };
            Power = new int[] { 5, 10, 15 };
            RateOfFire = new double[] { 10, 15, 20, };
            TowerType = towerType;
            CanShootAlgorithm = new CanMiniGunShoot(Level, Coordinates, Range);
        }

        public MiniGunTower(int level, int[] price, Point coordinates, int[] range, int[] power, double[] rateOfFire,
            string sprite, List<Ammunition> ammunition, TowerType towerType, int shootingCooldown, PlayerType playerType) : base(level, price, coordinates, range, power, rateOfFire,
            sprite, ammunition, towerType, shootingCooldown, playerType)
        {
        }
    }
}
