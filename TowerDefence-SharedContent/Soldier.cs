using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_SharedContent
{
    public class Soldier : IMove
    {
        public int Level { get; set; }
        public int[] UpgradePrice { get; set; }
        public int[] BuyPrice { get; set; }
        public double Speed { get; set; }
        public double[] Hitpoints { get; set; }
        public Point Coordinates { get; set; }
        public string Sprite { get; set; }

        public Soldier(PlayerType type, int level)
        {

            Coordinates = type==PlayerType.PLAYER1 ? new Point(0, 450) : new Point(1000, 450);
            Sprite = SpritePaths.getSoldier(type);
            Speed = 5;
            Level = level;
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
