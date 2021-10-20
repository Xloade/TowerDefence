using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class LaserTower : Tower
    {
        public override int Level { get; set; }
        public override int[] Price { get; set; }
        public override Point Coordinates { get; set; }
        public override int[] Range { get; set; }
        public override int[] Power { get; set; }
        public override double[] RateOfFire { get; set; }
        public override string Sprite { get; set; }
        public override List<Bullet> Bullets { get; set; }

        public LaserTower(PlayerType type, Point coordinates) : base(type, coordinates)
        {
            Price = new int[] { 50, 100, 150 };
            Range = new int[] { 50, 100, 150 };
            RateOfFire = new double[] { 1, 3, 5 };
        }
    }
}
