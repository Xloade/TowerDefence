using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerDefence_ServerSide;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;

namespace TowerDefence_ServerSide.Tests
{
    [TestClass()]
    public class MapControllerTests
    {
        private MapController mapController;

        private void Setup()
        {
            var gameHub = new Mock<IHubContext<GameHub>>();
            mapController = new MapController(gameHub.Object);
        }

        [TestMethod()]
        public void destroyTower_checksIfCanDestroy_destroys()
        {
            Setup();

            var result = mapController.CanDestroy(new Point(1, 2), new Point(1, 5));

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void destroyTower_checksIfCanDestroy_doesntDestroy()
        {
            Setup();

            var result = mapController.CanDestroy(new Point(1, 2), new Point(2, 5));

            Assert.IsFalse(result);
        }
    }
}