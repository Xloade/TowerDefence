using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    public class PlayerStatsView
    {
        private readonly int Lifepoints;
        private readonly int TowerCurrency;
        private readonly int SoldierCurrency;

        public PlayerStatsView(int lifepoints, int towerCurrency, int soldierCurrency)
        {
            Lifepoints = lifepoints;
            TowerCurrency = towerCurrency;
            SoldierCurrency = soldierCurrency;
        }

        public PlayerStatsView()
        {

        }

        public int ShowLifepoints()
        {
            MyConsole.WriteLineWithCount("Adapter: Get Lifepoints");
            return Lifepoints;
        }

        public int ShowTowerCurrency()
        {
            MyConsole.WriteLineWithCount("Adapter: Get Tower Currency");
            return TowerCurrency;
        }

        public int ShowSoldierCurrency()
        {
            MyConsole.WriteLineWithCount("Adapter: Get Soldier Currency");
            return SoldierCurrency;
        }

        public int[] ShowAll()
        {
            MyConsole.WriteLineWithCount("Adapter: Get All");
            return new int[] { Lifepoints, TowerCurrency, SoldierCurrency };
        }
    }
}
