using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_SharedContent.Soldiers
{
    public class Soldier: DrawInfo, ILevel, IHitpoints
    {
        public int Level { get; set; }
        public int[] UpgradePrice { get; set; }
        public int[] BuyPrice { get; set; }
        public double Speed { get; set; }
        public int[] Hitpoints { get; set; }
        public  int CurrentHitpoints { get; set; }
        public SoldierType SoldierType { get; set; }
        public int CurrentLvlHitpoints => Hitpoints[Level];

        public Soldier(PlayerType playerType, SoldierType soldierType, int level)
        {
            Coordinates = playerType == PlayerType.Player1 ? new Point(0, 350) : new Point(1000, 350);
            Sprite = SpritePaths.GetSoldier(playerType, soldierType);
            Speed = 5;
            Level = level;
            SoldierType = soldierType;
            BuyPrice = new int[] { 10, 15, 20 };
            Rotation = playerType == PlayerType.Player1 ? 90 : -90;
        }

        public void MoveForward(PlayerType playerType)
        {
            switch(playerType)
            {
                case PlayerType.Player1:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X + Speed * Level), Coordinates.Y);
                    break;
                case PlayerType.Player2:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X - Speed * Level), Coordinates.Y);
                    break;
                default:
                    break;
            }            
        }

        public bool IsOutOfMap(PlayerType playerType)
        {
            return playerType switch
            {
                PlayerType.Player1 => Coordinates.X > 1100,
                PlayerType.Player2 => Coordinates.X < -100,
                _ => false,
            };
        }
    }
}
