﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class TowerFactory : GameElementFactory
    {
        public override Tower CreateTower(PlayerType playerType, TowerType towerType)
        {
            return towerType switch
            {
                TowerType.Minigun => new MiniGunTower(playerType, towerType),
                TowerType.Rocket => new RocketTower(playerType, towerType),
                TowerType.Laser => new LaserTower(playerType, towerType),
                _ => null,
            };
        }
    }
}