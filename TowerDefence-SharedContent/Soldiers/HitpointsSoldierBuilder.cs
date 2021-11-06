using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Soldiers
{
    public class HitpointsSoldierBuilder : SoldierBuilder
    {
        public HitpointsSoldierBuilder(PlayerType playerType, SoldierType soldierType, int level)
        {
            soldier = new Soldier(playerType, soldierType, level);
        }

        public override void BuildHitpoints()
        {
            soldier.Hitpoints = new double[] { 1, 2, 3 };
        }

        public override void BuildSpeed()
        {
            soldier.Speed = 5;
        }

        public override void BuildSprite(PlayerType playerType)
        {
            soldier.Sprite = SpritePaths.getSoldier(playerType, soldier.SoldierType);
        }
    }
}
