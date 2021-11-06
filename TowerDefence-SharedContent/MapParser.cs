using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class MapParser
    {
        private static MapParser instance;

        public static void CreateInstance()
        {
            if (instance != null) return;
            instance = new MapParser();
        }

        public static MapParser getInstance()
        {
            try
            {
                lock (instance)
                {
                    return instance;
                };
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("instance not yet created");
            }
        }

        public Map Parse(string json)
        {
            var result = JObject.Parse(json);
            var color = result["mapColor"].ToObject<Color>();
            var players = result["players"].Children();
            Map map = new Map();
            map.mapColor = color;

            foreach (JToken player in players)
            {
                var hitpoints = player["Hitpoints"].ToObject<int>();
                var towerCurrency = player["TowerCurrency"].ToObject<int>();
                var soldierCurrency = player["SoldierCurrency"].ToObject<int>();
                var playerType = player["PlayerType"].ToObject<PlayerType>();
                var soldiers = player["soldiers"].ToObject<List<Soldier>>();
                var towersJson = player["towers"].Children();
                

                var towers = new List<Tower>();
                foreach (JToken tower in towersJson)
                {
                    var ammunitionJson = tower["Ammunition"].Children();
                    var ammunition = new List<ShootAlgorithm>();
                    foreach(JToken amm in ammunitionJson)
                    {
                        ammunition.Add(ParseAmmunition(amm));
                    }
                    towers.Add(ParseTower(tower));
                }

                Player parsedPlayer = new Player(hitpoints, towerCurrency, soldierCurrency, playerType, soldiers, towers);
                map.players.Add(parsedPlayer);
            }
            return map;
        }
        public Tower ParseTower(JToken token)
        {
            var type = token["TowerType"].ToObject<TowerType>();
            switch (type)
            {
                case TowerType.Minigun:
                    return JsonConvert.DeserializeObject<MiniGunTower>(token.ToString());
                case TowerType.Rocket:
                    return JsonConvert.DeserializeObject<RocketTower>(token.ToString());
                case TowerType.Laser:
                    return JsonConvert.DeserializeObject<LaserTower>(token.ToString());
                default:
                    return null;
            }
        }

        public ShootAlgorithm ParseAmmunition(JToken token)
        {
            var type = token["AmmunitionType"].ToObject<AmmunitionType>();
            switch (type)
            {
                case AmmunitionType.Bullet:
                    return JsonConvert.DeserializeObject<Bullet>(token.ToString());
                case AmmunitionType.Rocket:
                    return JsonConvert.DeserializeObject<Rocket>(token.ToString());
                case AmmunitionType.Laser:
                    return JsonConvert.DeserializeObject<Laser>(token.ToString());
                default:
                    return null;
            }
        }
    }
}
