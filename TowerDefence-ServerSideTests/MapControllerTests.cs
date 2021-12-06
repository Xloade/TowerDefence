using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerDefence_ServerSide;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;


namespace TowerDefence_ServerSide.Tests
{
    [TestClass()]
    public class MapControllerTests
    {
        private MapController mapController;
        Map map;
        MapFactory factory;
        [TestInitialize()]
        public void Setup()
        {
            var gameHub = new Mock<IHubContext<GameHub>>();
            MapController.SetIHubContext(gameHub.Object);
            factory = new MapFactory();
            MapController.CreateInstance();
            map = factory.CreateMap("Winter");
            mapController = MapController.GetInstance();
            mapController.Attach(map);
        }
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void removeInstance_checksIfEmpty_ExceptionTrown()
        {
            MapController.RemoveInstance();
            MapController mapController = MapController.GetInstance();
            Assert.Fail("Expected Exception");
        }
        [TestMethod()]
        public void createInstance_checksIfCreated_getsMapcontroller()
        {
            MapController.CreateInstance();
            MapController mapController = MapController.GetInstance();
            Assert.IsNotNull(mapController);
        }
        [TestMethod()]
        public void restartInstance_checksIfSoldersRemoved_areRemoved()
        {
            MapController.CreateInstance();
            MapController mapControllerFromSingleton = MapController.GetInstance();
            Map mapFromSingleton = factory.CreateMap("Winter");
            mapControllerFromSingleton.Attach(mapFromSingleton);
            mapFromSingleton.AddPlayer(PlayerType.Player1);
            Barrack barrack = new Barrack();
            HitpointsSoldierBuilder hitpointsSoldierBuilder = new HitpointsSoldierBuilder(PlayerType.Player1, SoldierType.HitpointsSoldier, 1);
            barrack.Train((SoldierBuilder)hitpointsSoldierBuilder, PlayerType.Player1);
            mapControllerFromSingleton.AddSoldier(hitpointsSoldierBuilder.Soldier, PlayerType.Player1);

            MapController.RestartInstance();

            var numOfsoldiers = mapFromSingleton.GetPlayer(PlayerType.Player1).Soldiers.Count;
            Assert.AreEqual(0, numOfsoldiers);
        }
        [TestMethod()]
        public void deattach_checksIfPlayerAdded_FailsToAdd(){
            mapController.Deattach(map);
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=>{
                mapController.AddPlayer(PlayerType.Player1);
            });
        }
    }
}