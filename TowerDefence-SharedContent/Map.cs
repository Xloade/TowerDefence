using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_SharedContent
{
    public class Map
    {
        public Player player1;
        public Player player2;
        public Color mapColor;

        public Map()
        {
            player1 = new Player();
            player2 = new Player();
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

        public void deleteTower(PlayerType playerType)
        {
            var towers = GetPlayer(playerType).towers;
            towers.Clear();
        }
    }
}
