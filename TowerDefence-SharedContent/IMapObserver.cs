using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public interface IMapObserver
    {
        void AddPlayer(PlayerType playerType);
        void AddSoldier(Soldier soldier, PlayerType playerType);
        void AddTower(Towers.Tower tower, PlayerType playerType);
        void UpdateSoldierMovement();
        void UpdateTowerActivity();
        string ToJson();
        void Restart();
        void UpgradeTowers(PlayerType playerType, List<IdableObject> towers);
        void UpgradeSoldiers(PlayerType playerType, List<IdableObject> soldier);
    }
}
