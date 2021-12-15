using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;
using Newtonsoft.Json;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers.State;
using TowerDefence_SharedContent.Ammunition;

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

        public List<Ammunition.Ammunition> Ammunition { get; set; }
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
            Ammunition = new List<Ammunition.Ammunition>();
            TowerType = towerType;
            Sprite = SpritePaths.GetTower(playerType, towerType);
            ShootingCooldown = 0;
            PlayerType = playerType;
            Rotation = playerType == PlayerType.Player1 ? 90 : -90;
            State = new PrepareNextShotState(this);
            ShotsFired = 0;
        }

        public Tower(int level, int[] price, Point coordinates, int[] range, int[]power, double[]rateOfFire,
            string sprite, List<Ammunition.Ammunition> ammunition, TowerType towerType, int shootingCooldown, PlayerType playerType, bool isReloading, bool isOverheated)
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
            var ammunitionRemoveList = new List<Ammunition.Ammunition>();
            foreach (var currentAmmunition in new AmmunitionList(Ammunition))
            {
                if (currentAmmunition.IsOutOfMap(type))
                {
                    ammunitionRemoveList.Add(currentAmmunition);
                }
                else
                {
                    break;
                }
            }
            ammunitionRemoveList.ForEach(x=>Ammunition.Remove(x));
            foreach (var currentAmmunition in Ammunition)
            {
                currentAmmunition.MoveForward(type);
            }
        }

        public void Scan(List<Soldier> soldiers, PlayerType playerType)
        {
            ShootingCooldown--;
            foreach (var soldier in soldiers)
            {
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
            }
            var ammunitionRemoveList = new List<Ammunition.Ammunition>();
            foreach (var currentAmmunition in Ammunition)
            {
                var soldierRemoveList = new List<Soldier>();
                foreach (var soldier in new SoldierList(soldiers, currentAmmunition))
                {
                    //closests sorted first so after first not destroyable break
                    if (!currentAmmunition.CanDestroy(soldier.Coordinates, playerType)) break;
                    soldier.CurrentHitpoints -= currentAmmunition.Power;
                    if (soldier.CurrentHitpoints <= 0)
                    {
                        soldierRemoveList.Add(soldier);
                    }
                    ammunitionRemoveList.Add(currentAmmunition);
                    break;
                }
                soldierRemoveList.ForEach(x => soldiers.Remove(x));
            }
            if (TowerType == TowerType.Laser)
            {
                Ammunition.Clear();
            }
            else
            {
                ammunitionRemoveList.ForEach(x => Ammunition.Remove(x));
            }
        }

       
    }
}
