using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public class Player
    {
        public int Hitpoints { get; set; }
        public int TowerCurrency { get; set; }
        public int SoldierCurrency { get; set; }

        public List<Soldier> soldiers;
        public List<Tower> towers;

        public Player()
        {
            soldiers = new List<Soldier>();
            towers = new List<Tower>();
        }
    }
}
