using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TowerDefence_SharedContent.Observers;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;

namespace TowerDefence_SharedContent
{
    public class Soldier : ISoldierObserver
    {
        public PlayerType PlayerType { get; set; }
        public int Level { get; set; }
        public int[] UpgradePrice { get; set; }
        public int[] BuyPrice { get; set; }
        public double Speed { get; set; }
        public double[] Hitpoints { get; set; }
        public Point Coordinates { get; set; }
        public string Sprite { get; set; }
        public int position { get; set; }

        public Soldier(PlayerType type, int position)
        {
            PlayerType = type;
            Coordinates = type==PlayerType.PLAYER1 ? new Point(0, 450) : new Point(1000, 450);
            Sprite = SpritePaths.getSoldier(type);
            Speed = 5;
            this.position = position;
        }

        public void Move()
        {
            switch (PlayerType)
            {
                case PlayerType.PLAYER1:
                    Coordinates = new Point((int)(Coordinates.X + Speed), Coordinates.Y);
                    break;
                case PlayerType.PLAYER2:
                    Coordinates = new Point((int)(Coordinates.X - Speed), Coordinates.Y);
                    break;
            }
        }

        public void Remove(HubConnection connection, string message)
        {
            NotifyServer(connection, message);
        }

        public bool IsOutOfMap()
        {
            switch (PlayerType)
            {
                case PlayerType.PLAYER1:
                    return Coordinates.X > 1100;
                case PlayerType.PLAYER2:
                    return Coordinates.X < -100;
                default:
                    return false;
            }
        }

        public string ToJson()
        {
            JObject json = (JObject)JToken.FromObject(this);
            return json.ToString();
        }     

        public void NotifyServer(HubConnection connection, string message)
        {
            switch(message)
            {
                case "buySoldier":
                    connection.SendAsync(message, PlayerType, ToJson());
                    break;
                case "moveSoldier":
                    connection.SendAsync(message, PlayerType, position, ToJson());
                    break;
                case "removeSoldier":
                    connection.SendAsync(message, PlayerType, ToJson());
                    break;
            }            
        }
    }
}
