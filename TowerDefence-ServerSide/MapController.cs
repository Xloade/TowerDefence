using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Timers;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ServerSide
{
    public class MapController : IMapController
    {
        private List<IMapObserver> mapObservers = new List<IMapObserver>();

        static IHubContext<GameHub> hubContext;
        public Timer timer = new Timer();
        public static double timerSpeed = 36; //~30times per second

        private static MapController instance;

        public static void setIHubContext(IHubContext<GameHub> context)
        {
            hubContext = context;
        }

        public static MapController getInstance(){
            try
            {
                lock (instance)
                {
                    return instance;
                };
            }
            catch
            {
                throw new Exception("instance not yet created");
            }
        }
        public static void createInstance(){
            if (instance != null) return;
            instance = new MapController();
        }
        public static void removeInstance()
        {
            instance = null;
        }
        public static void restartInstance()
        {
            MapController.removeInstance();
        }
        public MapController()
        {
            timer.Interval = timerSpeed;
            timer.Start();

            SendMapUpdates();     
        }

        public void SendMapUpdates()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                Notify();
                await hubContext.Clients.All.SendAsync("ReceiveMessage", mapObservers[0].ToJson());
            };
        }

        public void AddSoldier(Soldier soldier, PlayerType playerType)
        {
            mapObservers[0].AddSoldier(soldier, playerType);
        }

        public void AddTower(Tower tower, PlayerType playerType)
        {
            mapObservers[0].AddTower(tower, playerType);
        }

        public void AddPlayer(PlayerType playerType)
        {
            mapObservers[0].AddPlayer(playerType);
        }

        public void Attach(IMapObserver mapObserver)
        {
            mapObservers.Add(mapObserver);
        }

        public void Deattach(IMapObserver mapObserver)
        {
            mapObservers.Remove(mapObserver);
        }

        public void Notify()
        {
            mapObservers[0].UpdateSoldierMovement();
            mapObservers[0].UpdateTowerActivity();
        }
    }
}
