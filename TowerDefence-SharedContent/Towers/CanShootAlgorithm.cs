using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers
{
    public interface ICanShootAlgorithm
    {
        bool CanShoot(Point soldierCoordinates);
    }
}
