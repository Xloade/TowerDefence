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
            stopwatch.Start();
        }

        public override void Finish()
        {
            stopwatch.Stop();
            MyConsole.WriteLineWithCount("Flyweight: Rendering speed: " + stopwatch.Elapsed);
            stopwatch.Reset();
        }
    }
}
