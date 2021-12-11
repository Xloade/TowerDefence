using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_SharedContent.Towers.State
{
    public class ShootingState : TowerState, IStateChange
    {
        public ShootingState(Tower tower)
        {
            Tower = tower;
        }

        public ShootingState(TowerState state) : this(state.Tower) { }
        public override void Shoot()
        {
            if (Tower.ShootingCooldown > 0)
            {
                return;
            }
            GameElementFactory ammunitionFactory = new AmmunitionFactory();
            MyConsole.WriteLineWithCount("----- Strategy -----");
            Tower.ShootingCooldown = (int)(600 / Tower.RateOfFire[Tower.Level]);
            switch (Tower)
            {
                case MiniGunTower _:
                    Tower.Ammunition.Add(ammunitionFactory.CreateAmmunition(Tower.Coordinates, AmmunitionType.Bullet, Tower.Power[Tower.Level], Tower.PlayerType));
                    break;
                case RocketTower _:
                    Tower.Ammunition.Add(ammunitionFactory.CreateAmmunition(Tower.Coordinates, AmmunitionType.Rocket, Tower.Power[Tower.Level], Tower.PlayerType));
                    break;
                case LaserTower _:
                    Tower.Ammunition.Add(ammunitionFactory.CreateAmmunition(Tower.Coordinates, AmmunitionType.Laser, Tower.Power[Tower.Level], Tower.PlayerType));
                    break;
            }
            MyConsole.WriteLineWithCount("----- State: shot state -----");
            Tower.ShotsFired++;
            Tower.OverheatLevel++;
            OnStateChange();
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        public override void Cooldown()
        {
            throw new NotImplementedException();
        }

        public override void Check(ICanShootAlgorithm canShootAlgorithm, Point soldierCoordinates)
        {
            if (!canShootAlgorithm.CanShoot(soldierCoordinates))
            {
                Tower.State = new PrepareNextShotState(this);
            }
        }

        public void OnStateChange()
        {
            if (Tower.MaxMagazineSize == Tower.ShotsFired)
            {
                Tower.State = new ReloadingState(this);
            }
            else if (Tower.OverheatLevel == 30)
            {
                Tower.State = new OverheatState(this);
            }
        }
    }
}
