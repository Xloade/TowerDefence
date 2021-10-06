using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public static class SpritePaths
    {
        public static string getTower(PlayerType type)
        {
            return type == PlayerType.PLAYER1 ? "../../../Sprites/tower(Blue).png" : "../../../Sprites/tower(Red).png";
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
