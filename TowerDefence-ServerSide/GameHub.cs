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
            Map map = MapSingleton.getMap();
            Console.WriteLine($"{playerType.ToString()}: buySoldier");
            map.addSoldier(playerType);
            await Clients.All.SendAsync("ReceiveMessage", map.ToJson());
        }

        public async Task buyTower(PlayerType playerType)
        {
            Map map = MapSingleton.getMap();
            Console.WriteLine($"{playerType.ToString()}: buyTower");
            map.addTower(playerType);        
            await Clients.All.SendAsync("ReceiveMessage", map.ToJson());
        }        
    }
}
