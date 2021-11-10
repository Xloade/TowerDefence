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
            MapController.setIHubContext(gameHub.Object);
            factory = new MapFactory();
            MapController.createInstance();
            map = factory.CreateMap("Winter");
            mapController = MapController.getInstance();
            mapController.Attach(map);
        }
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void removeInstance_checksIfEmpty_ExceptionTrown()
        {
            MapController.removeInstance();
            MapController mapController = MapController.getInstance();
            Assert.Fail("Expected Exception");
        }
        [TestMethod()]
        public void createInstance_checksIfCreated_getsMapcontroller()
        {
            MapController.createInstance();
            MapController mapController = MapController.getInstance();
            Assert.IsNotNull(mapController);
        }
        [TestMethod()]
        public void restartInstance_checksIfSoldersRemoved_areRemoved()
        {
            MapController.createInstance();
            MapController mapControllerFromSingleton = MapController.getInstance();
            Map mapFromSingleton = factory.CreateMap("Winter");
            mapControllerFromSingleton.Attach(mapFromSingleton);
            mapFromSingleton.AddPlayer(PlayerType.PLAYER1);
            Barrack barrack = new Barrack();
            HitpointsSoldierBuilder hitpointsSoldierBuilder = new HitpointsSoldierBuilder(PlayerType.PLAYER1, SoldierType.HitpointsSoldier, 1);
            barrack.Train((SoldierBuilder)hitpointsSoldierBuilder, PlayerType.PLAYER1);
            mapControllerFromSingleton.AddSoldier(hitpointsSoldierBuilder.Soldier, PlayerType.PLAYER1);

            MapController.restartInstance();

            var numOfsoldiers = mapFromSingleton.GetPlayer(PlayerType.PLAYER1).soldiers.Count;
            Assert.AreEqual(0, numOfsoldiers);
        }
        [TestMethod()]
        public void deattach_checksIfPlayerAdded_FailsToAdd(){
            mapController.Deattach(map);
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=>{
                mapController.AddPlayer(PlayerType.PLAYER1);
            });
        }
    }
}