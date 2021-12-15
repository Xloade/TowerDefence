using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Laser : Ammunition.Ammunition, IMove
    {
        public Laser(Point towerCoordinates, AmmunitionType ammunitionType, int power, PlayerType playerType) : base(towerCoordinates, ammunitionType, power, playerType)
        {
            Coordinates = towerCoordinates;
            Speed = 50;
            AmmunitionType = ammunitionType;
        }

        public sealed override bool Player1Destroy(Point soldierCoordinates)
        {
            return soldierCoordinates.X >= this.Coordinates.X && soldierCoordinates.X < 800;
        }

        public sealed override bool Player2Destroy(Point soldierCoordinates)
        {
            return soldierCoordinates.X <= this.Coordinates.X && soldierCoordinates.X > 100;
        }

        public override void MoveForward(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Player1:
                    Coordinates = new System.Drawing.Point(Coordinates.X + Speed, Coordinates.Y);
                    break;
                case PlayerType.Player2:
                    Coordinates = new System.Drawing.Point(Coordinates.X - Speed, Coordinates.Y);
                    break;
                default:
                    break;
            }
        }
    }
}
