using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent
{
    public class Tower
    {
        public int Level { get; set; }
        public int[] Price  { get; set; }
        public Point Coordinates { get; set; }
        public int[] Range { get; set; }
        public int[] Power { get; set; }
        public double[] RateOfFire { get; set; }
        public string Sprite { get; set; }
        public List<Bullet> Bullets { get; set; }

        public Tower(PlayerType type)
        {
            Level = 0;
            Range = new int[] { 100, 200, 300};
            Coordinates = type == PlayerType.PLAYER1 ? new Point(200, 450) : new Point(800, 450);
            Sprite = SpritePaths.getTower(type);
            Bullets = new List<Bullet>();
            //Bullets.Add(new Bullet(Coordinates));
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

                if (CanShoot(soldier.Coordinates, this.Coordinates, this.Range[2]))
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

        public bool CanShoot(Point soldierCoordinates, Point towerCoordinates, int range)
        {
            return Math.Abs(soldierCoordinates.X - towerCoordinates.X) == range;
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
