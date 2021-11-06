using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public static class SpritePaths
    {
        public static string getTower(PlayerType playerType, TowerType towerType)
        {
            switch(towerType)
            {
                case TowerType.Minigun:
                    return playerType == PlayerType.PLAYER1 ? "../../../Sprites/bulletTower(Blue).png" : "../../../Sprites/bulletTower(Red).png";
                case TowerType.Rocket:
                    return playerType == PlayerType.PLAYER1 ? "../../../Sprites/rocketTower(Blue).png" : "../../../Sprites/rocketTower(Red).png";
                case TowerType.Laser:
                    return playerType == PlayerType.PLAYER1 ? "../../../Sprites/laserTower(Blue).png" : "../../../Sprites/laserTower(Red).png";
                default:
                    return "";
            }
        }

        public static string getSoldier(PlayerType playerType, SoldierType soldierType)
        {
            switch(soldierType)
            {
                case SoldierType.Hitpoints:
                    return playerType == PlayerType.PLAYER1 ? @"../../../Sprites/hpSoldier(Blue).png" : @"../../../Sprites/hpSoldier(Red).png";
                case SoldierType.Speed:
                    return playerType == PlayerType.PLAYER1 ? @"../../../Sprites/speedSoldier(Blue).png" : @"../../../Sprites/speedSoldier(Red).png";
                default:
                    return "";
            }           
        }

        public static string getBullet()
        {
            return @"../../../Sprites/bullet.png";
        }
    }
}
