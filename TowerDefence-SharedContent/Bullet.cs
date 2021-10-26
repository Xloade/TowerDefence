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

        public void Fly(PlayerType playerType)
        {
            switch(playerType)
            {
                case PlayerType.PLAYER1:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X + Speed), Coordinates.Y);
                    break;
                case PlayerType.PLAYER2:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X - Speed), Coordinates.Y);
                    break;
            }
        }
    }
}
