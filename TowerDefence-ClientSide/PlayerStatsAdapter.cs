using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    public class PlayerStatsAdapter : IStats
    {
        private PlayerStatsView ConsoleView;

        public PlayerStatsAdapter(Player player)
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
