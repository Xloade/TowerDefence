using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Laser : ShootAlgorithm, IMove
    {
        public override Point Coordinates { get; set; }
        public override string Sprite { get; set; }
        public override int Speed { get; set; }
        public override int Width { get; set; }
        public override int Height { get; set; }
        public override AmmunitionType AmmunitionType { get; set; }

        public Laser(Point towerCoordinates, AmmunitionType ammunitionType) : base(towerCoordinates, ammunitionType)
        {
            Coordinates = towerCoordinates;
            Sprite = SpritePaths.getLaser();
            Speed = 400;
            Width = 50;
            Height = 1500;
            AmmunitionType = ammunitionType;
        }

        public override bool CanDestroy(Point soldierCoordinates, PlayerType playerType)
        {
            if (playerType == PlayerType.PLAYER1) 
                return soldierCoordinates.X >= this.Coordinates.X && soldierCoordinates.X < 700;
            else 
                return soldierCoordinates.X <= this.Coordinates.X && soldierCoordinates.X > 200;
        }

        public override void MoveForward(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.PLAYER1:
                    Coordinates = new System.Drawing.Point((int)(680), Coordinates.Y);
                    break;
                case PlayerType.PLAYER2:
                    Coordinates = new System.Drawing.Point((int)(220), Coordinates.Y);
                    break;
                default:
                    break;
            }
        }

    }
}
