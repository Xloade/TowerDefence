using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Commands;
using TowerDefence_SharedContent.Observers;

namespace TowerDefence_ServerSide.Controllers
{
    public class SoldierController
    {
        PlayerType playerType;
        IHubContext<GameHub> hubContext;
        List<ISoldierObserver> soldierObservers;

        public Timer timer = new Timer();
        public static double timerSpeed = 36; //~30times per second

        public SoldierController(IHubContext<GameHub> hubContext, PlayerType playerType)
        {
            this.hubContext = hubContext;
            this.playerType = playerType;
            soldierObservers = new List<ISoldierObserver>();

            timer.Interval = timerSpeed;
            timer.Start();
            LoopMovement();
        }

        public void Attach(Soldier soldier)
        {            
            soldierObservers.Add(soldier);
        }

        private void Deattach(ISoldierObserver observer)
        {
            soldierObservers.Remove(observer);
        }

        public async void NotifyAll()
        {
            await hubContext.Clients.All.SendAsync(SoldierCommand.CoordinatesChanged.ToString(), playerType, GetSerializedSoldiers());            
        }

        private void LoopMovement()
        {
            timer.Elapsed += async (Object source, System.Timers.ElapsedEventArgs e) => {
                for(int i = 0; i < soldierObservers.Count; i++)
                {
                    soldierObservers[i].Move();
                    if (soldierObservers[i].IsOutOfMap())
                    {
                        Deattach(soldierObservers[i]);
                        i--;
                    }
                    NotifyAll();
                }
            };        
        }

        private string GetSerializedSoldiers()
        {
            return JsonConvert.SerializeObject(soldierObservers.ConvertAll(soldier => (Soldier)soldier));
        }
    }
}
