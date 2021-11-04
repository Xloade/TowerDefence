﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public interface IMapObserver
    {
        void AddPlayer(PlayerType playerType);
        void AddSoldier(Soldier soldier, PlayerType playerType);
        void AddTower(Tower tower, PlayerType playerType);
        void UpdateSoldierMovement();
        void UpdateTowerActivity();
        string ToJson();
    }
}
