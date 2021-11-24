using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class RocketTower : Tower
    {
        public override int Level { get; set; }
        public override int[] Price { get; set; }
        public override int[] Range { get; set; }
        public override double[] RateOfFire { get; set; }
        public override List<Ammunition> Ammunition { get; set; }
        public override TowerType TowerType { get; set; }
        public override int ShootingCooldown { get; set; }
        public RocketTower(PlayerType playerType, TowerType towerType, Point coordinates) : base(playerType, towerType, coordinates)
        {
            Price = new int[] { 25, 50, 75 };
            Range = new int[] { 150, 250, 300 };
            Power = new int[] { 15, 20, 25 };
            RateOfFire = new double[] { 5, 6, 8 };
            TowerType = towerType;
            canShootAlgorithm = new CanRocketShoot(Level, Coordinates, Range);
        }

        public RocketTower(int level, int[] price, Point coordinates, int[] range, int[] power, double[] rateOfFire,
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
