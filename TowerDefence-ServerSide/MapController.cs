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
        public static double timerSpeed = 16; //~60times per second
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
                map.GetPlayer(PlayerType.PLAYER1).soldiers.ForEach((tower)=> {
                    tower.Coordinates = new System.Drawing.Point((int)(tower.Speed + tower.Coordinates.X), tower.Coordinates.Y);
                });
            };
        }
    }
}
