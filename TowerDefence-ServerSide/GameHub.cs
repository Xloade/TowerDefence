using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using TowerDefence_SharedContent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TowerDefence_ServerSide
{
    public class GameHub : Hub
    {
        public void createMap(String MapType)
        {
            MapControllerSingleton.createMap(MapType);
        }
        public void buySoldier(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.addSoldier(playerType);
            Console.WriteLine($"{playerType.ToString()}: buySoldier");
        }

        public void buyTower(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.addTower(playerType);
            Console.WriteLine($"{playerType.ToString()}: buyTower");
                    
        }
        public void restartGame()
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.restartMap();
            Console.WriteLine($"restartGame");
        }
        public void deleteTower(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.deleteTower(playerType);
            Console.WriteLine($"{playerType.ToString()}: deleteTower");

        }

    }
}
