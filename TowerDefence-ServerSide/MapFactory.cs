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
        private Map CreateSummerMap()
        {
            MyConsole.WriteLineWithCount("MapFactory: Summer");
            Map map = new Map();
            map.backgroundImageDir = SpritePaths.getMap("Summer");
            return map;
        }
        private Map CreateSpringMap()
        {
            MyConsole.WriteLineWithCount("MapFactory: Spring");
            Map map = new Map();
            map.backgroundImageDir = SpritePaths.getMap("Spring");
            return map;
        }
        private Map CreateWinterMap()
        {
            MyConsole.WriteLineWithCount("MapFactory: Winter");
            Map map = new Map();
            map.backgroundImageDir = SpritePaths.getMap("Winter");
            return map;
        }
        private Map CreateAutumnMap()
        {
            MyConsole.WriteLineWithCount("MapFactory: Autumn");
            Map map = new Map();
            map.backgroundImageDir = SpritePaths.getMap("Autumn");
            return map;
        }

        public Map CreateMap(String type)
        {
            switch (type)
            {
                case "Summer":
                    return CreateSummerMap();
                case "Spring":
                    return CreateSpringMap();
                case "Winter":
                    return CreateWinterMap();
                case "Autumn":
                    return CreateAutumnMap();
                default:
                    return CreateAutumnMap();
            }
        }
    }
}
