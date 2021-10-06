using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using TowerDefence_SharedContent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TowerDefence_ServerSide
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string user, string function, string[] args)
        {
            string argString ="";
            foreach(string arg in args)
            {
                argString += arg + ", ";
            }
            if(argString != "")
            {
                argString = argString.Substring(0, argString.Length - 2);
            }
            Console.WriteLine($"{user}: {function}({argString})");
            await Clients.All.SendAsync("ReceiveMessage", user, function, args);
        }

        public async Task buySoldier(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.addSoldier(playerType);
            Console.WriteLine($"{playerType.ToString()}: buySoldier");
        }

        public async Task buyTower(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.addTower(playerType);
            Console.WriteLine($"{playerType.ToString()}: buyTower");
                    
        }        
    }
}
