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
        [DataRow("Summer", "Summer")]
        [DataRow("Spring", "Spring")]
        [DataRow("Winter", "Winter")]
        [DataRow("Autumn", "Autumn")]
        [DataRow("RandromName", "Autumn")]
        public void CreateMap_checksIfcreatedCorrect_allMatches(string mapType, string mapTypeToGetDir)
        {
            string correctDir = SpritePaths.GetMap(mapTypeToGetDir);
            Map map = mapFactory.CreateMap(mapType);
            Assert.AreEqual(map.BackgroundImageDir, correctDir);
        }
    }
}