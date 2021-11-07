using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent
{
    public abstract class ShootAlgorithm : IMove
    {
        public abstract Point Coordinates { get; set; }
        public abstract int Speed { get; set; }
        public abstract AmmunitionType AmmunitionType { get; set; }

        public ShootAlgorithm(Point towerCoordinates, AmmunitionType ammunitionType, int power)
        {
            Coordinates = towerCoordinates;
            AmmunitionType = ammunitionType;
            Power = power;
        }

        public abstract void MoveForward(PlayerType playerType);
        
        public abstract bool CanDestroy(Point soldierCoordinates, PlayerType playerType);

        public bool IsOutOfMap(PlayerType playerType)
        {
            switch (playerType)
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
