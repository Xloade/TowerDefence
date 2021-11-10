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
            MapController.setIHubContext(gameHubMock.Object);
            MapFactory factory = new MapFactory();
            MapController.createInstance();
            map = factory.CreateMap("Winter");
            mapController = MapController.getInstance();
            mapController.Attach(map);
            mapController.AddPlayer(PlayerType.PLAYER1);
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            MapController.removeInstance();
        }
        [TestMethod()]
        public void createMapTest()
        {
            MapController.removeInstance();
            gameHub.createMap("Winter");
            Assert.IsNotNull(MapController.getInstance());
        }

        [TestMethod()]
        public void buySoldierTest()
        {

            gameHub.buySoldier(PlayerType.PLAYER1, SoldierType.HitpointsSoldier);
            Assert.AreEqual(1, map.GetPlayer(PlayerType.PLAYER1).soldiers.Count);
        }

        [TestMethod()]
        public void buyTowerTest()
        {
            gameHub.buyTower(PlayerType.PLAYER1, TowerType.Minigun, new System.Drawing.Point(100, 100));
            Assert.AreEqual(1, map.GetPlayer(PlayerType.PLAYER1).towers.Count);
        }

        [TestMethod()]
        public void restartGameTest()
        {
            gameHub.buyTower(PlayerType.PLAYER1, TowerType.Minigun, new System.Drawing.Point(100, 100));
            gameHub.restartGame();
            Assert.AreEqual(0, map.GetPlayer(PlayerType.PLAYER1).towers.Count);
        }
    }
}