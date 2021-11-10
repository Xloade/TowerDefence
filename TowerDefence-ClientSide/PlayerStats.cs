using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    public class PlayerStats : IStats
    {
        private PlayerStatsView ConsoleView;

        public PlayerStats(Player player)
        {
            ConsoleView = new PlayerStatsView(player.Hitpoints, player.TowerCurrency, player.SoldierCurrency);
        }
        public int[] Show()
        {
            return ConsoleView.ShowAll();
        }

        public int ShowParameter(PlayerStatsShowStatus consoleShowStatus)
        {
            return consoleShowStatus switch
            {
                PlayerStatsShowStatus.Lifepoints => ConsoleView.ShowLifepoints(),
                PlayerStatsShowStatus.TowerCurrency => ConsoleView.ShowTowerCurrency(),
                PlayerStatsShowStatus.SoldierCurrency => ConsoleView.ShowSoldierCurrency(),
                _ => 0,
            };
        }
    }
}
