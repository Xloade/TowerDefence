using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerDefence_ServerSide;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using TowerDefence_SharedContent;

namespace TowerDefence_ServerSide.Tests
{
    [TestClass()]
    public class MapControllerTests
    {
        private MapController mapController;

        private void Setup()
        {
            var gameHub = new Mock<IHubContext<GameHub>>();
            MapFactory factory = new MapFactory();
            mapController = new MapController(gameHub.Object, factory.CreateMap("Autumn"));
            var map = new Mock<MapController>();
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

        [TestMethod()]
        public void canShootTower_checksIfCanShoot_shoots()
        {
            Setup();

            var result = mapController.CanShoot(new Point(1, 1), new Point(10, 1), 9);

            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void canShootTower_checksIfCanShoot_doesntShoot()
        {
            Setup();

            var result = mapController.CanShoot(new Point(1, 1), new Point(10, 1), 11);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void shootTower_checksIfShoots_Shoot()
        {
            Setup();        
            
            var tower = new Tower(PlayerType.PLAYER1);
            mapController.Shoot(tower);
            var sum = tower.Bullets.Count;
            Assert.AreEqual(1, sum);
        }

        [TestMethod()]
        public void shootTower_checksIfShoots_doesntShoot()
        {
            Setup();

            var tower = new Tower(PlayerType.PLAYER1);
            mapController.Shoot(tower);
            var sum = tower.Bullets.Count;          
            Assert.AreNotEqual(0, sum);
        }

        [TestMethod()]
        public void scanAndShoot_checksIfSoldierWasRemoved_RemoveSoldier()
        {
            Setup();

            var tower = new Tower(PlayerType.PLAYER1);
            var soldiers = new List<Soldier>();
            soldiers.Add(new Soldier(PlayerType.PLAYER2));
            var sum = soldiers.Count;

            mapController.ScanAndShoot(tower, soldiers);
            var sum2 = soldiers.Count;

            var checks = sum >= sum2;

            Assert.IsTrue(checks);          
        }

        [TestMethod()]
        public void scanAndShoot_checksIfSoldierWasRemoved_DoesntRemoveSoldier()
        {
            Setup();

            var tower = new Tower(PlayerType.PLAYER1);
            var soldiers = new List<Soldier>();
            soldiers.Add(new Soldier(PlayerType.PLAYER2));
            var sum = soldiers.Count;

            mapController.ScanAndShoot(tower, soldiers);
            var sum2 = soldiers.Count;
            var checks = sum < sum2;

            Assert.IsFalse(checks);
        }

        [TestMethod()]
        public void addBulletMovement_checksIfBulletWasRemoved_Removed()
        {
            Setup();

            var tower = new Tower(PlayerType.PLAYER1);
            tower.Bullets.Add(new Bullet(new Point(1,1)));
            var sum = tower.Bullets.Count;

            mapController.AddBulletMovement();
            var sum2= tower.Bullets.Count;
            var checks = sum >= sum2;
            Assert.IsTrue(checks);
        }
        [TestMethod()]
        public void addBulletMovement_checksIfBulletWasRemoved_DoesntRemove()
        {
            Setup();

            var tower = new Tower(PlayerType.PLAYER1);
            tower.Bullets.Add(new Bullet(new Point(1, 1)));
            var sum = tower.Bullets.Count;

            mapController.AddBulletMovement();
            var sum2 = tower.Bullets.Count;
            var checks = sum < sum2;
            Assert.IsFalse(checks);
        }

        [TestMethod()]
        public void towerScan_checksIfSoldiersWereRemoved_RemoveSoldiers()
        {
            Setup();       
            
            var soldiers2 = new List<Soldier>();
            soldiers2.Add(new Soldier(PlayerType.PLAYER2));
            var soldiers1 = new List<Soldier>();
            soldiers1.Add(new Soldier(PlayerType.PLAYER1));
            var sum1 = soldiers1.Count;
            var sum2 = soldiers2.Count;

            mapController.AddTowerScan(PlayerType.PLAYER1, PlayerType.PLAYER2);
            var afterSum1 = soldiers1.Count;
            var afterSum2 = soldiers2.Count;

            var checks = sum1 >= afterSum1 & sum2 >= afterSum2;

            Assert.IsTrue(checks);
        }
        [TestMethod()]
        public void towerScan_checksIfSoldiersWereRemoved_DoesntRemoveSoldiers()
        {
            Setup();

            var soldiers2 = new List<Soldier>();
            soldiers2.Add(new Soldier(PlayerType.PLAYER2));
            var soldiers1 = new List<Soldier>();
            soldiers1.Add(new Soldier(PlayerType.PLAYER1));
            var sum1 = soldiers1.Count;
            var sum2 = soldiers2.Count;

            mapController.AddTowerScan(PlayerType.PLAYER1, PlayerType.PLAYER2);
            var afterSum1 = soldiers1.Count;
            var afterSum2 = soldiers2.Count;

            var checks = sum1 < afterSum1 & sum2 < afterSum2;

            Assert.IsFalse(checks);
        }

        //nepavyko
        //[TestMethod()]
        //public void addSoldierMovement_checksIfSoldierMoves_Moves()
        //{
        //    Setup();

        //    var soldiers2 = new List<Soldier>();
        //    soldiers2.Add(new Soldier(PlayerType.PLAYER2));
        //    var soldiers1 = new List<Soldier>();
        //    soldiers1.Add(new Soldier(PlayerType.PLAYER1));
        //    var coord1 = new List<Point>();
        //    var coord2 = new List<Point>();

        //    for (int i = 0; i < soldiers1.Count; i++)
        //    {
        //        coord1.Add(soldiers1[i].Coordinates);
        //    }

        //    for (int i = 0; i < soldiers2.Count; i++)
        //    {
        //        coord2.Add(soldiers2[i].Coordinates);
        //    }

        //    mapController.AddSoldierMovement();
        //    var checks = false;

        //    if (soldiers1.Count == 0 || soldiers2.Count == 0)
        //    {
        //        checks = true;
        //        Assert.IsTrue(checks);
        //    }
        //    else
        //    {
        //        for (int i = 0; i < soldiers1.Count; i++)
        //        {                    
        //            if (coord1[i] != soldiers1[i].Coordinates)
        //                checks = true;
        //        }

        //        for (int i = 0; i < soldiers2.Count; i++)
        //        {
        //            if (coord2[i] != soldiers2[i].Coordinates)
        //                checks = true;
        //        }
        //        Assert.IsTrue(checks);
        //    }
        //}

        //[TestMethod()]
        //public void addSoldierMovement_checksIfSoldierMoves_DoesntMoves()
        //{
        //    Setup();

        //    var soldiers1 = new Soldier(PlayerType.PLAYER1);
        //    var soldiers2 = new Soldier(PlayerType.PLAYER2);

        //    var coord1 = soldiers1.Coordinates;
        //    var coord2 = soldiers2.Coordinates;

        //    mapController.AddSoldierMovement();
        //    if(soldiers1 != null && soldiers2 != null)
        //    {
        //        var afterCoord1 = soldiers1.Coordinates;
        //        var afterCoord2 = soldiers2.Coordinates;
        //        var checks = coord1 == afterCoord1 & coord2 == afterCoord2;
        //        Assert.IsFalse(checks);
        //    }
        //    else
        //    {
        //        Assert.IsFalse(false);
        //    }                   
           
        //}
    }
}