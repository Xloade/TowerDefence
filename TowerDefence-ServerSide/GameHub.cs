using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using TowerDefence_SharedContent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TowerDefence_SharedContent.Observers;
using System.Collections.Generic;
using TowerDefence_ServerSide.Singleton;
using System.Timers;

namespace TowerDefence_ServerSide
{
    public class GameHub : Hub
    {
        public Timer timer = new Timer();
        public static double timerSpeed = 36; //~30times per second

        public void createMap(String MapType)
        {
            MapControllerSingleton.createMap(MapType);
        }

        public void createPlayer(PlayerType playerType)
        {
            PlayerSingleton.AddPlayer(playerType);
            if(PlayerSingleton.GetPlayers().Count == 1)
            {
                OnFrameTick();
            }           
        }
        public void buySoldier(PlayerType playerType, string soldierJson)
        {
            PlayerSingleton.GetPlayer(playerType).soldierController
                .Attach(JsonConvert.DeserializeObject<Soldier>(soldierJson));
            //soldierController.Attach(JsonConvert.DeserializeObject<Map>(Soldier))
            //MapControllerSingleton.Update(UpdateMessage.BuySoldier, playerType);
            //MapController mapController = MapControllerSingleton.getMapController();
            //UpdateMap(UpdateMessage.BuySoldier, playerType);
            //MapControllerSingleton.Update(map);
            Console.WriteLine($"{playerType.ToString()}: buySoldier");
        }

        public void OnFrameTick()
        {
            timer.Interval = timerSpeed;
            timer.Start();
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                if (PlayerSingleton.GetPlayers().Count > 0)
                {
                    PlayerSingleton.GetPlayers().ForEach(player =>
                    {
                        player.soldierController.OnFrameTick();
                    });
                }                
            };                    
        }

        //public void buyTower(PlayerType playerType)
        //{
        //    MapControllerSingleton.Update(UpdateMessage.BuyTower, playerType);
        //    //MapController mapController = MapControllerSingleton.getMapController();
        //    //mapController.map.addTower(playerType);
        //    Console.WriteLine($"{playerType.ToString()}: buyTower");
                    
        //}
        //public void restartGame()
        //{
        //  // MapControllerSingleton.Update(UpdateMessage.DeleteTower, playerType);
        //    //MapController mapController = MapControllerSingleton.getMapController();
        //    //mapController.restartMap();
        //    Console.WriteLine($"restartGame");
        //}
        //public void deleteTower(PlayerType playerType)
        //{
        //    MapControllerSingleton.Update(UpdateMessage.DeleteTower, playerType);
        //    //MapController mapController = MapControllerSingleton.getMapController();
        //    //mapController.map.deleteTower(playerType);
        //    Console.WriteLine($"{playerType.ToString()}: deleteTower");
        //}
    }
}
