using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Towers;
using TowerDefence_SharedContent;


namespace TowerDefence_ClientSide.Prototype
{
    static class AmunitionStore
    {
        private static Dictionary<AmmunitionType, Shape> _ammunition = new Dictionary<AmmunitionType, Shape>();

        static AmunitionStore()
        {
            _ammunition.Add(AmmunitionType.Bullet, new BulletShape());
            _ammunition.Add(AmmunitionType.Laser, new LaserShape());
            _ammunition.Add(AmmunitionType.Rocket, new RocketShape());
        }

        public static Shape GetAmunitionShape(AmmunitionType type)
        {
            MyConsole.WriteLineWithCount($"Prototype: cloning {type.ToString()}");
            Shape shape = (Shape)_ammunition[type].Clone();
            shape.DecoratedDrawInterface = shape;
            return shape;

        }
    }
}
