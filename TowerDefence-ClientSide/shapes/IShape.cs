using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;
using TowerDefence_ClientSide.Composite;
namespace TowerDefence_ClientSide.shapes
{
    public interface IShape
    {
        public DrawInfo Info { get; }
        public PlatoonType PlatoonType { get; set; }
    }
}
