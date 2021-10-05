using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence_SharedContent;

namespace TowerDefence_ServerSide
{
    public class MapSingleton
    {
        static Map map = new Map();
        public static Map getMap()
        {
            return map;
        }
    }
}
