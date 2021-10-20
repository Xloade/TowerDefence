using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Soldiers
{
    public class SlowSoldier : Soldier
    {
        public override int Level { get; set; }
        public override int[] UpgradePrice { get; set; }
        public override int[] BuyPrice { get; set; }
        public override double Speed { get; set; }
        public override double[] Hitpoints { get; set; }
        public override Point Coordinates { get; set; }
        public override string Sprite { get; set; }

        public SlowSoldier(PlayerType type, Point coordinates) : base(type, coordinates) 
        {
            Coordinates = type == PlayerType.PLAYER1 ? new Point(100, 400) : new Point(800, 400);
            Speed = 5;
        }

        public string ToJson()
        {
            JObject mapJson = (JObject)JToken.FromObject(this);
            return mapJson.ToString();
        }
    }
}
