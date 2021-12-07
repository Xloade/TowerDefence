using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public static class SpritePaths
    {
        readonly public static string Dir = "../../../Sprites";
        readonly public static string CursorsDir = "../../../Cursors";
        public static string GetTower(PlayerType playerType, TowerType towerType)
        {
            return towerType switch
            {
                TowerType.Minigun => playerType == PlayerType.Player1 ? $"{Dir}/bulletTower(Blue).png" : $"{Dir}/bulletTower(Red).png",
                TowerType.Rocket => playerType == PlayerType.Player1 ? $"{Dir}/rocketTower(Blue).png" : $"{Dir}/rocketTower(Red).png",
                TowerType.Laser => playerType == PlayerType.Player1 ? $"{Dir}/laserTower(Blue).png" : $"{Dir}/laserTower(Red).png",
                _ => "",
            };
        }

        public static string GetTowerCursor(PlayerType playerType, TowerType towerType)
        {
            return towerType switch
            {
                TowerType.Minigun => playerType == PlayerType.Player1 ? $"{CursorsDir}/bulletTower_blue.cur" : $"{CursorsDir}/bulletTower_red.cur",
                TowerType.Rocket => playerType == PlayerType.Player1 ? $"{CursorsDir}/rocketTower_blue.cur" : $"{CursorsDir}/rocketTower_red.cur",
                TowerType.Laser => playerType == PlayerType.Player1 ? $"{CursorsDir}/laserTower_blue.cur" : $"{CursorsDir}/laserTower_red.cur",
                _ => "",
            };
        }

        public static string GetSoldier(PlayerType playerType, SoldierType soldierType)
        {
            return soldierType switch
            {
                SoldierType.HitpointsSoldier => playerType == PlayerType.Player1 ? $"{Dir}/hpSoldier(Blue).png" : $"{Dir}/hpSoldier(Red).png",
                SoldierType.SpeedSoldier => playerType == PlayerType.Player1 ? $"{Dir}/speedSoldier(Blue).png" : $"{Dir}/speedSoldier(Red).png",
                _ => "",
            };
        }        

        public static string GetBullet()
        {
            return $"{Dir}/bullet.png";
        }

        public static string GetLaser()
        {
            return $"{Dir}/laserBeam.png";
        }

        public static string GetRocket()
        {
            return $"{Dir}/rocket.png";
        }

        public static string GetMap(String mapType)
        {
            return $"{Dir}/{mapType}Map.png";
        }
    }
}
