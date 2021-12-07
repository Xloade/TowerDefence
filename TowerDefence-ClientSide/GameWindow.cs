using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using Microsoft.AspNetCore.SignalR.Client;
using TowerDefence_SharedContent.Towers;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_ClientSide.Prototype;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TowerDefence_ClientSide.Composite;
using TowerDefence_ClientSide.Proxy;

namespace TowerDefence_ClientSide
{
    public class GameWindow : Window, ICursorChange, IPlayerStats, IHaveShapePlatoon
    {
        private const string ButtonBuySoldier = "Buy soldier";
        private const string ButtonBuyTower = "Buy tower";
        private const string ButtonRestartGame = "Restart game";
        private const string ButtonDeleteTower = "Delete tower";
        private const string ButtonUpgradeSoldier = "Upgrade soldier";
        private const string ButtonQuickBuy = "Quick buy two";
        private const string ServerUrl = "https://localhost:5001/GameHub";
        Map currentMap;

        public ShapePlatoon Shapes { get; set; } = new ShapePlatoon(PlatoonType.Root);
        private readonly MapUpdater mapUpdater;

        private readonly PlayerType playerType;
        private PlayerStatsShowStatus PlayerStatsShowStatus = PlayerStatsShowStatus.All;
        private CursorState cursorState = CursorState.Default;
        private readonly GameCursor gameCursor;
        private readonly Command cursorCommand;
        private string towerToBuy = "";
        private System.Windows.Forms.Timer renderTimer = new System.Windows.Forms.Timer();
        private ServerConnection serverConnection;

        string IPlayerStats.LifePointsText{ set => LifePointsText.Text = value; }

        string IPlayerStats.TowerCurrencyText { set => TowerCurrencyText.Text = value; }
        string IPlayerStats.SoldierCurrencyText { set => SoldierCurrencyText.Text = value; }

        PlayerStatsShowStatus IPlayerStats.PlayerStatsShowStatus => PlayerStatsShowStatus;

        public GameWindow(PlayerType playerType, String mapType) : base(mapType, playerType.ToString(),
            1000, 700, ButtonBuySoldier, ButtonBuyTower, ButtonRestartGame, ButtonDeleteTower, ButtonUpgradeSoldier, ButtonQuickBuy)
        {
            mapUpdater = new MapUpdater(this,playerType);
            gameCursor = new GameCursor(this, playerType);
            cursorCommand = new CursorCommand(gameCursor);
            this.playerType = playerType;
            SetupServerConnection(mapType);
            MapParser.CreateInstance();
            renderTimer.Tick += RenderTimer_Tick;
            renderTimer.Interval = 10;
            renderTimer.Start();
        }
        private void SetupServerConnection(string mapType)
        {
            serverConnection = new ServerConnection(ServerUrl);
            serverConnection.GetConnection().On<string>("ReceiveMessage", ReceiveMessage);
            serverConnection.GetConnection().StartAsync();

            serverConnection.SendMessage(new MapMessage("createMap", MessageType.Map, mapType));
            serverConnection.SendMessage(new PlayerMessage("addPlayer", MessageType.Player, playerType));
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            renderTimer.Stop();
            if (currentMap != null)
            {
                mapUpdater.UpdateMap(currentMap, out BgImage, this);
                Refresh();
            }
            renderTimer.Start();
        }

        private void ReceiveMessage(string updatedMapJson)
        {
            MapParser mapParser = MapParser.GetInstance();
            currentMap = mapParser.Parse(updatedMapJson);
        }

        protected override void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawImage(BgImage, 0, 0, DrawArea.Width, DrawArea.Height);
            gr.SmoothingMode = SmoothingMode.HighSpeed;

            foreach (Shape shape in Shapes)
            {
                shape.DecoratedDraw(gr);
            }
            /*Parallel.ForEach(shapes, shape =>
            {
                shape.DecoratedDraw(gr);
            });*/
            
        }

