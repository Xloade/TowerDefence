using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Observers;

namespace TowerDefence_ServerSide
{
    public class MapController
    {
        List<ISoldierObserver> playerSoldierObservers = new List<ISoldierObserver>();
        IMapObserver mapObserver { get; set; }
        public Map map { get; set; }
        //IHubContext<GameHub> hubContext;
        public Timer timer = new Timer();
        public static double timerSpeed = 36; //~30times per second
        public MapController(IHubContext<GameHub> hubContext, Map map)
        {
            this.map = map;
            //this.hubContext = hubContext;
            timer.Interval = timerSpeed;
            timer.Start();

            Loop();
            //AddMapSend();                  
        }

        private void Loop()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) => {
                AddSoldierMovement();

                AddTowerScan(PlayerType.PLAYER1, PlayerType.PLAYER2);
                AddTowerScan(PlayerType.PLAYER2, PlayerType.PLAYER1);

                AddBulletMovement();
            };
        }
        //private void AddMapSend()
        //{
        //    timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
        //    {
        //        await hubContext.Clients.All.SendAsync("ReceiveMessage", map.ToJson());
        //    };
        //}
        public void AddSoldierMovement()
        {
            var soldiersPlayer1 = map.GetPlayer(PlayerType.PLAYER1).soldiers;
            soldiersPlayer1.ForEach(soldier =>
            {
                //soldier.Move(PlayerType.PLAYER1);
                if (soldier.Coordinates.X > 1100)
                {
                    soldiersPlayer1.Remove(soldier);
                }
                NotifyAll();
            });
            var soldiersPlayer2 = map.GetPlayer(PlayerType.PLAYER2).soldiers;
            soldiersPlayer1.ForEach(soldier =>
            {
                //soldier.Move(PlayerType.PLAYER2);
                if (soldier.Coordinates.X < -100)
                {
                    soldiersPlayer1.Remove(soldier);
                }
                NotifyAll();
            });
        }

        public void AddTowerScan(PlayerType player1, PlayerType player2)
        {
            var soldiers = map.GetPlayer(player1).soldiers;
            var towers = map.GetPlayer(player2).towers;
            towers.ForEach((tower) =>
            {
                ScanAndShoot(tower, soldiers);
                NotifyAll();
            });
        }

        public void AddBulletMovement()
        {
            var towersPlayer1 = map.GetPlayer(PlayerType.PLAYER1).towers;
            towersPlayer1.ForEach((tower) =>
            {
                tower.Bullets.ForEach(bullet =>
                {
                    bullet.Fly(PlayerType.PLAYER1);
                    if (bullet.Coordinates.X > 1100)
                    {
                        tower.Bullets.Remove(bullet);
                    }
                    NotifyAll();
                });
            });
            var towersPlayer2 = map.GetPlayer(PlayerType.PLAYER2).towers;
            towersPlayer2.ForEach((tower) =>
            {
                tower.Bullets.ForEach(bullet =>
                {
                    bullet.Fly(PlayerType.PLAYER2);
                    if (bullet.Coordinates.X < -100)
                    {
                        tower.Bullets.Remove(bullet);
                    }
                    NotifyAll();
                });
            });
        }

        public void ScanAndShoot(Tower tower, List<Soldier> soldiers)
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                var soldier = soldiers[i];

                if (CanShoot(soldier.Coordinates, tower.Coordinates, tower.Range[2]))
                {
                    tower.Shoot();                  
                }
                if(tower.Bullets.Count > 0)
                {
                    if (CanDestroy(soldier.Coordinates, tower.Bullets[0].Coordinates))
                    {
                        tower.Bullets.Clear();
                        soldiers.Remove(soldier);                        
                        i--;
                    }
                }            
            }
        }

        public bool CanShoot(Point soldierCoordinates, Point towerCoordinates, int range)
        {
            return Math.Abs(soldierCoordinates.X - towerCoordinates.X) == range;
        }

        public bool CanDestroy(Point soldierCoordinates, Point bulletCoordinates)
        {
            return soldierCoordinates.X == bulletCoordinates.X;
        }
  
        public void restartMap()
        {
            //todo
        }
      
        public void NotifyAll()
        {
            mapObserver.UpdateClient(map);
        }
    }
}
