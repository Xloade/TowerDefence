using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class CanMiniGunShoot : ICanShootAlgorithm
    {
        private readonly int Level;
        private Point Coordinates;
        private readonly int[] Range;

        public CanMiniGunShoot(int level, Point coordinates, int[] range)
        {
            Level = level;
            Coordinates = coordinates;
            Range = range;
        }
        public bool CanShoot(Point soldierCoordinates)
        {
            MyConsole.WriteLineWithCount("Bridge: Can Minigun Tower shoot");
            return Math.Sqrt(Math.Pow((soldierCoordinates.X - Coordinates.X), 2) + Math.Pow((soldierCoordinates.Y - Coordinates.Y), 2)) < Range[Level];
        }
    }
}
