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
        public List<Player> Players { get; set; }
        public string BackgroundImageDir { get; set; }

        public Map()
        {
            Players = new List<Player>();
        }

        public void AddPlayer(PlayerType playerType)
        {
            Map map = this;
            lock (map)
            {
                Players.RemoveAll((player)=> player.PlayerType == playerType);
                Players.Add(new Player(playerType));
            }
        }


        public Player GetPlayer(PlayerType type)
        {
            Map map = this;
            lock (map)
            {
                return Players.Find(player => player.PlayerType == type);
            }
        }

        public Player GetPlayerEnemy(PlayerType type)
        {
            Map map = this;
            lock (map)
            {
                return Players.Find(player => player.PlayerType != type);
            }
        }

        public string ToJson()
        {
            string mapJson;
            Map map = this;
            lock (map)
            {
                //mapJson = (JObject)JToken.FromObject(this);
                mapJson = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }

            return mapJson;
        }

        public void AddSoldier(Soldier soldier, PlayerType playerType)
        {
            MyConsole.WriteLineWithCount("Observer: Update Map");
            Map map = this;
            lock (map)
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
            Map map = this;
            lock (map)
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
            Map map = this;
            lock (map)
            {
                foreach (Player player in Players)
                {
                    player.UpdateSoldierMovement();
                }
            }
        }

        public void UpdateTowerActivity()
        {
            Map map = this;
            lock (map)
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
            Map map = this;
            lock (map)
            {
                Players.ForEach((player) =>
                {
                    player.Soldiers.Clear();
                    player.Towers.Clear();
                });
            }
        }

        public void UpgradeTowers(PlayerType playerType, List<IdableObject> towers)
        {
            Map map = this;
            lock (map)
            {
                var player = GetPlayer(playerType);
                foreach (var tower in towers)
                {
                    var realTower = player.Towers.Find(x => x.Id == tower.Id);
                    if (realTower == null) continue;
                    var price = realTower.UpgradePrice;
                    if (realTower.isUpgrable && realTower.UpgradePrice <= player.TowerCurrency)
                    {
                        realTower.Upgrade();
                        player.TowerCurrency -= price;
                    }
                }
            }
        }
        public void UpgradeSoldiers(PlayerType playerType, List<IdableObject> soldiers)
        {
            Map map = this;
            lock (map)
            {
                var player = GetPlayer(playerType);
                foreach (var soldier in soldiers)
                {
                    var realSoldier = player.Soldiers.Find(x => x.Id == soldier.Id);
                    if (realSoldier == null) continue;
                    var price = realSoldier.UpgradePrice;
                    if (realSoldier.isUpgrable &&
                        realSoldier.UpgradePrice <= player.SoldierCurrency)
                    {
                        realSoldier.Upgrade();
                        player.SoldierCurrency -= price;
                    }
                }
            }
        }
    }
}
