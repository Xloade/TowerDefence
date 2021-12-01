using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;
using TowerDefence_ClientSide.Composite;
using System.Drawing;
using TowerDefence_ClientSide.Prototype;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide
{
    class MapUpdater
    {
        private LazyImageDictionary imageDictionary = new LazyImageDictionary();
        public void UpdateMap(Map map, ShapePlatoon root,PlayerType playerType,
            out Image bgImage, IPlayerStats playerStats)
        {
            root.Shapes.Clear();
            IStats newPlayerStats = new PlayerStats(map.GetPlayer(playerType));
            UpdateStatsView(playerStats, newPlayerStats);

            updateMapColor(map.backgroundImageDir, out bgImage);

            foreach (Player player in map.players)
            {
                updateSoldiers(player.soldiers, root);
                updateTowers(player.towers, root);
            }
        }


        //rotation temporary
        private void updateMapColor(string image, out Image bgImage)
        {
            bgImage = imageDictionary.get(image);
        }
        private void updateSoldiers(List<Soldier> soldiers, ShapePlatoon shapePlatoon)
        {
            soldiers.ForEach((soldier) =>
            {
                Shape firstWrap = new Shape(soldier, 100, 100, imageDictionary.get(soldier.Sprite));
                IDraw secondWrap = new LvlDrawDecorator(firstWrap, soldier);
                IDraw thirdWrap = new NameDrawDecorator(secondWrap, soldier);
                IDraw fourthWrap = new HpDrawDecorator(thirdWrap, soldier);
                firstWrap.DecoratedDrawInterface = fourthWrap;
                shapePlatoon.Shapes.Add(firstWrap);
            });
        }

        private void updateTowers(List<TowerDefence_SharedContent.Towers.Tower> towers, ShapePlatoon shapePlatoon)
        {
            towers.ForEach((tower) =>
            {
                Shape firstWrap = new Shape(tower, 100, 100, imageDictionary.get(tower.Sprite));
                IDraw secondWrap = new LvlDrawDecorator(firstWrap, tower);
                IDraw thirdWrap = new NameDrawDecorator(secondWrap, tower);
                firstWrap.DecoratedDrawInterface = thirdWrap;
                shapePlatoon.Shapes.Add(firstWrap);
                ShapePlatoon shapePlatoonNew = new ShapePlatoon();
                shapePlatoon.Shapes.Add(shapePlatoonNew);
                updateAmmunition(tower.Ammunition, shapePlatoonNew);
            });
        }

        private void updateAmmunition(List<Ammunition> ammunition, ShapePlatoon shapePlatoon)
        {
            ammunition.ForEach((amm) =>
            {
                Shape temp = AmunitionStore.getAmunitionShape(amm.AmmunitionType);
                temp.Info = amm;
                shapePlatoon.Shapes.Add(temp);
            });
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
