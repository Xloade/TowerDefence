using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public class CanLaserShoot : CanShootAlgorithm
    {
        public bool CanShoot(Point soldierCoordinates)
        {
            MyConsole.WriteLineWithCount("Bridge: Can Laser Tower shoot");
            return true;
        }
    }
}
