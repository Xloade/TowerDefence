using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerDefence_ServerSide;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TowerDefence_SharedContent;

namespace TowerDefence_ServerSide.Tests
{
    [TestClass()]
    public class MapFactoryTests
    {
        private MapFactory mapFactory;
        [TestInitialize()]
        public void Setup()
        {
            mapFactory = new MapFactory();
        }
        [DataTestMethod()]
        [DataRow("Summer", "Green")]
        [DataRow("Spring", "GreenYellow")]
        [DataRow("Winter", "LightCyan")]
        [DataRow("Autumn", "OrangeRed")]
        [DataRow("RandromName", "OrangeRed")]
        public void CreateMap_checksIfcreatedCorrect_allMatches(string mapType, String correctColor)
        {
            Color color = Color.FromName(correctColor);
            Map map = mapFactory.CreateMap(mapType);
            Assert.AreEqual(map.mapColor,color);
        }
        [DataTestMethod()]
        [DataRow("Summer", "Blue")]
        [DataRow("RandromName", "RandomColor")]
        public void CreateMap_checksIfcreatedCorrect_allDoesntMatch(string mapType, String correctColor)
        {
            Color color = Color.FromName(correctColor);
            Map map = mapFactory.CreateMap(mapType);
            Assert.AreNotEqual(map.mapColor, color);
        }
    }
}