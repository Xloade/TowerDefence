using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers.State
{
    public class PrepareNextShotState : TowerState
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
            if (canShootAlgorithm.CanShoot(soldierCoordinates)) StateChangeCheck();
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        public override void Shoot()
        {
            throw new NotImplementedException();
        }

        public override void StateChangeCheck() => Tower.State = new ShootingState(this);
    }
}
