using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public class Rocket : ShootAlgorithm, IMove
    {
        public override Point Coordinates { get; set; }
        public override int Speed { get; set; }  
        public override AmmunitionType AmmunitionType { get; set; }

        public Rocket(Point towerCoordinates, AmmunitionType ammunitionType, int power) : base(towerCoordinates, ammunitionType, power)
        {
            Coordinates = towerCoordinates;
            Speed = 5;
            AmmunitionType = ammunitionType;
        }

        public override bool CanDestroy(Point soldierCoordinates, PlayerType playerType)
        {
            return soldierCoordinates.X == this.Coordinates.X;
        }

        public override void MoveForward(PlayerType playerType)
        {
            // rocket doesn't move
        }

    }
}
