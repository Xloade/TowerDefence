using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ServerSide
{
    public class MapController
    {
        public Map map { get; set; }
        IHubContext<GameHub> hubContext;
        public Timer timer = new Timer();
        public static double timerSpeed = 36; //~30times per second
        public MapController(IHubContext<GameHub> hubContext)
        {
            map = new Map();
            this.hubContext = hubContext;
            timer.Interval = timerSpeed;
            timer.Start();

            AddMapSend();
            AddSoldierMovement();

            //AddTowerScan(PlayerType.PLAYER1, PlayerType.PLAYER2);
            //AddTowerScan(PlayerType.PLAYER2, PlayerType.PLAYER1);

            //AddBulletMovement();
        }
        private void AddMapSend()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                if(map.players.Count > 0)
                {
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", map.ToJson());
                }                
            };
        }
        public void AddSoldierMovement()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) => {
                map.players.ForEach(player =>
                {
                    for (int i = 0; i < player.soldiers.Count; i++)
                    {
                        var soldier = player.soldiers[i];
                        if (player.PlayerType == PlayerType.PLAYER1)
                        {
                            soldier.Coordinates = new System.Drawing.Point((int)(soldier.Coordinates.X + soldier.Speed), soldier.Coordinates.Y);
                            if (soldier.Coordinates.X > 1100)
                            {
                                player.soldiers.Remove(soldier);
                                i--;
                            }
                        }
                        else
                        {
                            soldier.Coordinates = new System.Drawing.Point((int)(soldier.Coordinates.X - soldier.Speed), soldier.Coordinates.Y);
                            if (soldier.Coordinates.X < -100)
                            {
                                player.soldiers.Remove(soldier);
                                i--;
                            }
                        }
                    }
                });
                //var soldiersPlayer1 = map.GetPlayer(PlayerType.PLAYER1).soldiers;
                //for (int i = 0; i < soldiersPlayer1.Count; i++)
                //{
                //    var soldier = soldiersPlayer1[i];
                //    soldier.Coordinates = new System.Drawing.Point((int)(soldier.Coordinates.X + soldier.Speed), soldier.Coordinates.Y);
                //    if (soldier.Coordinates.X > 1100)
                //    {
                //        soldiersPlayer1.Remove(soldier);
                //        i--;
                //    }
                //}
                //var soldiersPlayer2 = map.GetPlayer(PlayerType.PLAYER2).soldiers;
                //for (int i = 0; i < soldiersPlayer2.Count; i++)
                //{
                //    var soldier = soldiersPlayer2[i];
                //    soldier.Coordinates = new System.Drawing.Point((int)(soldier.Coordinates.X - soldier.Speed), soldier.Coordinates.Y);
                //    if (soldier.Coordinates.X < -100)
                //    {
                //        soldiersPlayer2.Remove(soldier);
                //        i--;
                //    }
                //}               
            };
        }

        public void AddTowerScan(PlayerType player1, PlayerType player2)
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                if(map.players.Count > 0)
                {
                    var soldiers = map.GetPlayer(player1).soldiers;
                    var towers = map.GetPlayer(player2).towers;
                    towers.ForEach((tower) =>
                    {
                        ScanAndShoot(tower, soldiers);
                    });
                }               
            };
        }

        public void AddBulletMovement()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) => {
                if(map.players.Count > 0)
                {
                    var towersPlayer1 = map.GetPlayer(PlayerType.PLAYER1).towers;
                    towersPlayer1.ForEach((tower) =>
                    {
                        for (int i = 0; i < tower.Bullets.Count; i++)
                        {
                            var bullet = tower.Bullets[i];
                            bullet.Coordinates = new System.Drawing.Point((int)(bullet.Coordinates.X + bullet.Speed), bullet.Coordinates.Y);
                            if (bullet.Coordinates.X > 1100)
                            {
                                tower.Bullets.Remove(bullet);
                                i--;
                            }
                        }
                    });
                    var towersPlayer2 = map.GetPlayer(PlayerType.PLAYER2).towers;
                    towersPlayer2.ForEach((tower) =>
                    {
                        for (int i = 0; i < tower.Bullets.Count; i++)
                        {
                            var bullet = tower.Bullets[i];
                            bullet.Coordinates = new System.Drawing.Point((int)(bullet.Coordinates.X - bullet.Speed), bullet.Coordinates.Y);
                            if (bullet.Coordinates.X < -100)
                            {
                                tower.Bullets.Remove(bullet);
                                i--;
                            }
                        }
                    });
                }                                      
            };
        }

        public void ScanAndShoot(Tower tower, List<Soldier> soldiers)
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                var soldier = soldiers[i];

                if (CanShoot(soldier.Coordinates, tower.Coordinates, tower.Range[2]))
                {
                    Shoot(tower);
                    
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

        public void Shoot(Tower tower)
        {
            tower.Bullets.Add(new Bullet(tower.Coordinates));
        }        
    }
}
