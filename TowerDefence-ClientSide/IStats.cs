using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide
{
    public interface IStats
    {
        int[] Show();
        int ShowParameter(PlayerStatsShowStatus consoleShowStatus);
    }
}
