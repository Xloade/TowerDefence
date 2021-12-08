using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Bullet : Ammunition, IMove
    {
        public Bullet(Point towerCoordinates, AmmunitionType ammunitionType, int power, PlayerType playerType) : base(towerCoordinates, ammunitionType, power, playerType)
        {
            Coordinates = towerCoordinates;
            Speed = 5;
            AmmunitionType = ammunitionType;
        }

        public override bool CanDestroy(Point soldierCoordinates, PlayerType playerType)
        {            
            if(playerType == PlayerType.Player1)
                return soldierCoordinates.X <= this.Coordinates.X;
            else
                return soldierCoordinates.X >= this.Coordinates.X;
        }
        public override void MoveForward(PlayerType playerType)
        {
            switch (playerType)
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

    }
}
