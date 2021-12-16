
namespace TowerDefence_ClientSide.Composite
{
    partial class PlatoonControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.deselectOneButton = new System.Windows.Forms.ToolStripButton();
            this.selectAllButton = new System.Windows.Forms.ToolStripButton();
            this.DeselectAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.selectPlatoon1 = new System.Windows.Forms.ToolStripButton();
            this.addToOneButton = new System.Windows.Forms.ToolStripButton();
            this.removeFromOneButton = new System.Windows.Forms.ToolStripButton();
            this.selectPlatoon2 = new System.Windows.Forms.ToolStripButton();
            this.addToTwoButton = new System.Windows.Forms.ToolStripButton();
            this.removeFromTwoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.upgradeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.deselectOneButton,
            this.selectAllButton,
            this.DeselectAllButton,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripButton1,
            this.selectPlatoon1,
            this.addToOneButton,
            this.removeFromOneButton,
            this.selectPlatoon2,
            this.addToTwoButton,
            this.removeFromTwoButton,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.upgradeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(800, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 24);
            this.toolStripLabel1.Text = "selection";
            // 
            // deselectOneButton
            // 
            this.deselectOneButton.AccessibleDescription = "";
            this.deselectOneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deselectOneButton.Image = global::TowerDefence_ClientSide.Properties.Resources.remove;
            this.deselectOneButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deselectOneButton.Name = "deselectOneButton";
            this.deselectOneButton.Size = new System.Drawing.Size(29, 24);
            this.deselectOneButton.Text = "toolStripButton2";
            this.deselectOneButton.ToolTipText = "remove one selection layer";
            this.deselectOneButton.Click += new System.EventHandler(this.deselectOneButton_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectAllButton.Image = global::TowerDefence_ClientSide.Properties.Resources.add_all;
            this.selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(29, 24);
            this.selectAllButton.Text = "toolStripButton3";
            this.selectAllButton.ToolTipText = "select all";
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // DeselectAllButton
            // 
            this.DeselectAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeselectAllButton.Image = global::TowerDefence_ClientSide.Properties.Resources.remove_all;
            this.DeselectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeselectAllButton.Name = "DeselectAllButton";
            this.DeselectAllButton.Size = new System.Drawing.Size(29, 24);
            this.DeselectAllButton.Text = "toolStripButton4";
            this.DeselectAllButton.ToolTipText = "remove all selection layers";
            this.DeselectAllButton.Click += new System.EventHandler(this.DeselectAllButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(67, 24);
            this.toolStripLabel2.Text = "platoons";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::TowerDefence_ClientSide.Properties.Resources._d3;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // selectPlatoon1
            // 
            this.selectPlatoon1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectPlatoon1.Image = global::TowerDefence_ClientSide.Properties.Resources._1;
            this.selectPlatoon1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectPlatoon1.Name = "selectPlatoon1";
            this.selectPlatoon1.Size = new System.Drawing.Size(29, 24);
            this.selectPlatoon1.Text = "Select platoon 1";
            this.selectPlatoon1.ToolTipText = "Select platoon 1";
            this.selectPlatoon1.Click += new System.EventHandler(this.selectPlatoon1_Click);
            // 
            // addToOneButton
            // 
            this.addToOneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addToOneButton.Image = global::TowerDefence_ClientSide.Properties.Resources.outline_exposure_plus_1_black_24dp;
            this.addToOneButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToOneButton.Name = "addToOneButton";
            this.addToOneButton.Size = new System.Drawing.Size(29, 24);
            this.addToOneButton.Text = "toolStripButton5";
            this.addToOneButton.ToolTipText = "add to platoon1";
            this.addToOneButton.Click += new System.EventHandler(this.addToOneButton_Click);
            // 
            // removeFromOneButton
            // 
            this.removeFromOneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeFromOneButton.Image = global::TowerDefence_ClientSide.Properties.Resources.outline_exposure_neg_1_black_24dp;
            this.removeFromOneButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeFromOneButton.Name = "removeFromOneButton";
            this.removeFromOneButton.Size = new System.Drawing.Size(29, 24);
            this.removeFromOneButton.Text = "toolStripButton6";
            this.removeFromOneButton.ToolTipText = "remove from platoon1";
            this.removeFromOneButton.Click += new System.EventHandler(this.removeFromOneButton_Click);
            // 
            // selectPlatoon2
            // 
            this.selectPlatoon2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectPlatoon2.Image = global::TowerDefence_ClientSide.Properties.Resources._2;
            this.selectPlatoon2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectPlatoon2.Name = "selectPlatoon2";
            this.selectPlatoon2.Size = new System.Drawing.Size(29, 24);
            this.selectPlatoon2.Text = "Select platoon 2";
            this.selectPlatoon2.ToolTipText = "Select platoon 2";
            this.selectPlatoon2.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // addToTwoButton
            // 
            this.addToTwoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addToTwoButton.Image = global::TowerDefence_ClientSide.Properties.Resources.outline_exposure_plus_2_black_24dp;
            this.addToTwoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToTwoButton.Name = "addToTwoButton";
            this.addToTwoButton.Size = new System.Drawing.Size(29, 24);
            this.addToTwoButton.Text = "toolStripButton8";
            this.addToTwoButton.ToolTipText = "add to platoon2";
            this.addToTwoButton.Click += new System.EventHandler(this.addToTwoButton_Click);
            // 
            // removeFromTwoButton
            // 
            this.removeFromTwoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeFromTwoButton.Image = global::TowerDefence_ClientSide.Properties.Resources.outline_exposure_neg_2_black_24dp;
            this.removeFromTwoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeFromTwoButton.Name = "removeFromTwoButton";
            this.removeFromTwoButton.Size = new System.Drawing.Size(29, 24);
            this.removeFromTwoButton.Text = "toolStripButton9";
            this.removeFromTwoButton.ToolTipText = "remove from platoon2";
            this.removeFromTwoButton.Click += new System.EventHandler(this.removeFromTwoButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(56, 24);
            this.toolStripLabel3.Text = "actions";
            // 
            // upgradeButton
            // 
            this.upgradeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.upgradeButton.Image = global::TowerDefence_ClientSide.Properties.Resources.outline_upgrade_black_24dp;
            this.upgradeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.upgradeButton.Name = "upgradeButton";
            this.upgradeButton.Size = new System.Drawing.Size(29, 24);
            this.upgradeButton.Text = "toolStripButton7";
            this.upgradeButton.ToolTipText = "upgrade selected";
            this.upgradeButton.Click += new System.EventHandler(this.upgradeButton_Click);
            // 
            // PlatoonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Name = "PlatoonControl";
            this.Size = new System.Drawing.Size(800, 27);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton deselectOneButton;
        private System.Windows.Forms.ToolStripButton selectAllButton;
        private System.Windows.Forms.ToolStripButton DeselectAllButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton addToOneButton;
        private System.Windows.Forms.ToolStripButton removeFromOneButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton upgradeButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton addToTwoButton;
        private System.Windows.Forms.ToolStripButton removeFromTwoButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton selectPlatoon1;
        private System.Windows.Forms.ToolStripButton selectPlatoon2;
    }
}
