using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace TowerDefence_SharedContent.Towers.State
{
    public abstract class TowerState
    {
        public Tower Tower { get; set; }

        public abstract void Shoot();
        public abstract void Reload();
        public abstract void Cooldown();
        public abstract void Check(ICanShootAlgorithm canShootAlgorithm, Point soldierCoordinates);
        public abstract void StateChangeCheck();
    }
}
