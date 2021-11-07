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
        public abstract List<ShootAlgorithm> Ammunition { get; set; }
        public abstract TowerType TowerType { get; set; }
        public int ShootingCooldown { get; set; }

        public Tower(PlayerType playerType, TowerType towerType)
        {
            Level = 0;
            Coordinates = playerType == PlayerType.PLAYER1 ? new Point(200, 450) : new Point(800, 450);
            Ammunition = new List<ShootAlgorithm>();
            TowerType = towerType;
            Sprite = SpritePaths.getTower(playerType, towerType);
            ShootingCooldown = 0;
        }

        public Tower(int level, int[] price, Point coordinates, int[] range, int[]power, double[]rateOfFire,
            string sprite, List<ShootAlgorithm> ammunition, TowerType towerType)
        {
            Level = level;
            Price = price;
            Coordinates = coordinates;
            Range = range;
            Power = power;
            RateOfFire = rateOfFire;
            Sprite = sprite;
            Ammunition = ammunition;
            TowerType = towerType;
            ShootingCooldown = 0;
        }

        public void MoveAmmunition(PlayerType type)
        {
            for (int i = 0; i < Ammunition.Count; i++)
            {
                Ammunition[i].MoveForward(type);
                if (Ammunition[i].IsOutOfMap(type))
                {
                    Ammunition.Remove(Ammunition[i]);
                    i--;
                }
            }
        }

        public void Scan(List<Soldier> soldiers, PlayerType playerType)
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                var soldier = soldiers[i];
                ShootingCooldown--;
                if (CanShoot(soldier.Coordinates, this.Coordinates))
                {
                    Shoot();
                }
                if (this.Ammunition.Count > 0)
                {
                    for (int k = 0; k < Ammunition.Count; k++)
                    {
                        if (this.Ammunition[k].CanDestroy(soldier.Coordinates, playerType))
                        {
                            soldier.CurrentHitpoints -= Ammunition[k].Power;
                            if(soldier.CurrentHitpoints <= 0)
                            {
                                soldiers.Remove(soldier);
                                i--;
                            }
                            Ammunition.RemoveAt(k);
                            k--;
                        }
                    }
                }
            }
        }

        public bool CanShoot(Point soldierCoordinates, Point towerCoordinates)
        {
            return Math.Sqrt(Math.Pow((soldierCoordinates.X - towerCoordinates.X), 2) + Math.Pow((soldierCoordinates.Y - towerCoordinates.Y), 2)) < Range[Level];
        }

        public void Shoot()
        {
            if(ShootingCooldown > 0)
            {
                return;
            }
            ShootingCooldown = (int)(600/RateOfFire[Level]);
            GameElementFactory ammunitionFactory = new AmmunitionFactory();
            Console.WriteLine("----- Strategy -----");
            if (this is MiniGunTower)
                Ammunition.Add(ammunitionFactory.CreateAmmunition(this.Coordinates, AmmunitionType.Bullet, Power[Level]));
            else if (this is RocketTower)
                Ammunition.Add(ammunitionFactory.CreateAmmunition(this.Coordinates, AmmunitionType.Rocket, Power[Level]));
            else if  (this is LaserTower)
                Ammunition.Add(ammunitionFactory.CreateAmmunition(this.Coordinates, AmmunitionType.Laser, Power[Level]));
        }
    }
}
