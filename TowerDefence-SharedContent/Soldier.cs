using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_SharedContent
{
    public class Soldier
    {
        public int Level { get; set; }
        public int[] UpgradePrice { get; set; }
        public int[] BuyPrice { get; set; }
        public double Speed { get; set; }
        public double[] Hitpoints { get; set; }
        public Point Coordinates { get; set; }
        public string Sprite { get; set; }

        public Soldier(PlayerType type, int level)
        {

            Coordinates = type==PlayerType.PLAYER1 ? new Point(0, 450) : new Point(1000, 450);
            Sprite = SpritePaths.getSoldier(type);
            Speed = 5;
            Level = level;
        }
    }
}
