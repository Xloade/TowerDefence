using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_SharedContent.Soldiers
{
    public abstract class Soldier
    {
        public abstract int Level { get; set; }
        public abstract int[] UpgradePrice { get; set; }
        public abstract int[] BuyPrice { get; set; }
        public abstract double Speed { get; set; }
        public abstract double[] Hitpoints { get; set; }
        public abstract Point Coordinates { get; set; }
        public abstract string Sprite { get; set; }

        protected Soldier(PlayerType type, Point coordinates)
        {

            Coordinates = coordinates;
            Sprite = SpritePaths.getSoldier(type);
        }
    }
}
