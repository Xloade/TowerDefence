using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace TowerDefence_SharedContent.Soldiers
{
    public class Soldier: DrawInfo, ILevel, IHitpoints, IUpgradable
    {
        public int Level { get; set; }
        public int[] BuyPrice { get; set; }
        public double Speed
        {
            get => Speeds[Level];
        }
        public double[] Speeds { get; set; }
        public int[] Hitpoints { get; set; }
        public  int CurrentHitpoints { get; set; }
        public SoldierType SoldierType { get; set; }
        public int CurrentLvlHitpoints => Hitpoints[Level];

        public Soldier(PlayerType playerType, SoldierType soldierType, int level)
        {
            Coordinates = playerType == PlayerType.Player1 ? new Point(0, 350) : new Point(1000, 350);
            Sprite = SpritePaths.GetSoldier(playerType, soldierType);
            Speeds = new double[]{1,1,1};
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
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X + Speed), Coordinates.Y);
                    break;
                case PlayerType.Player2:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X - Speed), Coordinates.Y);
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

        public void Upgrade()
        {
            if (Level > 1) return;
            Level++;
            CurrentHitpoints = Hitpoints[Level];
        }

        public int UpgradePrice
        {
            get => Level > 1 ? 0 : BuyPrice[Level + 1];
        }

        public bool isUpgrable
        {
            get => Level <= 1;
        }
    }
}
