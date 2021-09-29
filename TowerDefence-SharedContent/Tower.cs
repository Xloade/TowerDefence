using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent
{
    class Tower
    {
        public int Level { get; set; }
        public int[] Price  { get; set; }
        public Point Coordinates { get; set; }
        public double[] Range { get; set; }
        public int[] Power { get; set; }
        public double[] RateOfFire { get; set; }
        public Bitmap Sprite { get; set; }
    }
}
