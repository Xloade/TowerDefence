using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;
using TowerDefence_ClientSide.Composite;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using TowerDefence_ClientSide.Prototype;
using TowerDefence_ClientSide.shapes;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide
{
    class MapUpdater
    {
        private LazyImageDictionary imageDictionary = new LazyImageDictionary();
        private IHaveShapePlatoon ShapePlatoon;
        private ShapePlatoon DefaultPlatoon;
        private ShapePlatoon Platoon1;
        private ShapePlatoon Platoon2;
        private ShapePlatoon Enemy;
        private PlayerType CurrentPlayerType;
        public ShapePlatoon root
        {
            get { return ShapePlatoon.Shapes; }
        }
        public MapUpdater(IHaveShapePlatoon shapePlatoon, PlayerType playerType)
        {
            ShapePlatoon = shapePlatoon;
            CurrentPlayerType = playerType;

            DefaultPlatoon = new ShapePlatoon(PlatoonType.DefaultPlatoon);
            Platoon1 = new ShapePlatoon(PlatoonType.Platoon1);
            Platoon2 = new ShapePlatoon(PlatoonType.Platoon2);
            Enemy = new ShapePlatoon(PlatoonType.Enemy);
            root.Shapes.Add(DefaultPlatoon);
            root.Shapes.Add(Platoon1);
            root.Shapes.Add(Platoon2);
            root.Shapes.Add(Enemy);
        }
        public void UpdateMap(Map map, out Image bgImage, IPlayerStats playerStats,MouseSelection mouseSelection)
        {
            IStats newPlayerStats = new PlayerStats(map.GetPlayer(CurrentPlayerType));
            UpdateStatsView(playerStats, newPlayerStats);

            updateMapColor(map.backgroundImageDir, out bgImage);

            GetNewShapes(map);
            DeleteOldShapes(map);
            root.UpdatePlatoon(PlatoonType.Root);
            UpdateTempSelection(mouseSelection);
            UpdatePermaSelection(mouseSelection);
        }
        private void UpdatePermaSelection(MouseSelection selection)
        {
            root.UpdateSelection(PlatoonType.Root);
        }
        private void UpdateTempSelection(MouseSelection selection)
        {
            foreach (var shape in root)
            {
                if (shape.CenterX > selection.Left && shape.CenterX < selection.Right &&
                    shape.CenterY > selection.Top && shape.CenterY < selection.Bot && selection.Selected)
                {
                    shape.Selected = true;
                }
                else
                {
                    shape.Selected = false;
                }
            }
        }
        public void SaveSelection(MouseSelection selection)
        {
            root.SaveSelection(selection);
        }

        private void DeleteOldShapes(Map map)
        {
            List<DrawInfo> allIdableObjects = new List<DrawInfo>();
            foreach (Player player in map.players)
            {
                allIdableObjects.AddRange(player.soldiers);
                allIdableObjects.AddRange(player.towers);
                foreach (Tower tower in player.towers)
                {
                    allIdableObjects.AddRange(tower.Ammunition);
                }
            }

            foreach (Shape shape in root)
            {
                DrawInfo newShapeInfo = allIdableObjects.Find(x => x.Id == shape.Info.Id);
                if (newShapeInfo != null)
                {
                    shape.Info = newShapeInfo;
                }
                else
                {
                    root.DeleteShape(shape);
                }
            }
        }

        private void GetNewShapes(Map map)
        {
            List<Shape> currentShapes = root.ToList();
            foreach (Player player in map.players)
            {
                ShapePlatoon platoonToUse = player.PlayerType == CurrentPlayerType ? DefaultPlatoon : Enemy;
                GetNewSoldiers(player.soldiers, platoonToUse, currentShapes);
                GetNewTowers(player.towers, platoonToUse, currentShapes);
            }

        }
        private void GetNewSoldiers(List<Soldier> soldiers, ShapePlatoon shapePlatoon, List<Shape> currentShapes)
        {
            soldiers.ForEach((soldier) =>
            {
                if (!currentShapes.Exists(x => x.Info.Id == soldier.Id))
                {
                    Shape firstWrap = new Shape(soldier, 100, 100, imageDictionary.get(soldier.Sprite));
                    IDraw secondWrap = new LvlDrawDecorator(firstWrap, firstWrap);
                    IDraw thirdWrap = new PlatoonDecorator(secondWrap, firstWrap);
                    IDraw fourthWrap = new HpDrawDecorator(thirdWrap, firstWrap);
                    IDraw fithWrap = new SelectDrawDecorator(fourthWrap, firstWrap);
                    firstWrap.DecoratedDrawInterface = fithWrap;
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
                    Shape firstWrap = new Shape(tower, 100, 100, imageDictionary.get(tower.Sprite));
                    IDraw secondWrap = new LvlDrawDecorator(firstWrap, firstWrap);
                    IDraw thirdWrap = new PlatoonDecorator(secondWrap, firstWrap);
                    IDraw fourthWrap = new SelectDrawDecorator(thirdWrap, firstWrap);
                    firstWrap.DecoratedDrawInterface = fourthWrap;
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
                    Shape temp = AmunitionStore.getAmunitionShape(amm.AmmunitionType);
                    temp.Info = amm;
                    shapePlatoon.Shapes.Add(temp);
                }
            });
        }

        private void updateMapColor(string image, out Image bgImage)
        {
            bgImage = imageDictionary.get(image);
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
