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
        public int[] Range { get; set; }
        public int[] Power { get; set; }
        public double[] RateOfFire { get; set; }
        public string Sprite { get; set; }
        public List<Bullet> Bullets { get; set; }

        public Tower(PlayerType type)
        {
            Level = 0;
            Range = new int[] { 100, 200, 300, 350};
            Coordinates = new Point(200, 450);
            Sprite = SpritePaths.getTower(type);
            Bullets = new List<Bullet>();
            //Bullets.Add(new Bullet(Coordinates));
        }
    }
}
