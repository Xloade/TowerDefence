using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class MinigunTower : Tower
    {
        public override int Level { get; set; }
        public override int[] Price { get; set; }
        public override Point Coordinates { get; set; }
        public override int[] Range { get; set; }
        public override int[] Power { get; set; }
        public override double[] RateOfFire { get; set; }
        public override string Sprite { get; set; }
        public override List<Bullet> Bullets { get; set; }

        public MinigunTower(PlayerType type, Point coordinates) : base(type, coordinates)
        {
            Price = new int[] { 5, 10, 15 };
            Range = new int[] { 5, 10, 15 };
            RateOfFire = new double[] { 5, 10, 15 };
        }
    }
}
