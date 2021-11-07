using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_SharedContent.Soldiers
{
    public class Soldier : IMove
    {
        public int Level { get; set; }
        public int[] UpgradePrice { get; set; }
        public int[] BuyPrice { get; set; }
        public double Speed { get; set; }
        public double[] Hitpoints { get; set; }
        public double CurrentHitpoints { get; set; }
        public Point Coordinates { get; set; }
        public string Sprite { get; set; }
        public SoldierType SoldierType { get; set; }

        public Soldier(PlayerType playerType, SoldierType soldierType, int level)
        {
            Coordinates = playerType == PlayerType.PLAYER1 ? new Point(0, 350) : new Point(1000, 350);
            Sprite = SpritePaths.getSoldier(playerType, soldierType);
            Speed = 5;
            Level = level;
            SoldierType = soldierType;
        }

        public void MoveForward(PlayerType playerType)
        {
            switch(playerType)
            {
                case PlayerType.PLAYER1:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X + Speed * Level), Coordinates.Y);
                    break;
                case PlayerType.PLAYER2:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X - Speed * Level), Coordinates.Y);
                    break;
                default:
                    break;
            }            
        }

        public bool IsOutOfMap(PlayerType playerType)
        {
            switch(playerType)
            {
                case PlayerType.PLAYER1:
                    return Coordinates.X > 1100;
                case PlayerType.PLAYER2:              
                    return Coordinates.X < -100;
                default:
                    return false;
            }
        }       
    }
}
