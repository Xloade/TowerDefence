using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Player
    {
        public PlayerType PlayerType { get; set; }
        public int Hitpoints { get; set; }
        public int TowerCurrency { get; set; }
        public int SoldierCurrency { get; set; }

        public List<Soldier> soldiers;
        public List<Tower> towers;

        public Player(PlayerType playerType)
        {
            PlayerType = playerType;
            soldiers = new List<Soldier>();
            towers = new List<Tower>();
        }
    }
}
