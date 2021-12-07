using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;
using TowerDefence_ClientSide.Composite;
using System.Drawing;
using System.Linq;
using TowerDefence_ClientSide.Prototype;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide
{
    class MapUpdater
    {
        private readonly LazyImageDictionary imageDictionary = new LazyImageDictionary();
        private readonly IHaveShapePlatoon ShapePlatoon;
        private readonly ShapePlatoon DefaultPlatoon;
        private readonly ShapePlatoon Platoon1;
        private readonly ShapePlatoon Platoon2;
        private readonly ShapePlatoon Enemy;
        private readonly PlayerType CurrentPlayerType;
        public ShapePlatoon Root => ShapePlatoon.Shapes;

        public MapUpdater(IHaveShapePlatoon shapePlatoon, PlayerType playerType)
        {
            ShapePlatoon = shapePlatoon;
            CurrentPlayerType = playerType;

            DefaultPlatoon = new ShapePlatoon(PlatoonType.DefaultPlatoon);
            Platoon1 = new ShapePlatoon(PlatoonType.Platoon1);
            Platoon2 = new ShapePlatoon(PlatoonType.Platoon2);
            Enemy = new ShapePlatoon(PlatoonType.Enemy);
            Root.Shapes.Add(DefaultPlatoon);
            Root.Shapes.Add(Platoon1);
            Root.Shapes.Add(Platoon2);
            Root.Shapes.Add(Enemy);
        }
        public void UpdateMap(Map map, out Image bgImage, IPlayerStats playerStats)
        {
            IStats newPlayerStats = new PlayerStats(map.GetPlayer(CurrentPlayerType));
            UpdateStatsView(playerStats, newPlayerStats);

            UpdateMapColor(map.BackgroundImageDir, out bgImage);

            GetNewShapes(map);
            DeleteOldShapes(map);
            Root.UpdatePlatoon(PlatoonType.DefaultPlatoon);
        }

        private void DeleteOldShapes(Map map)
        {
            List<DrawInfo> allIdableObjects = new List<DrawInfo>();
            foreach (Player player in map.Players)
            {
                allIdableObjects.AddRange(player.Soldiers);
                allIdableObjects.AddRange(player.Towers);
                foreach (Tower tower in player.Towers)
                {
                    allIdableObjects.AddRange(tower.Ammunition);
                }
            }

            foreach (Shape shape in Root)
            {
                DrawInfo newShapeInfo = allIdableObjects.Find(x => x.Id == shape.Info.Id);
                if (newShapeInfo != null)
                {
                    shape.Info = newShapeInfo;
                }
                else
                {
                    Root.DeleteShape(shape);
                }
            }
        }

        private void GetNewShapes(Map map)
        {
            List<Shape> currentShapes = Root.ToList();
            foreach (Player player in map.Players)
            {
                ShapePlatoon platoonToUse = player.PlayerType == CurrentPlayerType ? DefaultPlatoon : Enemy;
                GetNewSoldiers(player.Soldiers, platoonToUse, currentShapes);
                GetNewTowers(player.Towers, platoonToUse, currentShapes);
            }

        }
        private void GetNewSoldiers(List<Soldier> soldiers, ShapePlatoon shapePlatoon, List<Shape> currentShapes)
        {
            soldiers.ForEach((soldier) =>
            {
                if (!currentShapes.Exists(x => x.Info.Id == soldier.Id))
                {
                    Shape firstWrap = new Shape(soldier, 100, 100, imageDictionary.Get(soldier.Sprite));
                    IDraw secondWrap = new LvlDrawDecorator(firstWrap, firstWrap);
                    IDraw thirdWrap = new PlatoonDecorator(secondWrap, firstWrap);
                    IDraw fourthWrap = new HpDrawDecorator(thirdWrap, firstWrap);
                    firstWrap.DecoratedDrawInterface = fourthWrap;
                    shapePlatoon.Shapes.Add(firstWrap);
                }
            });
        }

        private void GetNewTowers(List<TowerDefence_SharedContent.Towers.Tower> towers, ShapePlatoon shapePlatoon, List<Shape> currentShapes)
        {
            towers.ForEach((tower) =>
            {
                if (!currentShapes.Exists(x => x.Info.Id == tower.Id))
                {
                    Shape firstWrap = new Shape(tower, 100, 100, imageDictionary.Get(tower.Sprite));
                    IDraw secondWrap = new LvlDrawDecorator(firstWrap, firstWrap);
                    IDraw thirdWrap = new PlatoonDecorator(secondWrap, firstWrap);
                    firstWrap.DecoratedDrawInterface = thirdWrap;
                    shapePlatoon.Shapes.Add(firstWrap);
                }
                GetNewAmmunition(tower.Ammunition, shapePlatoon, currentShapes);
            });
        }

        private void GetNewAmmunition(List<Ammunition> ammunition, ShapePlatoon shapePlatoon, List<Shape> currentShapes)
        {
            ammunition.ForEach((amm) =>
            {
                if (!currentShapes.Exists(x => x.Info.Id == amm.Id))
                {
                    Shape temp = AmunitionStore.GetAmunitionShape(amm.AmmunitionType);
                    temp.Info = amm;
                    shapePlatoon.Shapes.Add(temp);
                }
            });
        }

        private void UpdateMapColor(string image, out Image bgImage)
        {
            bgImage = imageDictionary.Get(image);
        }

        private void UpdateStatsView(IPlayerStats playerStats, IStats stats)
        {
            switch (playerStats.PlayerStatsShowStatus)
            {
                case PlayerStatsShowStatus.All:
                    int[] newPlayerStats = stats.Show();
                    playerStats.LifePointsText = $"Lifepoints: {newPlayerStats[0]}";
                    playerStats.TowerCurrencyText = $"Tower Currency: {newPlayerStats[1]}";
                    playerStats.SoldierCurrencyText = $"Soldier Currency: {newPlayerStats[2]}";
                    break;
                case PlayerStatsShowStatus.Lifepoints:
                    int lifepoints = stats.ShowParameter(playerStats.PlayerStatsShowStatus);
                    playerStats.LifePointsText = $"Lifepoints: {lifepoints}";
                    break;
                case PlayerStatsShowStatus.TowerCurrency:
                    int towerCurrency = stats.ShowParameter(playerStats.PlayerStatsShowStatus);
                    playerStats.LifePointsText = $"Lifepoints: {towerCurrency}";
                    break;
                case PlayerStatsShowStatus.SoldierCurrency:
                    int soldierCurrency = stats.ShowParameter(playerStats.PlayerStatsShowStatus);
                    playerStats.LifePointsText = $"Lifepoints: {soldierCurrency}";
                    break;
            }
        }
    }
}
