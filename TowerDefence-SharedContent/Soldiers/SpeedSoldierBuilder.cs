using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Soldiers
{
    public class SpeedSoldierBuilder : SoldierBuilder
    {
        public SpeedSoldierBuilder(PlayerType playerType, SoldierType soldierType, int level)
        {
            soldier = new Soldier(playerType, soldierType, level);
        }
        public override void BuildHitpoints()
        {
            soldier.Hitpoints = new double[] { 0.5, 1, 1.5 };
        }

        public override void BuildSpeed()
        {
            soldier.Speed = 7;
        }

        public override void BuildSprite(PlayerType playerType)
        {
            soldier.Sprite = SpritePaths.getSoldier(playerType);
        }
    }
}
