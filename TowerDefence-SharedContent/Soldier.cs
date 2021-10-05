using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public class Soldier
    {
        public int Level { get; set; }
        public int[] UpgradePrice { get; set; }
        public int[] BuyPrice { get; set; }
        public double Speed { get; set; }
        public double[] Hitpoints { get; set; }
    }
}
