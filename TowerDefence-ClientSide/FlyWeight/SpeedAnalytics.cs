using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.FlyWeight
{
    public class SpeedAnalytics : Analytics
    {
        private Stopwatch stopwatch;
        public SpeedAnalytics()
        {
            AnalyticsType = AnalyticsType.Speed;
            stopwatch = new Stopwatch();
        }

        public override void Start()
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        public override void Finish()
        {
            stopwatch.Stop();
            Result = stopwatch.ElapsedTicks.ToString();
            MyConsole.WriteLineWithCount("Flyweight: speed test");
        }
    }
}
