using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent
{
    public class Bullet
    {
        public Point Coordinates { get; set; }
        public string Sprite { get; set; }
        public int Speed { get; set; }

        public Bullet(Point towerCoordinates)
        {
            Coordinates = towerCoordinates;
            Sprite = SpritePaths.getBullet();
            Speed = 5;
        }
    }
}
