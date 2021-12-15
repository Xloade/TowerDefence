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
        private static MapParser _instance;

        public static void CreateInstance()
        {
            if (_instance != null) return;
            _instance = new MapParser();
        }

        public static MapParser GetInstance()
        {
            try
            {
                lock (_instance)
                {
                    return _instance;
                }
            }
            catch
            {
                throw new Exception("instance not yet created");
            }
        }

        public Map Parse(string json)
        {
            var result = JObject.Parse(json);
            var players = result["Players"].Children();
            Map map = new Map();
            map.BackgroundImageDir = result["BackgroundImageDir"].ToObject<string>();

            foreach (JToken player in players)
            {
                var hitpoints = player["Hitpoints"].ToObject<int>();
                var towerCurrency = player["TowerCurrency"].ToObject<int>();
                var soldierCurrency = player["SoldierCurrency"].ToObject<int>();
                var playerType = player["PlayerType"].ToObject<PlayerType>();
                var soldiers = player["Ammunition"].ToObject<List<Soldier>>();
                var towersJson = player["Towers"].Children();
                

                var towers = new List<Towers.Tower>();
                foreach (JToken tower in towersJson)
                {
                    towers.Add(ParseTower(tower));
                }

                Player parsedPlayer = new Player(hitpoints, towerCurrency, soldierCurrency, playerType, soldiers, towers);
                map.Players.Add(parsedPlayer);
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
            var isReloading = token["IsReloading"].ToObject<bool>();
            var isOverheated = token["IsReloading"].ToObject<bool>();

            var ammunition = new List<Ammunition.Ammunition>();
            foreach (JToken amm in ammunitionJson)
            {
                ammunition.Add(ParseAmmunition(amm));
            }
 
            Tower tower = towerType switch
            {
                TowerType.Minigun => new MiniGunTower(level, price, coordinates, range, power, rateOfFire, sprite, ammunition, towerType, shootingCooldown, playerType, isReloading, isOverheated),
                TowerType.Rocket => new RocketTower(level, price, coordinates, range, power, rateOfFire, sprite, ammunition, towerType, shootingCooldown, playerType, isReloading, isOverheated),
                TowerType.Laser => new LaserTower(level, price, coordinates, range, power, rateOfFire, sprite, ammunition, towerType, shootingCooldown, playerType, isReloading, isOverheated),
                _ => null,
            };
            tower.Id = Id;
            return tower;
        }

        public Ammunition.Ammunition ParseAmmunition(JToken token)
        {
            var type = token["AmmunitionType"].ToObject<AmmunitionType>();
            return type switch
            {
                AmmunitionType.Bullet => JsonConvert.DeserializeObject<Bullet>(token.ToString()),
                AmmunitionType.Rocket => JsonConvert.DeserializeObject<Rocket>(token.ToString()),
                AmmunitionType.Laser => JsonConvert.DeserializeObject<Laser>(token.ToString()),
                _ => null,
            };
        }
    }
}
