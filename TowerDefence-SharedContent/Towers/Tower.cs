using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public abstract class Tower
    {
        public abstract int Level { get; set; }
        public abstract int[] Price  { get; set; }
        public abstract Point Coordinates { get; set; }
        public abstract int[] Range { get; set; }
        public abstract int[] Power { get; set; }
        public abstract double[] RateOfFire { get; set; }
        public abstract string Sprite { get; set; }
        public abstract List<Bullet> Bullets { get; set; }

        protected Tower(PlayerType type, Point coordinates)
        {
            Level = 0;
            Coordinates = coordinates;
            Sprite = SpritePaths.getTower(type);
            Bullets = new List<Bullet>();
        }
    }
}
