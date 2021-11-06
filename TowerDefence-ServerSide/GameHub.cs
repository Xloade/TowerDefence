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
        MapFactory factory = new MapFactory();
        public void createMap(String MapType)
        {
            MapController.createInstance();
            Map map = factory.CreateMap(MapType);

            MapController mapController = MapController.getInstance();
            mapController.Attach(map);
        }

        public void addPlayer(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.AddPlayer(playerType);
        }

        public void buySoldier(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.AddSoldier(new Soldier(playerType, 1), playerType);
            Console.WriteLine($"{playerType.ToString()}: buySoldier");
        }

        public void buyTower(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.AddTower(new Tower(playerType), playerType);
            Console.WriteLine($"{playerType.ToString()}: buyTower");
                    
        }
        public void restartGame()
        {
            MapController.restartInstance();
            Console.WriteLine($"restartGame");
        }
        public void deleteTower(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
           // mapController.map.deleteTower(playerType);
            Console.WriteLine($"{playerType.ToString()}: deleteTower");

        }

        public void upgradeSoldier(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
           // mapController.map.upgradeSoldier(playerType, 2);
            Console.WriteLine($"{playerType.ToString()}: upgradeSoldier");
        }
    }
}
