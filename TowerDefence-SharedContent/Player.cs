using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;
using TowerDefence_SharedContent.Memento;

namespace TowerDefence_SharedContent
{
    public class Player
    {
        public int Hitpoints { get; set; }
        public int TowerCurrency { get; set; }
        public int SoldierCurrency { get; set; }
        public PlayerType PlayerType { get; set; }
        public List<Tower> Towers { get; set; }
        public List<Soldier> Soldiers { get; set; }

        public Player(PlayerType playerType)
        {
            Hitpoints = 5;
            TowerCurrency = 250;
            SoldierCurrency = 700;
            Soldiers = new List<Soldier>();
            Towers = new List<Towers.Tower>();
            this.PlayerType = playerType;
        }

        public Player(int hitpoints, int towerCurrency, int soldierCurrency, PlayerType playerType, List<Soldier> soldiers, List<Towers.Tower> towers)
        {
            Hitpoints = hitpoints;
            TowerCurrency = towerCurrency;
            SoldierCurrency = soldierCurrency;
            PlayerType = playerType;
            this.Soldiers = soldiers;
            this.Towers = towers;
        }

        public void UpdateSoldierMovement()
        {
            for (int i = 0; i < Soldiers.Count; i++)
            {
                Soldiers[i].MoveForward(PlayerType);
                if (Soldiers[i].IsOutOfMap(PlayerType))
                {
                    Soldiers.RemoveAt(i);
                    i--;
                }
            }
        }

        public void UpdateTowerActivity(List<Soldier> enemySoldiers)
        {
            foreach (var tower in Towers)
            {
                tower.MoveAmmunition(PlayerType);
                tower.Scan(enemySoldiers, PlayerType);
            }
        }

        public void setSoldierCurency(int soldierCurrency)
        {
            SoldierCurrency = soldierCurrency;
        }

        public MementoPlayer saveSoldierCurency()
        {
            return new MementoPlayer(PlayerType, SoldierCurrency);
        }
    }
}
