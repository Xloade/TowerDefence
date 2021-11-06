using System;
using System.Collections.Generic;
using System.Text;
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

        public static string getSoldier(PlayerType type)
        {
            return type == PlayerType.PLAYER1 ? @"../../../Sprites/soldier(Blue).png" : @"../../../Sprites/soldier(Red).png";
        }

        public static string getBullet()
        {
            return @"../../../Sprites/bullet.png";
        }
    }
}
