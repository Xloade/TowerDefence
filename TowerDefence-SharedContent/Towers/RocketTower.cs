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
        public override Point Coordinates { get; set; }
        public override int[] Range { get; set; }
        public override int[] Power { get; set; }
        public override double[] RateOfFire { get; set; }
        public override string Sprite { get; set; }
        public override List<ShootAlgorithm> Ammunition { get; set; }
        public override TowerType TowerType { get; set; }
        public RocketTower(PlayerType playerType, TowerType towerType) : base(playerType, towerType)
        {
            Price = new int[] { 20, 30, 40 };
            Range = new int[] { 200, 300, 400 };
            Power = new int[] { 25, 35, 45 };
            RateOfFire = new double[] { 1, 2, 3, };
            TowerType = towerType;
        }
    }
}
