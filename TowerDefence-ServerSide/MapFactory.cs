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
            Map map = new Map();
            map.mapColor = Color.Green;
            return map;
        }
        private Map CreateSpringMap()
        {
            Map map = new Map();
            map.mapColor = Color.GreenYellow;
            return map;
        }
        private Map CreateWinterMap()
        {
            Map map = new Map();
            map.mapColor = Color.LightCyan;
            return map;
        }
        private Map CreateAutumnMap()
        {
            Map map = new Map();
            map.mapColor = Color.OrangeRed;
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
