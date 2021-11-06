using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Soldiers;

namespace TowerDefence_SharedContent.Towers
{
    public abstract class Tower
    {
        public abstract int Level { get; set; }
        public abstract int[] Price  { get; set; }
        public abstract Point Coordinates { get; set; }
        public abstract int[] Range { get; set; }
        public abstract int[] Power { get; set; }
        public abstract double[] RateOfFire { get; set; }
        public abstract string Sprite { get; set; }
        public abstract List<Bullet> Bullets { get; set; }
        public abstract TowerType TowerType { get; set; }

        public Tower(PlayerType playerType, TowerType towerType)
        {
            Level = 0;
            Coordinates = playerType == PlayerType.PLAYER1 ? new Point(200, 450) : new Point(800, 450);
            Bullets = new List<Bullet>();
            TowerType = towerType;
            Sprite = SpritePaths.getTower(playerType, towerType);
        }

        public void MoveBullets(PlayerType type)
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].MoveForward(type);
                if (Bullets[i].IsOutOfMap(type))
                {
                    Bullets.Remove(Bullets[i]);
                    i--;
                }
            }
        }

        public void Scan(List<Soldier> soldiers)
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                var soldier = soldiers[i];

                if (CanShoot(soldier.Coordinates, this.Coordinates))
                {
                    Shoot();

                }
                if (this.Bullets.Count > 0)
                {
                    if (CanDestroy(soldier.Coordinates, this.Bullets[0].Coordinates))
                    {
                        this.Bullets.Clear();
                        soldiers.Remove(soldier);
                        i--;
                    }
                }
            }
        }

        public bool CanShoot(Point soldierCoordinates, Point towerCoordinates)
        {
            return Math.Abs(soldierCoordinates.X - towerCoordinates.X) == Range[Level];
        }

        public void Shoot()
        {
            Bullets.Add(new Bullet(this.Coordinates));
        }

        public bool CanDestroy(Point soldierCoordinates, Point bulletCoordinates)
        {
            return soldierCoordinates.X == bulletCoordinates.X;
        }
    }
}
