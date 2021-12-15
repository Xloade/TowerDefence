using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_SharedContent.Ammunition
{
    public abstract class Ammunition : DrawInfo, IMove
    {
        public int Speed { get; set; }
        public int Power { get; set; }
        public AmmunitionType AmmunitionType { get; set; }

        public Ammunition(Point towerCoordinates, AmmunitionType ammunitionType, int power, PlayerType playerType)
        {
            Coordinates = towerCoordinates;
            AmmunitionType = ammunitionType;
            Power = power;
            Rotation = playerType == PlayerType.Player1 ? 90 : -90;
        }

        public abstract void MoveForward(PlayerType playerType);

        public bool CanDestroy(Point soldierCoordinates, PlayerType playerType)
        {
            if (playerType == PlayerType.Player1)
                return Player1Destroy(soldierCoordinates);
            else
                return Player2Destroy(soldierCoordinates);
        }

        public virtual bool Player1Destroy(Point soldierCoordinates)
        {
            return false;
        }

        public virtual bool Player2Destroy(Point soldierCoordinates)
        {
            return false;
        }

        public bool IsOutOfMap(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Player1:
                    return Coordinates.X > 1100;
                case PlayerType.Player2:
                    return Coordinates.X < -100;
                default:
                    return false;
            }
        }

    }
}
