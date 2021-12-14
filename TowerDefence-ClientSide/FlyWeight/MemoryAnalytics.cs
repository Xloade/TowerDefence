using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.FlyWeight
{
    public class MemoryAnalytics : Analytics
    {
        public MemoryAnalytics()
        {
            AnalyticsType = AnalyticsType.Memory;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Finish()
        {
            throw new NotImplementedException();
        }
    }
}
