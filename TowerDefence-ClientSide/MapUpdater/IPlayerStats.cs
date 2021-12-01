using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide
{
    interface IPlayerStats
    {
        public string LifePointsText { set; }
        public string TowerCurrencyText { set; }
        public string SoldierCurrencyText { set; }
        public PlayerStatsShowStatus PlayerStatsShowStatus { get; }

    }
}