        protected override void Btn_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case ButtonBuySoldier:
                    OpenSoldierSelection();
                    break;
                case ButtonBuyTower:
                    OpenTowerSelection();
                    break;
                case ButtonDeleteTower:
                    serverConnection.SendMessage(new TowerMessage("deleteTower", MessageType.TowerDelete, playerType));
                    break;
                case ButtonUpgradeSoldier:
                    serverConnection.SendMessage(new SoldierMessage("upgradeSoldier", MessageType.SoldierUpgrade, playerType));
                    break;
                case ButtonRestartGame:
                    serverConnection.SendMessage(new PlayerMessage("restartGame", MessageType.RestartGame));
                    break;
                case ButtonQuickBuy:
                    serverConnection.SendMessage(new SoldierMessage("buyTwoSoldier", MessageType.Soldier, playerType, SoldierType.HitpointsSoldier));
                    break;
            }
        }

        private void OpenTowerSelection()
        {
            TowerSelectionBox.Visible = true;
            TowerSelectionBox.DroppedDown = true;
        }

        private void OpenSoldierSelection()
        {
            SoldierSelectionBox.Visible = true;
            SoldierSelectionBox.DroppedDown = true;
        }

        protected override void Mouse_Click(object sender, MouseEventArgs e)
        {
            if (cursorState == CursorState.Modified && e.Button == MouseButtons.Left && CanBuyTower())
            {
                BuyTower(towerToBuy, PointToClient(Cursor.Position));
                cursorCommand.Undo();
                //connection.SendAsync("SendMessage", playerType.ToString(), "explotion", new string[] { e.X.ToString(), e.Y.ToString() });
            } else if(cursorState == CursorState.Modified && e.Button == MouseButtons.Right)
            {
                cursorCommand.Undo();
            }
        }

        private bool CanBuyTower()
        {
            return playerType switch
            {
                PlayerType.Player1 => PointToClient(Cursor.Position).X < 550,
                PlayerType.Player2 => PointToClient(Cursor.Position).X > 550,
                _ => false,
            };
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            GraphicalTimer.Stop();
            serverConnection.GetConnection().StopAsync();
            base.OnFormClosing(e);
        }

        protected override void Tower_selection_click(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                comboBox.Visible = false;
                cursorCommand.Do(GetTowerType(comboBox.SelectedItem.ToString()));
                towerToBuy = comboBox.SelectedItem.ToString();
            }
        }

        private TowerType GetTowerType(string name)
        {
            return name switch
            {
                "Minigun" => TowerType.Minigun,
                "Laser" => TowerType.Laser,
                "Rocket" => TowerType.Rocket,
                _ => TowerType.Empty,
            };
        }

        protected override void Soldier_selection_click(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                BuySoldier(comboBox.SelectedItem.ToString());
                comboBox.Visible = false;
            }
        }

        private void BuyTower(string name, Point coordinates)
        {
            switch (name)
            {
                case "Minigun":
                    serverConnection.SendMessage(new TowerMessage("buyTower", playerType, TowerType.Minigun, coordinates, MessageType.Tower));
                    break;
                case "Laser":
                    serverConnection.SendMessage(new TowerMessage("buyTower", playerType, TowerType.Laser, coordinates, MessageType.Tower));
                    break;
                case "Rocket":
                    serverConnection.SendMessage(new TowerMessage("buyTower", playerType, TowerType.Rocket, coordinates, MessageType.Tower));
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
                    serverConnection.SendMessage(new SoldierMessage("buySoldier", MessageType.Soldier, playerType, SoldierType.HitpointsSoldier));
                    break;
                case "Speed":
                    serverConnection.SendMessage(new SoldierMessage("buySoldier", MessageType.Soldier, playerType, SoldierType.SpeedSoldier));
                    break;
            }
        }

        protected override void Status_selection_click(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                switch (comboBox.SelectedItem.ToString())
                {
                    case "All":
                        PlayerStatsShowStatus = PlayerStatsShowStatus.All;
                        LifePointsText.Visible = true;
                        TowerCurrencyText.Visible = true;
                        SoldierCurrencyText.Visible = true;
                        MyConsole.WriteLineWithCount("Adapter: show all");
                        break;
                    case "Lifepoints":                        
                        PlayerStatsShowStatus = PlayerStatsShowStatus.Lifepoints;
                        LifePointsText.Visible = true;
                        TowerCurrencyText.Visible = false;
                        SoldierCurrencyText.Visible = false;
                        MyConsole.WriteLineWithCount("Adapter: show lifepoints");
                        break;
                    case "Tower Currency":
                        PlayerStatsShowStatus = PlayerStatsShowStatus.TowerCurrency;
                        LifePointsText.Visible = false;
                        TowerCurrencyText.Visible = true;
                        SoldierCurrencyText.Visible = false;
                        MyConsole.WriteLineWithCount("Adapter: show tower currency");
                        break;
                    case "Soldier Currency":                        
                        PlayerStatsShowStatus = PlayerStatsShowStatus.SoldierCurrency;
                        LifePointsText.Visible = false;
                        TowerCurrencyText.Visible = false;
                        SoldierCurrencyText.Visible = true;
                        MyConsole.WriteLineWithCount("Adapter: show soldier currency");
                        break;
                }
            }
        }

        public void OnCursorChanged(Cursor cursor, CursorState cursorState)
        {
            Cursor = cursor;
            this.cursorState = cursorState;
        }
    }
}
