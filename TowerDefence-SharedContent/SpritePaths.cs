using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public static class SpritePaths
    {
        readonly public static string dir = "../../../Sprites";
        public static string getTower(PlayerType playerType, TowerType towerType)
        {
            switch(towerType)
            {
                case TowerType.Minigun:
                    return playerType == PlayerType.PLAYER1 ? $"{dir}/bulletTower(Blue).png" : $"{dir}/bulletTower(Red).png";
                case TowerType.Rocket:
                    return playerType == PlayerType.PLAYER1 ? $"{dir}/rocketTower(Blue).png" : $"{dir}/rocketTower(Red).png";
                case TowerType.Laser:
                    return playerType == PlayerType.PLAYER1 ? $"{dir}/laserTower(Blue).png" : $"{dir}/laserTower(Red).png";
                default:
                    return "";
            }
        }

        public static string getSoldier(PlayerType playerType, SoldierType soldierType)
        {
            switch(soldierType)
            {
                case SoldierType.HitpointsSoldier:
                    return playerType == PlayerType.PLAYER1 ? $"{dir}/hpSoldier(Blue).png" : $"{dir}/hpSoldier(Red).png";
                case SoldierType.SpeedSoldier:
                    return playerType == PlayerType.PLAYER1 ? $"{dir}/speedSoldier(Blue).png" : $"{dir}/speedSoldier(Red).png";
                default:
                    return "";
            }           
        }

        public static string getBullet()
        {
            return $"{dir}/bullet.png";
        }

        public static string getLaser()
        {
            return $"{dir}/laserBeam.png";
        }

        public static string getRocket()
        {
            return $"{dir}/rocket.png";
        }

        public static string getMap(String mapType)
        {
            return $"{dir}/{mapType}Map.png";
        }
    }
}
