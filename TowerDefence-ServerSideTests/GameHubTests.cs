using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerDefence_ServerSide;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ServerSide.Tests
{
    [TestClass()]
    public class GameHubTests
    {

        private GameHub gameHub;
        private MapController mapController;
        private Map map;
        [TestInitialize()]
        public void Setup()
        {
            gameHub = new GameHub();
            var gameHubMock = new Mock<Microsoft.AspNetCore.SignalR.IHubContext<GameHub>>();
            MapController.SetIHubContext(gameHubMock.Object);
            MapFactory factory = new MapFactory();
            MapController.CreateInstance();
            map = factory.CreateMap("Winter");
            mapController = MapController.GetInstance();
            mapController.Attach(map);
            mapController.AddPlayer(PlayerType.Player1);
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            MapController.RemoveInstance();
        }
        [TestMethod()]
        public void CreateMapTest()
        {
            MapController.RemoveInstance();
            gameHub.CreateMap("Winter");
            Assert.IsNotNull(MapController.GetInstance());
        }

        [DataTestMethod()]
        [DataRow(SoldierType.HitpointsSoldier)]
        [DataRow(SoldierType.SpeedSoldier)]
        public void BuySoldierTest(SoldierType soldierType)
        {

            gameHub.BuySoldier(PlayerType.Player1, soldierType);
            Player player = map.GetPlayer(PlayerType.Player1);
            Assert.AreEqual(soldierType, player.Soldiers[^1].SoldierType);
        }

        [TestMethod()]
        public void BuyTowerTest()
        {
            gameHub.BuyTower(PlayerType.Player1, TowerType.Minigun, new System.Drawing.Point(100, 100));
            Assert.AreEqual(1, map.GetPlayer(PlayerType.Player1).Towers.Count);
        }

        [TestMethod()]
        public void RestartGameTest()
        {
            gameHub.BuyTower(PlayerType.Player1, TowerType.Minigun, new System.Drawing.Point(100, 100));
            gameHub.RestartGame();
            Assert.AreEqual(0, map.GetPlayer(PlayerType.Player1).Towers.Count);
        }

        [DataTestMethod()]
        [DataRow(PlayerType.Player1)]
        [DataRow(PlayerType.Player2)]
        public void AddPlayerTest(PlayerType playerType)
        {
            gameHub.AddPlayer(playerType);
            Assert.IsNotNull(map.GetPlayer(playerType));
        }
    }
}