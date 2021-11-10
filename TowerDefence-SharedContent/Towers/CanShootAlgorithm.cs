using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public interface CanShootAlgorithm
    {
        bool CanShoot(Point soldierCoordinates);
    }
}
