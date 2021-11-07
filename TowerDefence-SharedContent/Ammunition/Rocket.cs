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
        public override string Sprite { get; set; }
        public override int Speed { get; set; }        
        public override int Width { get; set; }
        public override int Height { get; set; }
        public override AmmunitionType AmmunitionType { get; set; }
        public override double Power { get; set; }

        public Rocket(Point towerCoordinates, AmmunitionType ammunitionType, double power) : base(towerCoordinates, ammunitionType, power)
        {
            Coordinates = towerCoordinates;
            Sprite = SpritePaths.getRocket();
            Speed = 5;
            Width = 300;
            Height = 300;
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
