using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Memento
{
    public class MementoPlayer
    {
        public int SoldierCurrency { get; set; }
        public PlayerType PlayerType { get; set; }

        public MementoPlayer(PlayerType playerType, int soldierCurrency)
        {
            SoldierCurrency = soldierCurrency;
            PlayerType = playerType;
        }

        public bool getSoldierCurency(Player player)
        {
            if (PlayerType == player.PlayerType)
            {
                player.setSoldierCurency(this.SoldierCurrency);
                return true;
            }
            else return false;
        }

        private int getSoldierCurency()
        {
            return SoldierCurrency;
        }
    }
}
