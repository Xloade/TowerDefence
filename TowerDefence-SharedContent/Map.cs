using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public class Map
    {
        public List<Player> players;
        public Season Season { get; set; }

        public Map()
        {
            players = new List<Player>();
        }

        public Player GetPlayer(PlayerType type)
        {
            return players.Find((player => player.PlayerType == type));
        }

        public string ToJson()
        {
            JObject mapJson = (JObject)JToken.FromObject(this);
            return mapJson.ToString();
        }               
    }
}
