using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using TowerDefence_ClientSide.Proxy;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Composite
{
    public partial class PlatoonControl : UserControl
    {
        private readonly ServerConnection ServerConnection;
        private readonly MapUpdater MapUpdater;
        private readonly PlayerType PlayerStats;
        public PlatoonControl(ServerConnection serverConnection, MapUpdater mapUpdater, PlayerType playerType)
        {
            InitializeComponent();
            ServerConnection = serverConnection;
            MapUpdater = mapUpdater;
            PlayerStats = playerType;

            this.Location = new Point(0, 470);
        }

        private void deselectOneButton_Click(object sender, EventArgs e)
        {
            MapUpdater.RemoveOneSelection();
        }

        private void DeselectAllButton_Click(object sender, EventArgs e)
        {
            MapUpdater.RemoveAllSelection();
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            MapUpdater.SelectAll();
        }

        private void addToOneButton_Click(object sender, EventArgs e)
        {
            MapUpdater.TransferSelectToPlatoon(PlatoonType.Platoon1);
        }

        private void removeFromOneButton_Click(object sender, EventArgs e)
        {
            MapUpdater.TransferFromPlatoonToPlatoon(PlatoonType.Platoon1, PlatoonType.DefaultPlatoon);
        }

        private void addToTwoButton_Click(object sender, EventArgs e)
        {
            MapUpdater.TransferSelectToPlatoon(PlatoonType.Platoon2);
        }

        private void removeFromTwoButton_Click(object sender, EventArgs e)
        {
            MapUpdater.TransferFromPlatoonToPlatoon(PlatoonType.Platoon2, PlatoonType.DefaultPlatoon);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MapUpdater.TransferSelectToPlatoon(PlatoonType.DefaultPlatoon);
        }

        private void upgradeButton_Click(object sender, EventArgs e)
        {
            var objects = MapUpdater.GetSelectedShapes().Select(x => x.Info).OfType<IdableObject>().ToList();
            ServerConnection.SendMessage(new UpgradeMessage("Upgrade", MessageType.Upgrade, PlayerStats, objects));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MapUpdater.SelectPlatoon(PlatoonType.Platoon2);
        }

        private void selectPlatoon1_Click(object sender, EventArgs e)
        {
            MapUpdater.SelectPlatoon(PlatoonType.Platoon1);
        }
    }
}
