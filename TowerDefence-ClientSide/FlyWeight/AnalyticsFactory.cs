using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.FlyWeight
{
    public class AnalyticsFactory
    {
        private List<Analytics> analyticsList = new List<Analytics>();
        public Analytics GetAnalytics(AnalyticsType analyticsType)
        {
            var analytics = analyticsList.Find(analytics => analytics.AnalyticsType == analyticsType);
            if (analytics != null) return analytics;

            analytics = analyticsType switch
            {
                AnalyticsType.Speed => new SpeedAnalytics(),
                AnalyticsType.Memory => new MemoryAnalytics()
            };
            analyticsList.Add(analytics);
            return analytics;
        }
    }
}
