using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Bullet : ShootAlgorithm, IMove
    {
        public override Point Coordinates { get; set; }
        public override int Speed { get; set; }
        public override AmmunitionType AmmunitionType { get; set; }
        public Bullet(Point towerCoordinates, AmmunitionType ammunitionType, int power) : base(towerCoordinates, ammunitionType, power)
        {
            Coordinates = towerCoordinates;
            Speed = 5;
            AmmunitionType = ammunitionType;
        }

        public override bool CanDestroy(Point soldierCoordinates, PlayerType playerType)
        {            
            if(playerType == PlayerType.PLAYER1)
                return soldierCoordinates.X <= this.Coordinates.X;
            else
                return soldierCoordinates.X >= this.Coordinates.X;
        }
        public override void MoveForward(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.PLAYER1:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X + Speed), Coordinates.Y);
                    break;
                case PlayerType.PLAYER2:
                    Coordinates = new System.Drawing.Point((int)(Coordinates.X - Speed), Coordinates.Y);
                    break;
                default:
                    break;
            }
        }

    }
}
