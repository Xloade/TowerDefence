using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Soldiers
{
    public class Barrack
    {
        public void Train(SoldierBuilder soldierBuilder, PlayerType playerType)
        {
            soldierBuilder.BuildHitpoints();
            soldierBuilder.BuildSpeed();
            soldierBuilder.BuildSprite(playerType);
        }
    }
}
