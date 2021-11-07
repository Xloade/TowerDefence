using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide
{
    public interface IStats
    {
        public int[] Show();

        public int ShowParameter(PlayerStatsShowStatus consoleShowStatus);
    }
}
