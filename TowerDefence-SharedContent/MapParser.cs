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
            catch
            {
                throw new Exception("instance not yet created");
            }
        }

        public Map Parse(string json)
        {
            var result = JObject.Parse(json);
            var players = result["players"].Children();
            Map map = new Map();
            map.backgroundImageDir = result["backgroundImageDir"].ToObject<string>();

            foreach (JToken player in players)
            {
                var hitpoints = player["Hitpoints"].ToObject<int>();
                var towerCurrency = player["TowerCurrency"].ToObject<int>();
                var soldierCurrency = player["SoldierCurrency"].ToObject<int>();
                var playerType = player["PlayerType"].ToObject<PlayerType>();
                var soldiers = player["soldiers"].ToObject<List<Soldier>>();
                var towersJson = player["towers"].Children();
                

                var towers = new List<Towers.Tower>();
                foreach (JToken tower in towersJson)
                {
                    towers.Add(ParseTower(tower));
                }

                Player parsedPlayer = new Player(hitpoints, towerCurrency, soldierCurrency, playerType, soldiers, towers);
                map.players.Add(parsedPlayer);
            }
            return map;
        }
        public Towers.Tower ParseTower(JToken token)
        {
            var level = token["Level"].ToObject<int>();
            var price = token["Price"].ToObject<int[]>();
            var coordinates = token["Coordinates"].ToObject<Point>();
            var range = token["Range"].ToObject<int[]>();
            var power = token["Power"].ToObject<int[]>();
            var rateOfFire = token["RateOfFire"].ToObject<double[]>();
            var sprite = token["Sprite"].ToObject<string>();
            var ammunitionJson = token["Ammunition"].Children();
            var towerType = token["TowerType"].ToObject<TowerType>();
            var shootingCooldown = token["ShootingCooldown"].ToObject<int>();
            var playerType = token["PlayerType"].ToObject<PlayerType>();
            var Id = token["Id"].ToObject<long>();

            var ammunition = new List<Ammunition>();
            foreach (JToken amm in ammunitionJson)
            {
                ammunition.Add(ParseAmmunition(amm));
            }

            Tower tower;
            switch (towerType)
            {
                case TowerType.Minigun:
                    tower =  new MiniGunTower(level, price, coordinates, range, power, rateOfFire, sprite, ammunition, towerType, shootingCooldown, playerType);
                    break;
                case TowerType.Rocket:
                    tower = new RocketTower(level, price, coordinates, range, power, rateOfFire, sprite, ammunition, towerType, shootingCooldown,playerType);
                    break;
                case TowerType.Laser:
                    tower = new LaserTower(level, price, coordinates, range, power, rateOfFire, sprite, ammunition, towerType, shootingCooldown, playerType);
                    break;    
                default:
                    tower = null;
                    break;
            }

            tower.Id = Id;
            return tower;
        }

        public Ammunition ParseAmmunition(JToken token)
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
