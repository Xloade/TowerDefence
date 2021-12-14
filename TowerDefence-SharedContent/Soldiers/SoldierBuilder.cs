using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Soldiers
{
    public abstract class SoldierBuilder
    {
        protected Soldier soldier;

        public Soldier Soldier => soldier;

        public abstract void BuildSpeed();
        public abstract void BuildHitpoints();
        public abstract void BuildSprite(PlayerType playerType);
    }
}
