using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TowerDefence_ServerSide.Controllers;
using TowerDefence_SharedContent;

namespace TowerDefence_ServerSide.Singleton
{
    public class PlayerSingleton
    {
        static List<PlayerController> players;
        static IHubContext<GameHub> gameHubContext;

        public Timer timer = new Timer();
        public static double timerSpeed = 36; //~30times per second

        public static void InitPlayerSingleton(IHubContext<GameHub> context)
        {
            gameHubContext = context;
            players = new List<PlayerController>();
        }

        public static void AddPlayer(PlayerType playerType)
        {
            players.Add(new PlayerController(gameHubContext, playerType));
        }

        public static PlayerController GetPlayer(PlayerType playerType)
        {
            return players.Find(player => player.playerType == playerType);
        }

        public static List<PlayerController> GetPlayers()
        {
            return players;
        }      
    }
}
