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
            MapController.createInstance(MapType);
        }
        public void buySoldier(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.map.addSoldier(playerType);
            Console.WriteLine($"{playerType.ToString()}: buySoldier");
        }

        public void buyTower(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.map.addTower(playerType);
            Console.WriteLine($"{playerType.ToString()}: buyTower");
                    
        }
        public void restartGame()
        {
            MapController mapController = MapController.getInstance();
            mapController.restartMap();
            Console.WriteLine($"restartGame");
        }
        public void deleteTower(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.map.deleteTower(playerType);
            Console.WriteLine($"{playerType.ToString()}: deleteTower");

        }

        public void upgradeSoldier(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.map.upgradeSoldier(playerType, 2);
            Console.WriteLine($"{playerType.ToString()}: upgradeSoldier");
        }
    }
}
