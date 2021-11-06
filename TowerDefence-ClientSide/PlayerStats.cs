using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    public class PlayerStats : Stats
    {
        private PlayerStatsView ConsoleView;

        public PlayerStats(Player player)
        {
            ConsoleView = new PlayerStatsView(player.Hitpoints, player.TowerCurrency, player.SoldierCurrency);
        }
        public override int[] Show()
        {
            return ConsoleView.ShowAll();
        }

        public override int ShowParameter(PlayerStatsShowStatus consoleShowStatus)
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
