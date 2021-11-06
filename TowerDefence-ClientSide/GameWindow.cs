using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TowerDefence_SharedContent.Towers;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using Newtonsoft.Json.Linq;
using TowerDefence_ClientSide;

class GameWindow : Window
{
    private const string BUTTON_BUY_SOLDIER = "Buy soldier";
    private const string BUTTON_BUY_TOWER = "Buy tower";
    private const string BUTTON_RESTART_GAME = "Restart game";
    private const string BUTTON_DELETE_TOWER = "Delete tower";
    private const string BUTTON_UPGRADE_SOLDIER = "Upgrade soldier";
    private const string SERVER_URL = "https://localhost:5001/GameHub";

    List<Shape> shapes = new List<Shape>();

    HubConnection connection;
    private PlayerType playerType;
    private Stats stats;

    public GameWindow(PlayerType playerType, String mapType) : base(mapType, playerType.ToString(),
        1000, 700, BUTTON_BUY_SOLDIER, BUTTON_BUY_TOWER, BUTTON_RESTART_GAME, BUTTON_DELETE_TOWER, BUTTON_UPGRADE_SOLDIER)
    {
        this.playerType = playerType;
        startSignalR(mapType);
        MapParser.CreateInstance();
    }
    
    private void updateMap(Map map) 
    {
        this.stats = new PlayerStats(map.GetPlayer(playerType));
        shapes = new List<Shape>();

        updateMapColor(map.backgroundImageDir);

        foreach(Player player in map.players)
        {
            updateSoldiers(player.soldiers, GetRotation(player.PlayerType));
            updateTowers(player.towers, player.PlayerType);
        }
   
        Refresh();
    }   
    
    private int GetRotation(PlayerType playerType)
    {
        return playerType == PlayerType.PLAYER1 ? 90 : -90;
    }

    //rotation temporary
    private void updateMapColor(string image)
    {

        this.bgImage = Image.FromFile(image);
    }
    private void updateSoldiers(List<Soldier> soldiers, float rotation)
    {
        soldiers.ForEach((soldier) =>
        {
            shapes.Add(new Shape(soldier.Coordinates, 100, 100, rotation, Image.FromFile(soldier.Sprite)));
        });
    }

    private void updateTowers(List<Tower> towers, PlayerType playerType)
    {
        towers.ForEach((tower) =>
        {
            shapes.Add(new Shape(tower.Coordinates, 100, 100, GetRotation(playerType), Image.FromFile(tower.Sprite)));

            updateAmmunition(tower.Ammunition, GetRotation(playerType));
        });
    }

    private void updateAmmunition(List<ShootAlgorithm> ammunition, float rotation)
    {
        ammunition.ForEach((amm) =>
        {
            shapes.Add(new Shape(amm.Coordinates, amm.Width, amm.Height, rotation, Image.FromFile(amm.Sprite)));
        });    
    }

    private void startSignalR(String mapType)
    {
        connection = new HubConnectionBuilder().WithUrl(SERVER_URL).Build();
        connection.On<string>("ReceiveMessage", (updatedMapJson) =>
        {
            MapParser mapParser = MapParser.getInstance();
            updateMap(mapParser.Parse(updatedMapJson));
        });
        connection.StartAsync();
        connection.SendAsync("createMap", mapType);
        connection.SendAsync("addPlayer", playerType);
    }

    protected override void graphicalTimer_Tick(object sender, EventArgs e)
    {
        Refresh();
    }
    protected override void Form1_Paint(object sender, PaintEventArgs e)
    {
        Graphics gr = e.Graphics;
        gr.DrawImage(bgImage, 0, 0, DrawArea.Width, DrawArea.Height);
        gr.SmoothingMode = SmoothingMode.AntiAlias;
        foreach (Shape shape in shapes)
        {
            shape.Draw(gr);
        }
    }

