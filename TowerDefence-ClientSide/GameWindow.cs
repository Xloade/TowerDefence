using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TowerDefence_SharedContent;

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

    public GameWindow(PlayerType playerType, String mapType) : base(Color.Cyan, playerType.ToString(),
        1000, 700, BUTTON_BUY_SOLDIER, BUTTON_BUY_TOWER, BUTTON_RESTART_GAME, BUTTON_DELETE_TOWER, BUTTON_UPGRADE_SOLDIER)
    {
        this.playerType = playerType;
        startSignalR(mapType);
    }
    
    private void updateMap(Map map)
    {
        shapes = new List<Shape>();

        updateMapColor(map.mapColor);

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
    private void updateMapColor(Color color)
    {
        this.bgColor = color;
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

            updateBullets(tower.Bullets, GetRotation(playerType));
        });
    }

    private void updateBullets(List<Bullet> bullets, float rotation)
    {
        bullets.ForEach((bullet) =>
        {
            shapes.Add(new Shape(bullet.Coordinates, 50, 50, rotation, Image.FromFile(bullet.Sprite)));
        });    
    }

    private void startSignalR(String mapType)
    {
        connection = new HubConnectionBuilder().WithUrl(SERVER_URL).Build();
        connection.On<string>("ReceiveMessage", (updatedMapJson) =>
        {
            Map updatedMap = JsonConvert.DeserializeObject<Map>(updatedMapJson);
            updateMap(updatedMap);
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
        gr.FillRectangle(new SolidBrush(bgColor), 0, 0, DrawArea.Width, DrawArea.Height);
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
                connection.SendAsync("buySoldier", playerType);
                break;
            case BUTTON_BUY_TOWER:
                connection.SendAsync("buyTower", playerType);
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
}
