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
        public string BackgroundImageDir;

        public List<Player> Players;

        public Map()
        {
            Players = new List<Player>();
        }

        public void AddPlayer(PlayerType playerType)
        {
            lock (this)
            {
                Players.RemoveAll((player)=> player.PlayerType == playerType);
                Players.Add(new Player(playerType));
            }
        }


        public Player GetPlayer(PlayerType type)
        {
            lock (this)
            {
                return Players.Find(player => player.PlayerType == type);
            }
        }

        public Player GetPlayerEnemy(PlayerType type)
        {
            lock (this)
            {
            return Players.Find(player => player.PlayerType != type);
            }
        }

        public string ToJson()
        {
            JObject mapJson;
            lock (this)
            {
                mapJson = (JObject)JToken.FromObject(this);
            }
            return mapJson.ToString();
        }

        public void AddSoldier(Soldier soldier, PlayerType playerType)
        {
            MyConsole.WriteLineWithCount("Observer: Update Map");
            lock (this)
            {
                foreach (Player player in Players)
                {
                    if (player.PlayerType == playerType)
                    {
                        player.Soldiers.Add(soldier);
                        player.SoldierCurrency -= soldier.BuyPrice[soldier.Level];
                    }
                }
            }
        }

        public void AddTower(Towers.Tower tower, PlayerType playerType)
        {
            MyConsole.WriteLineWithCount("Observer: Update Map");
            lock (this)
            {
                foreach (Player player in Players)
                {
                    if (player.PlayerType == playerType)
                    {
                        player.Towers.Add(tower);
                        player.TowerCurrency -= tower.Price[tower.Level];
                    }
                }
            }
        }

        public void UpdateSoldierMovement()
        {
            lock (this)
            {
                foreach (Player player in Players)
                {
                    player.UpdateSoldierMovement();
                }
            }
        }

        public void UpdateTowerActivity()
        {
            lock (this)
            {
                if (Players.Count > 1)
                {
                    foreach (Player player in Players)
                    {
                        player.UpdateTowerActivity(GetPlayerEnemy(player.PlayerType).Soldiers);
                    }
                }
            }         
        }

        public void Restart()
        {
            Players.ForEach((player)=> {
                player.Soldiers.Clear();
                player.Towers.Clear();
            });
        }
    }
}
