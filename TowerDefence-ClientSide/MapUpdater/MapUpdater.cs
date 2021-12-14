using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;
using TowerDefence_ClientSide.Composite;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using TowerDefence_ClientSide.Prototype;
using TowerDefence_ClientSide.shapes;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide
{
    public class MapUpdater
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
        public void UpdateMap(Map map, out Image bgImage, IPlayerStats playerStats,MouseSelection mouseSelection)
        {
            IStats newPlayerStats = new PlayerStats(map.GetPlayer(CurrentPlayerType));
            UpdateStatsView(playerStats, newPlayerStats);

            UpdateMapColor(map.BackgroundImageDir, out bgImage);

            GetNewShapes(map);
            DeleteOldShapes(map);
            UpdatePermaSelection();
            UpdateTempSelection(mouseSelection);
            Root.UpdatePlatoon(PlatoonType.DefaultPlatoon);
        }

        public void RemoveOneSelection()
        {
            Root.RemoveDeepestSelection();
            UpdatePermaSelection();
        }
        public void RemoveAllSelection()
        {
            Root.RemoveAllSelections();
            UpdatePermaSelection();
        }
        private void UpdatePermaSelection()
        {
            Root.UpdateSelection(PlatoonType.Root);
        }
        private void UpdateTempSelection(MouseSelection selection)
        {
            foreach (var shape in Root)
            {
                if (shape.CenterX > selection.Left && shape.CenterX < selection.Right &&
                    shape.CenterY > selection.Top && shape.CenterY < selection.Bot && selection.Selected)
                {
                    shape.Selected = true;
                }
            }
        }
        public void SaveSelection(MouseSelection selection)
        {
            Root.SaveSelection(selection);
            Root.UpdatePlatoon(PlatoonType.DefaultPlatoon);
        }

        public void SelectAll()
        {
            MouseSelection mouseSelection = new MouseSelection();
            mouseSelection.StartPoint = new Point(0, 0);
            mouseSelection.EndPoint = new Point(int.MaxValue, int.MaxValue);
            Root.SaveSelection(mouseSelection);
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
                    Shape firstWrap = new Shape(tower, 100, 100, imageDictionary.Get(tower.Sprite));
                    IDraw secondWrap = new LvlDrawDecorator(firstWrap, firstWrap);
                    IDraw thirdWrap = new PlatoonDecorator(secondWrap, firstWrap);
                    IDraw fourthWrap = new SelectDrawDecorator(thirdWrap, firstWrap);
                    IDraw fifthWrap = new StateDecorator(fourthWrap, firstWrap);
                    firstWrap.DecoratedDrawInterface = fifthWrap;
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

        private ShapePlatoon GetPlatoon(PlatoonType type)
        {
            return type switch
            {
                PlatoonType.Root => Root,
                PlatoonType.Platoon1 => Platoon1,
                PlatoonType.Platoon2 => Platoon2,
                PlatoonType.DefaultPlatoon => DefaultPlatoon,
                _ => null,
            };
        }
        public void TransferSelectToPlatoon(PlatoonType type)
        {
            GetPlatoon(type).Shapes.AddRange(Root.RemoveAllRootSelections());
            Root.UpdatePlatoon(PlatoonType.DefaultPlatoon);
        }

        public void TransferFromPlatoonToPlatoon(PlatoonType donor, PlatoonType recipient)
        {
            GetPlatoon(recipient).Shapes.AddRange(GetPlatoon(donor).Shapes);
            GetPlatoon(donor).Shapes.Clear();
            Root.UpdatePlatoon(PlatoonType.DefaultPlatoon);
        }

        public List<Shape> GetSelectedShapes()
        {
            return Root.OfType<Shape>().Where(x => x.Selected).ToList();
        }
    }
}
