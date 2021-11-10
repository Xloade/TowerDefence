using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using TowerDefence_SharedContent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TowerDefence_SharedContent.Towers;
using TowerDefence_SharedContent.Soldiers;
using System.Drawing;
using System.Threading;
using TowerDefence_ServerSide.Facade;

namespace TowerDefence_ServerSide
{
    public class GameHub : Hub
    {
        private PatternFacade facade = PatternFacade.GetInstance();
        public void createMap(String MapType)
        {
            MapController.createInstance();
            Map map = facade.CreateMap(MapType);

            MapController mapController = MapController.getInstance();
            mapController.Attach(map);
        }

        public void addPlayer(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.AddPlayer(playerType);
        }

        public void buySoldier(PlayerType playerType, SoldierType soldierType)
        {
            MapController mapController = MapController.getInstance();
            mapController.AddSoldier(facade.TrainSoldier(playerType, soldierType), playerType);
            MyConsole.WriteLineWithCount($"{playerType.ToString()}: buySoldier");
        }

        public void buyTower(PlayerType playerType, TowerType towerType, Point point)
        {
            MapController mapController = MapController.getInstance();
            mapController.AddTower(facade.CreateTower(playerType, towerType, point), playerType);
            MyConsole.WriteLineWithCount($"{playerType.ToString()}: buyTower");                   
        }
        public void restartGame()
        {
            MapController mapController = MapController.getInstance();
            mapController.Restart();
        }
        public void buyTwoSoldier(PlayerType playerType, SoldierType soldierType)
        {
            Thread thread1 = new Thread(new ThreadStart(() =>
            {
                var controller = MapController.getInstance();
                controller.AddSoldier(facade.TrainSoldier(playerType, soldierType), playerType);
            }));
            Thread thread2 = new Thread(new ThreadStart(() =>
            {
                var controller = MapController.getInstance();
                controller.AddSoldier(facade.TrainSoldier(playerType, soldierType), playerType);
            }));
            thread1.Start();
            thread2.Start();
        }

        public void deleteTower(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
            // todo
            // mapController.deleteTower(playerType);
            MyConsole.WriteLineWithCount($"{playerType.ToString()}: deleteTower");

        }

        public void upgradeSoldier(PlayerType playerType)
        {
            MapController mapController = MapController.getInstance();
           // mapController.map.upgradeSoldier(playerType, 2);
            MyConsole.WriteLineWithCount($"{playerType.ToString()}: upgradeSoldier");
        }
    }
}
