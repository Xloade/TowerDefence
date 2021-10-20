using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;

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

        public async Task addPlayer(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.players.Add(new Player(playerType));
        }

        public async Task buySlowSoldier(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.GetPlayer(playerType).soldiers.Add(new SlowSoldier(playerType, new Point()));
            Console.WriteLine($"{playerType.ToString()}: buySlowSoldier");
        }

        public async Task buyFastSoldier(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.GetPlayer(playerType).soldiers.Add(new FastSoldier(playerType, new Point()));
            Console.WriteLine($"{playerType.ToString()}: buyFastSoldier");
        }

        public async Task buyTower(PlayerType playerType)
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map.GetPlayer(playerType).towers.Add(new RocketTower(playerType, new Point()));
            Console.WriteLine($"{playerType.ToString()}: buyTower");
                    
        }
        public async Task restartGame()
        {
            MapController mapController = MapControllerSingleton.getMapController();
            mapController.map = new Map();
            Console.WriteLine($"restartGame");
        }
        public async Task deleteTower(PlayerType playerType)
        {
            //MapController mapController = MapControllerSingleton.getMapController();
            //mapController.map.GetPlayer(playerType).towers.re
            //Console.WriteLine($"{playerType.ToString()}: deleteTower");

        }
    }
}
