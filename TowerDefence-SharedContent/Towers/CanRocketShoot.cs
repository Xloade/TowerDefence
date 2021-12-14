using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class CanRocketShoot : ICanShootAlgorithm
    {
        private int Level;
        private Point Coordinates;
        private int[] Range;

        public CanRocketShoot(int level, Point coordinates, int[] range)
        {
            Level = level;
            Coordinates = coordinates;
            Range = range;
        }
        public bool CanShoot(Point soldierCoordinates)
        {
            MyConsole.WriteLineWithCount("Bridge: Can Rocket Tower shoot");
            return soldierCoordinates.X <= this.Coordinates.X + 150 + Range[Level] && soldierCoordinates.X >= this.Coordinates.X - 150 - Range[Level];
        }
    }
}
