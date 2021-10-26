using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence_SharedContent;

namespace TowerDefence_ServerSide.Controllers
{
    public class PlayerController
    {
        public PlayerType playerType { get; set; }
        public SoldierController soldierController { get; set; }

        public PlayerController(IHubContext<GameHub> context, PlayerType playerType)
        {
            this.playerType = playerType;
            this.soldierController = new SoldierController(context, playerType);
        }
    }
}
