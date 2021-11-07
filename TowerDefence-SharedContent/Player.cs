using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Player
    {
        public int Hitpoints { get; set; }
        public int TowerCurrency { get; set; }
        public int SoldierCurrency { get; set; }

        public PlayerType PlayerType { get; set; }

        public List<Soldier> soldiers;
        public List<Tower> towers;

        public Player(PlayerType playerType)
        {
            soldiers = new List<Soldier>();
            towers = new List<Tower>();
            this.PlayerType = playerType;
        }

        public Player(int hitpoints, int towerCurrency, int soldierCurrency, PlayerType playerType, List<Soldier> soldiers, List<Tower> towers)
        {
            Hitpoints = hitpoints;
            TowerCurrency = towerCurrency;
            SoldierCurrency = soldierCurrency;
            PlayerType = playerType;
            this.soldiers = soldiers;
            this.towers = towers;
        }

        public void UpdateSoldierMovement()
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                soldiers[i].MoveForward(PlayerType);
                if (soldiers[i].IsOutOfMap(PlayerType))
                {
                    soldiers.Remove(soldiers[i]);
                    i--;
                }
            }
        }

        public void UpdateTowerActivity(List<Soldier> enemySoldiers)
        {
            foreach(Tower tower in towers)
            {
                tower.MoveAmmunition(PlayerType);
                tower.Scan(enemySoldiers, PlayerType);
            }
        }
    }
}
