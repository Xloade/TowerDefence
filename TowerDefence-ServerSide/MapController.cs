using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence_SharedContent;
using Microsoft.AspNetCore.SignalR;
using System.Timers;

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
        }
        private void AddMapSend()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) => {
                await hubContext.Clients.All.SendAsync("ReceiveMessage", map.ToJson());
            };
        }
        private void AddSoldierMovement()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) => {
                var player1TSoldiers = map.GetPlayer(PlayerType.PLAYER1).soldiers;
                for (int i = 0; i < player1TSoldiers.Count; i++)
                {
                    var tower = player1TSoldiers[i];
                    tower.Coordinates = new System.Drawing.Point((int)(tower.Coordinates.X + tower.Speed), tower.Coordinates.Y);
                    //deletes soldier when out of bounds
                    if (tower.Coordinates.X > 1100)
                    {
                        player1TSoldiers.Remove(tower);
                        i--;
                    }
                }
                var player2TSoldiers = map.GetPlayer(PlayerType.PLAYER2).soldiers;
                for (int i = 0; i < player2TSoldiers.Count; i++)
                {
                    var tower = player2TSoldiers[i];
                    tower.Coordinates = new System.Drawing.Point((int)(tower.Coordinates.X - tower.Speed), tower.Coordinates.Y);
                    //deletes soldier when out of bounds
                    if (tower.Coordinates.X < -100)
                    {
                        player2TSoldiers.Remove(tower);
                        i--;
                    }
                }
            };
        }
    }
}
