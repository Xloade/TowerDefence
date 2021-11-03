using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Commands;

class GameWindow : Window
{
    private const string BUTTON_BUY_SOLDIER = "Buy soldier";
    private const string BUTTON_BUY_TOWER = "Buy tower";
    private const string BUTTON_RESTART_GAME = "Restart game";
    private const string BUTTON_DELETE_TOWER = "Delete tower";
    private const string SERVER_URL = "https://localhost:5001/GameHub";

    List<Shape> shapes = new List<Shape>();
    private List<Soldier> soldiers = new List<Soldier>();

    HubConnection connection;
    PlayerType playerType;

    public GameWindow(PlayerType playerType, String mapType) : base(Color.Cyan, playerType.ToString(),
        1000, 700, BUTTON_BUY_SOLDIER, BUTTON_BUY_TOWER, BUTTON_RESTART_GAME, BUTTON_DELETE_TOWER)
    {
        this.playerType = playerType;
        startSignalR(mapType);
    }

    private void updateMapColor(Color color)
    {
        this.bgColor = color;
    }
    private void updateSoldiers(float rotation)
    {
        shapes = new List<Shape>();

        soldiers.ForEach((soldier) =>
        {
            shapes.Add(new Shape(soldier.Coordinates, 100, 100, rotation, Image.FromFile(soldier.Sprite)));
        });
        Refresh();
    }

    private void updateTowers(List<Tower> towers, float rotation, PlayerType type)
    {
        towers.ForEach((tower) =>
        {
            shapes.Add(new Shape(tower.Coordinates, 100, 100, rotation, Image.FromFile(tower.Sprite)));

            updateBullets(tower.Bullets, type == PlayerType.PLAYER1 ? 90 : -90);
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
        ReceiveSoldierUpdates();

        connection.StartAsync();

        CreatePlayer(playerType);
    }

    private void ReceiveSoldierUpdates()
    {
        connection.On<PlayerType, string>(SoldierCommand.Player1SoldierCoordinatesChanged.ToString(), (playerType, updatedSoldierList) =>
        {
            List<Soldier> updatedSoldiers = JsonConvert.DeserializeObject<List<Soldier>>(updatedSoldierList);
            soldiers = updatedSoldiers;

            updateSoldiers(90);
        });

        connection.On<PlayerType, string>(SoldierCommand.Player2SoldierCoordinatesChanged.ToString(), (playerType, updatedSoldierList) =>
        {
            List<Soldier> updatedSoldiers = JsonConvert.DeserializeObject<List<Soldier>>(updatedSoldierList);
            soldiers = updatedSoldiers;

            updateSoldiers(-90);
        });
    }

    private void CreatePlayer(PlayerType playerType)
    {
        connection.SendAsync("createPlayer", playerType);
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

    private void AddSoldier()
    {
        var soldier = new Soldier(playerType);
        soldier.NotifyServer(connection, "buySoldier");
        soldiers.Add(soldier);
    }

    protected override void btn_Click(object sender, EventArgs e)
    {
        switch (((Button)sender).Name)
        {
            case BUTTON_BUY_SOLDIER:
                AddSoldier();
                break;
            //case BUTTON_BUY_TOWER:
            //    mapNotifier.SendPlayerAction("buyTower", playerType);
            //    //connection.SendAsync("buyTower", playerType);
            //    break;
            //case BUTTON_DELETE_TOWER:
            //    mapNotifier.SendPlayerAction("deleteTower", playerType);
            //    //connection.SendAsync("deleteTower", playerType);
            //    break;
            //case BUTTON_RESTART_GAME:
            //    mapNotifier.SendPlayerAction("restartGame", playerType);
            //    //connection.SendAsync("restartGame");
            //    break;
            default:
                break;
        }
    }
 
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        graphicalTimer.Stop();
        connection.StartAsync();
        base.OnFormClosing(e);
    }
}
