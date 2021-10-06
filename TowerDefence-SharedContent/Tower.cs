using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent
{
    public class Tower
    {
        public int Level { get; set; }
        public int[] Price  { get; set; }
        public Point Coordinates { get; set; }
        public double[] Range { get; set; }
        public int[] Power { get; set; }
        public double[] RateOfFire { get; set; }
        public string Sprite { get; set; }

        public Tower(PlayerType type)
        {
            Coordinates = new Point(200, 450);
            Sprite = SpritePaths.getTower(type);
        }
    }
}
