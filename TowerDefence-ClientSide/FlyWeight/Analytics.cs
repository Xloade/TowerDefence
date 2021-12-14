using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_ClientSide.FlyWeight
{
    public abstract class Analytics
    {
        protected string Result { get; set; }
        public AnalyticsType AnalyticsType { get; set; }
        public abstract void Start();
        public abstract void Finish();
    }
}
