using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence_SharedContent;
using System.Drawing;

namespace TowerDefence_ServerSide
{
    public class MapFactory
    {
        public Map CreateSummerMap()
        {
            Map map = new Map();
            map.mapColor = Color.Green;
            return map;
        }
        public Map CreateSpringMap()
        {
            Map map = new Map();
            map.mapColor = Color.GreenYellow;
            return map;
        }
        public Map CreateWinterMap()
        {
            Map map = new Map();
            map.mapColor = Color.LightCyan;
            return map;
        }
        public Map CreateAutumnMap()
        {
            Map map = new Map();
            map.mapColor = Color.OrangeRed;
            return map;
        }
    }
}