    protected override void btn_Click(object sender, EventArgs e)
    {
        switch (((Button)sender).Name)
        {
            case BUTTON_BUY_SOLDIER:
                OpenSoldierSelection();
                break;
            case BUTTON_BUY_TOWER:
                OpenTowerSelection();
                break;
            case BUTTON_DELETE_TOWER:
                connection.SendAsync("deleteTower", playerType);
                break;
            case BUTTON_UPGRADE_SOLDIER:
                connection.SendAsync("upgradeSoldier", playerType);
                break;
            case BUTTON_RESTART_GAME:
                connection.SendAsync("restartGame");
                break;
            default:
                break;
        }
    }

    private void OpenTowerSelection()
    {
        towerSelectionBox.Visible = true;
        towerSelectionBox.DroppedDown = true;
    }

    private void OpenSoldierSelection()
    {
        soldierSelectionBox.Visible = true;
        soldierSelectionBox.DroppedDown = true;
    }

    protected override void Mouse_Click(object sender, MouseEventArgs e)
    {
        connection.SendAsync("SendMessage", playerType.ToString(), "explotion", new string[] { e.X.ToString(), e.Y.ToString() });
    }
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        graphicalTimer.Stop();
        connection.StopAsync();
        base.OnFormClosing(e);
    }

    protected override void tower_selection_click(object sender, EventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        if(comboBox.SelectedItem != null)
        {
            BuyTower(comboBox.SelectedItem.ToString());
            comboBox.Visible = false;
        }    
    }

    protected override void soldier_selection_click(object sender, EventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        if (comboBox.SelectedItem != null)
        {
            BuySoldier(comboBox.SelectedItem.ToString());
            comboBox.Visible = false;
        }
    }

    private void BuyTower(string name)
    {
        switch(name)
        {
            case "Minigun":
                connection.SendAsync("buyTower", playerType, TowerType.Minigun);
                break;
            case "Laser":
                connection.SendAsync("buyTower", playerType, TowerType.Laser);
                break;
            case "Rocket":
                connection.SendAsync("buyTower", playerType, TowerType.Rocket);
                break;
            default:
                break;
        }
    }

    private void BuySoldier(string name)
    {
        switch (name)
        {
            case "Hitpoints":
                connection.SendAsync("buySoldier", playerType, SoldierType.Hitpoints);
                break;
            case "Speed":
                connection.SendAsync("buySoldier", playerType, SoldierType.Speed);
                break;
            default:
                break;
        }
    }

    protected override void status_selection_click(object sender, EventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        if(comboBox.SelectedItem != null)
        {
            switch(comboBox.SelectedItem.ToString())
            {
                case "All":
                    int[] playerStats = stats.Show();

                    LifePointsText.Text = $"Lifepoints: {playerStats[0]}";
                    LifePointsText.Visible = true;

                    TowerCurrencyText.Text = $"Tower Currency: {playerStats[1]}";
                    TowerCurrencyText.Visible = true;

                    SoldierCurrencyText.Text = $"Soldier Currency: {playerStats[2]}";                                      
                    SoldierCurrencyText.Visible = true;
                    break;
                case "Lifepoints":
                    int lifepoints = stats.ShowParameter(PlayerStatsShowStatus.Lifepoints);

                    LifePointsText.Text = $"Lifepoints: {lifepoints}";
                    LifePointsText.Visible = true;

                    TowerCurrencyText.Visible = false;
                    SoldierCurrencyText.Visible = false;
                    break;
                case "Tower Currency":
                    int towerCurrency = stats.ShowParameter(PlayerStatsShowStatus.TowerCurrency);

                    TowerCurrencyText.Text = $"Tower Currency: {towerCurrency}";

                    LifePointsText.Visible = false;
                    TowerCurrencyText.Visible = true;
                    SoldierCurrencyText.Visible = false;
                    break;
                case "Soldier Currency":
                    int soldierCurrency = stats.ShowParameter(PlayerStatsShowStatus.SoldierCurrency);

                    SoldierCurrencyText.Text = $"Soldier Currency: {soldierCurrency}";

                    LifePointsText.Visible = false;
                    TowerCurrencyText.Visible = false;
                    SoldierCurrencyText.Visible = true;
                    break;
            }
        }
    }
}
