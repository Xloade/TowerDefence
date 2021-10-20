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
        public override List<Bullet> Bullets { get; set; }

        public RocketTower(PlayerType type, Point coordinates): base(type, coordinates)
        {
            Price = new int[] { 10, 20, 30 };
            Range = new int[] { 10, 20, 30 };
            RateOfFire = new double[] { 10, 20, 30 };
            Coordinates = type == PlayerType.PLAYER1 ? new Point(200, 400) : new Point(600, 400);
        }
    }
}
