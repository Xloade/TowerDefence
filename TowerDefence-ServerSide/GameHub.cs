using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using TowerDefence_SharedContent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TowerDefence_SharedContent.Towers;
using TowerDefence_SharedContent.Soldiers;

namespace TowerDefence_ServerSide
{
    public class GameHub : Hub
    {
        MapFactory mapFactory = new MapFactory();
        GameElementFactory towerFactory = new TowerFactory();
        Barrack barrack = new Barrack();
        SoldierBuilder builder;
        public void createMap(String MapType)
        {
            MapController.createInstance();
            Map map = mapFactory.CreateMap(MapType);

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
            mapController.AddSoldier(TrainConcreteSoldier(playerType, soldierType), playerType);
            Console.WriteLine($"{playerType.ToString()}: buySoldier");
        }

        public void buyTower(PlayerType playerType, TowerType towerType)
        {
            MapController mapController = MapController.getInstance();
            mapController.AddTower(towerFactory.CreateTower(playerType, towerType), playerType);
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

        private Soldier TrainConcreteSoldier(PlayerType playerType, SoldierType soldierType)
        {
            switch(soldierType)
            {
                case SoldierType.HitpointsSoldier:
                    builder = new HitpointsSoldierBuilder(playerType, soldierType, 1);
                    barrack.Train(builder, playerType);
                    return builder.Soldier;
                case SoldierType.SpeedSoldier:
                    builder = new SpeedSoldierBuilder(playerType, soldierType, 1);
                    barrack.Train(builder, playerType);
                    return builder.Soldier;
                default:
                    return null;
            }
        }
    }
}
