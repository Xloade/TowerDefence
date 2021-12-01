using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent.Soldiers;

namespace TowerDefence_SharedContent.Towers
{
    public class Tower: DrawInfo, Ilevel
    {
        protected CanShootAlgorithm canShootAlgorithm;
        public int Level { get; set; }
        public int[] Price  { get; set; }
        public int[] Range { get; set; }
        public int[] Power { get; set; }
        public double[] RateOfFire { get; set; }
        public List<Ammunition> Ammunition { get; set; }
        public TowerType TowerType { get; set; }
        public int ShootingCooldown { get; set; }
        public PlayerType PlayerType { get; set; }

        public Tower(PlayerType playerType, TowerType towerType, Point coordinates)
        {
            Level = 0;
            Coordinates = coordinates;
            Ammunition = new List<Ammunition>();
            TowerType = towerType;
            Sprite = SpritePaths.getTower(playerType, towerType);
            ShootingCooldown = 0;
            PlayerType = playerType;
            Rotation = playerType == PlayerType.PLAYER1 ? 90 : -90;
        }

        public Tower(int level, int[] price, Point coordinates, int[] range, int[]power, double[]rateOfFire,
            string sprite, List<Ammunition> ammunition, TowerType towerType, int shootingCooldown, PlayerType playerType)
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
            ShootingCooldown = shootingCooldown;
            PlayerType = playerType;
            Rotation = playerType == PlayerType.PLAYER1 ? 90 : -90;
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
            ShootingCooldown--;
            for (int i = 0; i < soldiers.Count; i++)
            {
                var soldier = soldiers[i];
                if (canShootAlgorithm.CanShoot(soldier.Coordinates))
                {
                    Shoot();
                }
                for (int k = 0; k < Ammunition.Count; k++)
                {
                    if (this.Ammunition[k].CanDestroy(soldier.Coordinates, playerType))
                    {
                        soldier.CurrentHitpoints -= Ammunition[k].Power;
                        if (soldier.CurrentHitpoints <= 0)
                        {
                            soldiers.RemoveAt(i);
                            i--;
                            if (TowerType == TowerType.Laser)
                            {
                                Ammunition.Clear();
                                k = 0;
                            }
                        }
                        if (TowerType != TowerType.Laser)
                        {
                            Ammunition.RemoveAt(k);
                            k--;
                        }
                    }
                }
            }
        }

        public void Shoot()
        {
            if(ShootingCooldown > 0)
            {
                return;
            }
            ShootingCooldown = (int)(600/RateOfFire[Level]);
            GameElementFactory ammunitionFactory = new AmmunitionFactory();
            MyConsole.WriteLineWithCount("----- Strategy -----");
            if (this is MiniGunTower)
                Ammunition.Add(ammunitionFactory.CreateAmmunition(this.Coordinates, AmmunitionType.Bullet, Power[Level], this.PlayerType));
            else if (this is RocketTower)
                Ammunition.Add(ammunitionFactory.CreateAmmunition(this.Coordinates, AmmunitionType.Rocket, Power[Level], this.PlayerType));
            else if  (this is LaserTower)
                Ammunition.Add(ammunitionFactory.CreateAmmunition(this.Coordinates, AmmunitionType.Laser, Power[Level], this.PlayerType));
        }
    }
}
