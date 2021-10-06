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
        public static void setMapController(MapController mapController)
        {
            MapControllerSingleton.mapController = mapController;
        }
        public static MapController getMapController()
        {
            return mapController;
        }
    }
}
