using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers.State
{
    public class PrepareNextShotState : TowerState, IStateChange
    {
        public PrepareNextShotState(Tower tower)
        {
            Tower = tower;
        }

        public PrepareNextShotState(TowerState state) : this(state.Tower) { }
        public override void Cooldown()
        {
            throw new NotImplementedException();
        }

        public override void Check(ICanShootAlgorithm canShootAlgorithm, Point soldierCoordinates)
        {
            MyConsole.WriteLineWithCount("----- State: prepare next shot state -----");
            if (canShootAlgorithm.CanShoot(soldierCoordinates)) OnStateChange();
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        public override void Shoot()
        {
            throw new NotImplementedException();
        }

        public void OnStateChange() => Tower.State = new ShootingState(this);
    }
}
