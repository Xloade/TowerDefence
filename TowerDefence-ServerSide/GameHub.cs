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
using TowerDefence_ServerSide.Mediator;

namespace TowerDefence_ServerSide
{
    public class GameHub : Hub
    {
        private readonly PatternFacade facade = PatternFacade.GetInstance();
        protected IMediator mediator;

        public void CreateMap(String mapType)
        {
            MapController.CreateInstance();
            Map map = facade.CreateMap(mapType);

            MapController mapController = MapController.GetInstance(mediator);
            mapController.Attach(map);
        }

        public void AddPlayer(PlayerType playerType)
        {
            MapController mapController = MapController.GetInstance(mediator);
            mapController.AddPlayer(playerType);
        }

        public void BuySoldier(PlayerType playerType, SoldierType soldierType)
        {
            MapController mapController = MapController.GetInstance(mediator);
            mapController.AddSoldier(facade.TrainSoldier(playerType, soldierType), playerType);
            MyConsole.WriteLineWithCount($"{playerType}: buySoldier");
        }

        public void BuyTower(PlayerType playerType, TowerType towerType, Point point)
        {
            MapController mapController = MapController.GetInstance(mediator);
            mapController.AddTower(facade.CreateTower(playerType, towerType, point), playerType);
            MyConsole.WriteLineWithCount($"{playerType}: buyTower");                   
        }
        public void RestartGame()
        {
            MapController mapController = MapController.GetInstance(mediator);
            mapController.Restart();
        }

        public void PauseGame()
        {
            MapController mapController = MapController.GetInstance(mediator);
            mapController.Pause();
            MyConsole.WriteLineWithCount($"Game paused");
        }

        public void BuyTwoSoldier(PlayerType playerType, SoldierType soldierType)
        {
            Thread thread1 = new Thread(new ThreadStart(() =>
            {
                var controller = MapController.GetInstance(mediator);
                controller.AddSoldier(facade.TrainSoldier(playerType, soldierType), playerType);
            }));
            Thread thread2 = new Thread(new ThreadStart(() =>
            {
                var controller = MapController.GetInstance(mediator);
                controller.AddSoldier(facade.TrainSoldier(playerType, soldierType), playerType);
            }));
            thread1.Start();
            thread2.Start();
        }

        public void DeleteTower(PlayerType playerType)
        {
            MyConsole.WriteLineWithCount($"{playerType}: deleteTower");

        }

        public void UpgradeSoldier(PlayerType playerType)
        {
            MyConsole.WriteLineWithCount($"{playerType}: upgradeSoldier");
        }
    }
}
