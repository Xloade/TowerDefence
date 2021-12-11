using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Timers;

namespace TowerDefence_SharedContent.Towers.State
{
    public class ReloadingState : TowerState
    {
        private System.Timers.Timer Timer;
        public ReloadingState(Tower tower)
        {
            Tower = tower;
        }

        public ReloadingState(TowerState state) : this(state.Tower) { }

        public override void Shoot()
        {
            throw new NotImplementedException();
        }

        public override void Reload()
        {
            Timer = new System.Timers.Timer();
            Timer.Interval = 3000;
            Timer.Start();
            Timer.Elapsed += (object source, ElapsedEventArgs e) =>
            {
                Tower.ShotsFired = 0;
                StateChangeCheck();
            };
            Timer.Stop();
            Timer.Close();
        }

        public override void Cooldown()
        {
            throw new NotImplementedException();
        }

        public override void Check(ICanShootAlgorithm canShootAlgorithm, Point soldierCoordinates)
        {
            throw new NotImplementedException();
        }

        public override void StateChangeCheck() => Tower.State = new PrepareNextShotState(this);
    }
}
