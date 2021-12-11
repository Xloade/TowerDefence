using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;
using Newtonsoft.Json;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers.State;

namespace TowerDefence_SharedContent.Towers
{
    public class Tower: DrawInfo, ILevel
    {
        protected ICanShootAlgorithm CanShootAlgorithm;
        public int Level { get; set; }
        public int[] Price  { get; set; }
        public int[] Range { get; set; }
        public int[] Power { get; set; }
        public double[] RateOfFire { get; set; }

        public List<Ammunition> Ammunition { get; set; }
        public TowerType TowerType { get; set; }
        public int ShootingCooldown { get; set; }
        public PlayerType PlayerType { get; set; }
        public int MaxMagazineSize { get; set; }
        public int ShotsFired { get; set; }
        public int OverheatLevel { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public TowerState State { get; set; }

        public Tower(PlayerType playerType, TowerType towerType, Point coordinates)
        {
            Level = 0;
            Coordinates = coordinates;
            Ammunition = new List<Ammunition>();
            TowerType = towerType;
            Sprite = SpritePaths.GetTower(playerType, towerType);
            ShootingCooldown = 0;
            PlayerType = playerType;
            Rotation = playerType == PlayerType.Player1 ? 90 : -90;
            State = new PrepareNextShotState(this);
            ShotsFired = 0;
        }

        public Tower(int level, int[] price, Point coordinates, int[] range, int[]power, double[]rateOfFire,
            string sprite, List<Ammunition> ammunition, TowerType towerType, int shootingCooldown, PlayerType playerType, bool isReloading, bool isOverheated)
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
            Rotation = playerType == PlayerType.Player1 ? 90 : -90;
            IsReloading = isReloading;
            IsOverheated = isOverheated;
        }

        public void MoveAmmunition(PlayerType type)
        {
            for (var i = 0; i < Ammunition.Count; i++)
            {
                Ammunition[i].MoveForward(type);
                if (!Ammunition[i].IsOutOfMap(type)) continue;
                Ammunition.Remove(Ammunition[i]);
                i--;
            }
        }

        public void Scan(List<Soldier> soldiers, PlayerType playerType)
        {
            ShootingCooldown--;
            for (var i = 0; i < soldiers.Count; i++)
            {
                var soldier = soldiers[i];
                switch (State)
                {
                    case ShootingState _:
                        State.Shoot();
                        break;
                    case ReloadingState _:
                        State.Reload();
                        break;
                    case OverheatState _:
                        State.Cooldown();
                        break;
                    case PrepareNextShotState _:
                        State.Check(CanShootAlgorithm, soldier.Coordinates);
                        break;
                }
                for (var k = 0; k < Ammunition.Count; k++)
                {
                    if (!Ammunition[k].CanDestroy(soldier.Coordinates, playerType)) continue;
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

                    if (TowerType == TowerType.Laser) continue;
                    Ammunition.RemoveAt(k);
                    k--;
                }
            }
        }
    }
}
