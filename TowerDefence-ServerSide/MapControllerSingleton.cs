using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence_SharedContent;

namespace TowerDefence_ServerSide
{
    public class MapControllerSingleton
    {
        static MapController mapController;
        static IHubContext<GameHub> GameHubContext;
        public static void setIHubContext(IHubContext<GameHub> context)
        {
            MapControllerSingleton.GameHubContext = context;
        }
        public static void createMap(String mapType)
        {
            if (mapController != null) return;
            MapFactory factory = new MapFactory();
            Map map;
            switch (mapType)
            {
                case "Summer":
                    map = factory.CreateSummerMap();
                    break;
                case "Spring":
                    map = factory.CreateSpringMap();
                    break;
                case "Winter":
                    map = factory.CreateWinterMap();
                    break;
                case "Autumn":
                    map = factory.CreateAutumnMap();
                    break;
                default:
                    map = factory.CreateAutumnMap();
                    break;
            }
            mapController = new MapController(GameHubContext, map);
        }
        public static MapController getMapController()
        {
            return mapController;
        }
    }
}
