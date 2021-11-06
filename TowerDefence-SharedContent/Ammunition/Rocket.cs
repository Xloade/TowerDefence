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

        public Rocket(Point towerCoordinates, AmmunitionType ammunitionType) : base(towerCoordinates, ammunitionType)
        {
            Coordinates = towerCoordinates;
            Sprite = SpritePaths.getBullet();
            Speed = 5;
            Width = 500;
            Height = 500;
            AmmunitionType = ammunitionType;
        }

        public override void MoveForward(PlayerType playerType)
        {
            // rocket doesn't move
        }

    }
}
