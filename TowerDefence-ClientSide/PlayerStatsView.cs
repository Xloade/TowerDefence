using System;
using System.Collections.Generic;
using System.Text;

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
            return Lifepoints;
        }

        public int ShowTowerCurrency()
        {
            return TowerCurrency;
        }

        public int ShowSoldierCurrency()
        {
            return SoldierCurrency;
        }

        public int[] ShowAll()
        {
            return new int[] { Lifepoints, TowerCurrency, SoldierCurrency };
        }
    }
}
