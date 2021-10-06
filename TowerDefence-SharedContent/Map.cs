using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace TowerDefence_SharedContent
{
    public class Map
    {
        public Player player1;
        public Player player2;
        public Timer timer = new Timer();
        public static double timerSpeed = 16; //~60times per second

        public Map()
        {
            player1 = new Player();
            player2 = new Player();
            timer.Interval = timerSpeed;
            timer.Start();
        }

        public void addTimerEvent(ElapsedEventHandler handler)
        {
            timer.Elapsed += handler;
        }

        public Player GetPlayer(PlayerType type)
        {
            return type == PlayerType.PLAYER1 ? player1 : player2;
        }

        public string ToJson()
        {
            JObject mapJson = (JObject)JToken.FromObject(this);
            return mapJson.ToString();
        }

        public void addSoldier(PlayerType playerType)
        {
            GetPlayer(playerType).soldiers.Add(new Soldier(playerType));
        }

        public void addTower(PlayerType playerType)
        {
            GetPlayer(playerType).towers.Add(new Tower(playerType));
        }
    }
}
