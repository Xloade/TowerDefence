using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Towers;
using TowerDefence_SharedContent;


namespace TowerDefence_ClientSide.Prototype
{
    static class AmunitionStore
    {
        private static Dictionary<AmmunitionType, Shape> ammunition = new Dictionary<AmmunitionType, Shape>();

        static AmunitionStore()
        {
            ammunition.Add(AmmunitionType.Bullet, new BulletShape());
            ammunition.Add(AmmunitionType.Laser, new LaserShape());
            ammunition.Add(AmmunitionType.Rocket, new RocketShape());
        }

        public static Shape getAmunitionShape(AmmunitionType type)
        {
            MyConsole.WriteLineWithCount($"Prototype: cloning {type.ToString()}");
            return (Shape)ammunition[type].Clone();
        }
    }
}
