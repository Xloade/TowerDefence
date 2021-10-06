﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using TowerDefence_SharedContent;

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

            AddTowerScan(PlayerType.PLAYER1, PlayerType.PLAYER2);
            AddTowerScan(PlayerType.PLAYER2, PlayerType.PLAYER1);

            AddBulletScan(PlayerType.PLAYER1);

        }
        private void AddMapSend()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                await hubContext.Clients.All.SendAsync("ReceiveMessage", map.ToJson());
            };
        }
        private void AddSoldierMovement()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                var player1TSoldiers = map.GetPlayer(PlayerType.PLAYER1).soldiers;
                for (int i = 0; i < player1TSoldiers.Count; i++)
                {
                    var soldier = player1TSoldiers[i];
                    soldier.Coordinates = new System.Drawing.Point((int)(soldier.Coordinates.X + soldier.Speed), soldier.Coordinates.Y);
                    //deletes soldier when out of bounds
                    if (soldier.Coordinates.X > 1100)
                    {
                        player1TSoldiers.Remove(soldier);
                        i--;
                    }
                }

                var player2TSoldiers = map.GetPlayer(PlayerType.PLAYER2).soldiers;
                for (int i = 0; i < player2TSoldiers.Count; i++)
                {
                    var soldier = player2TSoldiers[i];
                    soldier.Coordinates = new System.Drawing.Point((int)(soldier.Coordinates.X + soldier.Speed), soldier.Coordinates.Y);
                    //deletes soldier when out of bounds

                    if (soldier.Coordinates.X < -100)
                    {
                        player2TSoldiers.Remove(soldier);
                        i--;
                    }
                }
            };
        }

        private void AddTowerScan(PlayerType player1, PlayerType player2)
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                var soldiers = map.GetPlayer(player1).soldiers;
                var towers = map.GetPlayer(player2).towers;
                towers.ForEach((tower) =>
                {
                    ScanAndShoot(tower, soldiers);
                });
            };
        }

        private void AddBulletScan(PlayerType playerType)
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                var towers = map.GetPlayer(playerType).towers;
                towers.ForEach((tower) =>
                {
                    for (int i = 0; i < tower.Bullets.Count; i++)
                    {
                        var bullet = tower.Bullets[i];
                        bullet.Coordinates = new System.Drawing.Point((int)(bullet.Coordinates.X + 15), bullet.Coordinates.Y);
                        //deletes soldier when out of bounds
                        if (bullet.Coordinates.X > 1100)
                        {
                            tower.Bullets.Remove(bullet);
                            i--;
                        }
                    }
                });
            };
        }

        private void ScanAndShoot(Tower tower, List<Soldier> soldiers)
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                var soldier = soldiers[i];

                if (CanShoot(soldier.Coordinates, tower.Coordinates, tower.Range[tower.Level]))
                {
                    Shoot(tower);
                    //soldiers.Remove(soldier);
                    //tower.Level++;
                    //i--;
                }
            }
        }

        private bool CanShoot(Point soldierCoordinates, Point towerCoordinates, int range)
        {
            return Math.Abs(soldierCoordinates.X - towerCoordinates.X) == range;
        }

        private void Shoot(Tower tower)
        {
            tower.Bullets.Add(new Bullet(tower.Coordinates));
        }
    }
}
