using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly List<IMapObserver> mapObservers = new List<IMapObserver>();

        static IHubContext<GameHub> _hubContext;
        private static MapController _instance;

        public System.Timers.Timer Timer { get; set; } = new System.Timers.Timer();
        public static double TimerSpeed { get; set; } = 36;
        public static bool FoundThreading { get; set; } = false;

        public static void SetIHubContext(IHubContext<GameHub> context)
        {
            _hubContext = context;
        }

        public static MapController GetInstance(){
            int random = MyConsole.Random();
            MyConsole.LookForMultiThreads("Singleton", random);
            try
            {
                lock (_instance)
                {
                    return _instance;
                }
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
        public static void CreateInstance(){
            if (_instance != null) return;
            _instance = new MapController();
        }
        public static void RemoveInstance()
        {
            _instance = null;
        }
        public static void RestartInstance()
        {
            MapController.RemoveInstance();
        }
        public MapController()
        {
            Timer.Interval = TimerSpeed;
            Timer.Start();

            SendMapUpdates();     
        }

        public void SendMapUpdates()
        {
            Timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                Notify();
                if(mapObservers.Count > 0)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", mapObservers[0].ToJson());
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

        public void UpgradeSoldier(UpgradeType upgradeType)
        {

        }

        public void UpgradeTower(UpgradeType upgradeType)
        {

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
