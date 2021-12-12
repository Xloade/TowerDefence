using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Rocket : Ammunition, IMove
    {
        public int Width { get; set; }

        public Rocket(Point towerCoordinates, AmmunitionType ammunitionType, int power, PlayerType playerType) : base(towerCoordinates, ammunitionType, power, playerType)
        {
            Coordinates = towerCoordinates;
            Speed = 5;
            AmmunitionType = ammunitionType;
            Width = 300;
        }

        public sealed override bool Player1Destroy(Point soldierCoordinates)
        {
            return soldierCoordinates.X <= this.Coordinates.X + this.Width / 2 && soldierCoordinates.X >= this.Coordinates.X - this.Width / 2;
        }

        public sealed override bool Player2Destroy(Point soldierCoordinates)
        {
            return Player1Destroy(soldierCoordinates);
        }

        public override void MoveForward(PlayerType playerType)
        {
            // rocket doesn't move
        }

    }
}
