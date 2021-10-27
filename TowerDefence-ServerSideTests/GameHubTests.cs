using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerDefence_ServerSide;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TowerDefence_SharedContent;


namespace TowerDefence_ServerSide.Tests
{
    [TestClass()]
    public class GameHubTests
    {

        private GameHub gameHub;
        private MapController mapController;
        [TestInitialize()]
        public void Setup()
        {
            gameHub = new GameHub();
            var gameHubMock = new Mock<Microsoft.AspNetCore.SignalR.IHubContext<GameHub>>();
            MapController.setIHubContext(gameHubMock.Object);
            MapController.createInstance("Winter");
            mapController = MapController.getInstance();
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
            gameHub.buySoldier(PlayerType.PLAYER1);
            Assert.AreEqual(1, mapController.map.GetPlayer(PlayerType.PLAYER1).soldiers.Count);
        }

        [TestMethod()]
        public void buyTowerTest()
        {
            gameHub.buyTower(PlayerType.PLAYER1);
            Assert.AreEqual(1, mapController.map.GetPlayer(PlayerType.PLAYER1).towers.Count);
        }

        [TestMethod()]
        public void restartGameTest()
        {
            gameHub.restartGame();
            Assert.AreNotSame(mapController, MapController.getInstance());
        }

        [TestMethod()]
        public void deleteTowerTest()
        {
            gameHub.buyTower(PlayerType.PLAYER1);
            gameHub.deleteTower(PlayerType.PLAYER1);
            Assert.AreEqual(0, mapController.map.GetPlayer(PlayerType.PLAYER1).towers.Count);
        }
    }
}