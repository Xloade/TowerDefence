using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Timers;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;
using System.Threading;
using System.Drawing;
using System.Runtime.Serialization;

namespace TowerDefence_ServerSide
{
    public class MapController : IMapController
    {
        private List<IMapObserver> mapObservers = new List<IMapObserver>();

        static IHubContext<GameHub> hubContext;
        public System.Timers.Timer timer = new System.Timers.Timer();
        public static double timerSpeed = 36; //~30times per second
        public static bool foundThreading = false;
        private static MapController instance;

        public static void setIHubContext(IHubContext<GameHub> context)
        {
            hubContext = context;
        }

        public static MapController getInstance(){
            int random = MyConsole.Random();
            MyConsole.LookForMultiThreads("Singleton", random);
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
            finally
            {
                MyConsole.LookForMultiThreads("Singleton", random);
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
                if(mapObservers.Count > 0)
                {
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", mapObservers[0].ToJson());
                }
            };
        }

        public void AddSoldier(Soldier soldier, PlayerType playerType)
        {
            lock (mapObservers)
            {
                mapObservers[0].AddSoldier(soldier, playerType);
            }
        }

        public void AddTower(TowerDefence_SharedContent.Towers.Tower tower, PlayerType playerType)
        {
            lock (mapObservers)
            {
                mapObservers[0].AddTower(tower, playerType);
            }
        }

        public void AddPlayer(PlayerType playerType)
        {
            lock (mapObservers)
            {
                mapObservers[0].AddPlayer(playerType);
            }
        }

        public void Attach(IMapObserver mapObserver)
        {
            MyConsole.WriteLineWithCount("Observer: Attach map observer");
            lock (mapObservers)
            {
                mapObservers.Add(mapObserver);
            }
        }

        public void Deattach(IMapObserver mapObserver)
        {
            lock (mapObservers)
            {
                mapObservers.Remove(mapObserver);
            }
            
        }

        public void Notify()
        {
            lock (mapObservers)
            {
                if(mapObservers.Count > 0)
                {
                    mapObservers[0].UpdateSoldierMovement();
                    mapObservers[0].UpdateTowerActivity();
                }
            }
        }
        public void Restart(){
            mapObservers[0].Restart();
        }
    }
}
