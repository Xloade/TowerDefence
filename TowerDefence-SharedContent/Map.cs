using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Map : IMapObserver
    {
        public string backgroundImageDir;

        public List<Player> players;

        public Map()
        {
            players = new List<Player>();
        }

        public void AddPlayer(PlayerType playerType)
        {
            players.Add(new Player(playerType));
        }


        public Player GetPlayer(PlayerType type)
        {
            return players.Find(player => player.PlayerType == type);
        }

        public Player GetPlayerEnemy(PlayerType type)
        {
            return players.Find(player => player.PlayerType != type);
        }

        public string ToJson()
        {
            JObject mapJson = (JObject)JToken.FromObject(this);
            return mapJson.ToString();
        }

        public void AddSoldier(Soldier soldier, PlayerType playerType)
        {
            foreach(Player player in players)
            {
                if(player.PlayerType == playerType)
                {
                    player.soldiers.Add(soldier);
                }
            }
        }

        public void AddTower(Tower tower, PlayerType playerType)
        {
            foreach (Player player in players)
            {
                if (player.PlayerType == playerType)
                {
                    player.towers.Add(tower);
                }
            }
        }

        public void UpdateSoldierMovement()
        {
            foreach(Player player in players)
            {
                player.UpdateSoldierMovement();
            }
        }

        public void UpdateTowerActivity()
        {
            if(players.Count > 1)
            {
                foreach (Player player in players)
                {
                    player.UpdateTowerActivity(GetPlayerEnemy(player.PlayerType).soldiers);
                }
            }           
        }
    }
}
