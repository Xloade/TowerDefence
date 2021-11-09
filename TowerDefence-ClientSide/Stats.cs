using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide
{
    public class Stats
    {
        public virtual int[] Show()
        {
            return new int[0];
        }

        public virtual int ShowParameter(PlayerStatsShowStatus consoleShowStatus)
        {
            return 0;
        }
    }
}
