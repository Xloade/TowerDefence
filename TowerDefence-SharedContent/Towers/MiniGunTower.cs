using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class MiniGunTower : Tower
    {
        public override int Level { get; set; }
        public override int[] Price { get; set; }
        public override Point Coordinates { get; set; }
        public override int[] Range { get; set; }
        public override double[] RateOfFire { get; set; }
        public override string Sprite { get; set; }
        public override List<Ammunition> Ammunition { get; set; }
        public override TowerType TowerType { get; set; }
        public override int ShootingCooldown { get; set; }
        public MiniGunTower(PlayerType playerType, TowerType towerType, Point coordinates) : base(playerType, towerType, coordinates)
        {
            Price = new int[] { 10, 25, 40 };
            Range = new int[] { 400, 400, 400 };
            Power = new int[] { 5, 10, 15 };
            RateOfFire = new double[] { 10, 15, 20, };
            TowerType = towerType;
            canShootAlgorithm = new CanMiniGunShoot(Level, Coordinates, Range);
        }

        public MiniGunTower(int level, int[] price, Point coordinates, int[] range, int[] power, double[] rateOfFire,
            string sprite, List<Ammunition> ammunition, TowerType towerType, int shootingCooldown) : base(level, price, coordinates, range, power, rateOfFire,
            sprite, ammunition, towerType, shootingCooldown)
        {
            Level = level;
            Price = price;
            Coordinates = coordinates;
            Range = range;
            Power = power;
            RateOfFire = rateOfFire;
            Sprite = sprite;
            Ammunition = ammunition;
            TowerType = towerType;
            ShootingCooldown = shootingCooldown;
        }
    }
}
