using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Timers;
using Timer = System.Threading.Timer;

namespace TowerDefence_SharedContent.Towers.State
{
    public class OverheatState : TowerState
    {
        private System.Timers.Timer Timer;
        public OverheatState(Tower tower)
        {
            Tower = tower;
        }

        public OverheatState(TowerState state) : this(state.Tower) { }

        public override void Shoot()
        {
            throw new NotImplementedException();
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        public override void Cooldown()
        {
            MyConsole.WriteLineWithCount("----- State: cooling down state -----");
            Tower.IsOverheated = true;
            Timer = new System.Timers.Timer();
            Timer.Interval = 5000;
            Timer.Start();
            Timer.Elapsed += (object source, ElapsedEventArgs e) =>
            {
                Tower.OverheatLevel = 0;
                OnStateChange();
                Timer.Stop();
                Timer.Close();
            };
        }

        public void OnStateChange()
        {
            Tower.IsOverheated = false;
            Tower.State = new PrepareNextShotState(this);
        }

        public override void Check(ICanShootAlgorithm canShootAlgorithm, Point soldierCoordinates)
        {
            throw new NotImplementedException();
        }
    }
}
