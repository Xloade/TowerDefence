using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.FlyWeight
{
    public class MemoryAnalytics : Analytics
    {
        private long BeforeKBUsed;
        private long AfterKBUsed;
        public MemoryAnalytics()
        {
            AnalyticsType = AnalyticsType.Memory;
        }

        public override void Start()
        {
            BeforeKBUsed = GC.GetTotalMemory(false) / 1024;
        }

        public override void Finish()
        {
            AfterKBUsed = GC.GetTotalMemory(false) / 1024;
            Result = (AfterKBUsed - BeforeKBUsed).ToString();
            MyConsole.WriteLineWithCount("Flyweight: memory test");
        }
    }
}
