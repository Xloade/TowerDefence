using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers.State
{
    public class OverheatState : TowerState
    {
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
            throw new NotImplementedException();
        }

        public override void StateChangeCheck()
        {
            throw new NotImplementedException();
        }

        public override void Check(ICanShootAlgorithm canShootAlgorithm, Point soldierCoordinates)
        {
            throw new NotImplementedException();
        }
    }
}
