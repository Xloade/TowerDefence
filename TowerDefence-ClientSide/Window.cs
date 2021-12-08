using System;
using System.Drawing;
using System.Windows.Forms;
using TowerDefence_SharedContent;

public abstract class Window : Form
{
    protected Bitmap DrawArea;
    protected Image bgImage;
    protected Timer graphicalTimer { get; private set; }
    private System.ComponentModel.IContainer components = null;

    protected ComboBox towerSelectionBox;
    protected ComboBox soldierSelectionBox;

    private string[] statusNames = new string[] { "Lifepoints", "Tower Currency", "Soldier Currency" };

    protected Label LifePointsText;
    protected Label TowerCurrencyText;
    protected Label SoldierCurrencyText;
    protected ComboBox StatusSelectionBox;

    public Window(params string[] btnNames) : base()
    {
        DrawArea = new Bitmap(600, 400,
             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        bgImage = new Bitmap(1000, 700,
             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        createButtonsLine(btnNames);
        this.Text = "KTU IF 2018";
        InitializeComponent();
    }
    public Window(string bgImagePath, string title,
                     int width, int height,
                    params string[] btnNames) : base()
    {
        this.bgImage = Image.FromFile(SpritePaths.getMap("Summer"));
        DrawArea = new Bitmap(width, height,
             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        createButtonsLine(btnNames);
        createStatusLine();
        this.Text = title;
        InitializeComponent();
    }

    private void createStatusLine()
    {
        int margin = 20;
        int x = margin;
        foreach(var name in statusNames)
        {
            switch(name)
            {
                case "Lifepoints":
                    LifePointsText = new Label();
                    x += margin + SetText(LifePointsText, name, x);
                    break;
                case "Tower Currency":
                    TowerCurrencyText = new Label();
                    x += margin + SetText(TowerCurrencyText, name, x);
                    break;
                case "Soldier Currency":
                    SoldierCurrencyText = new Label();
                    x += margin + SetText(SoldierCurrencyText, name, x);
                    break;
                default:
                    break;
            }
        }

        StatusSelectionBox = new ComboBox();
        StatusSelectionBox.Location = new Point(x, DrawArea.Height - 700);
        StatusSelectionBox.Items.AddRange(new string[] { "All", "Lifepoints", "Tower Currency", "Soldier Currency" });
        StatusSelectionBox.DropDownClosed += new EventHandler(status_selection_click);
        StatusSelectionBox.SelectedIndex = 0;
        this.Controls.Add(StatusSelectionBox);
    }

    private int SetText(Label textBox, string name, int x)
    {
        textBox.Text = name;
        textBox.ForeColor = Color.Black;
        int textWidth = name.Length * 10 + 4;
        textBox.Location = new Point(x, DrawArea.Height - 700);
        textBox.Size = new Size(textWidth, 20);
        this.Controls.Add(textBox);
        return textWidth;
    }

    private void createButtonsLine(params string[] btnNames)
    {
        int margin = 20;
        int btnX = margin;
        foreach (string name in btnNames)
        {
            Button btn = new Button();
            btn.Name = name;
            btn.Text = name;
            int btnWidth = name.Length * 10 + 4;
            btn.Location = new Point(btnX, DrawArea.Height-200);
            btn.Size = new Size(btnWidth, 20);
            btn.Click += new EventHandler(btn_Click);
            this.Controls.Add(btn);
            if(name.Equals("Buy tower"))
            {
                towerSelectionBox = new ComboBox();
                towerSelectionBox.Location = new Point(btnX, DrawArea.Height - 220);
                towerSelectionBox.Items.AddRange(new string[] { "Minigun", "Rocket", "Laser"});
                towerSelectionBox.DropDownClosed += new EventHandler(tower_selection_click);
                this.Controls.Add(towerSelectionBox);
                towerSelectionBox.Visible = false;
            } else if(name.Equals("Buy soldier"))
            {
                soldierSelectionBox = new ComboBox();
                soldierSelectionBox.Location = new Point(btnX, DrawArea.Height - 220);
                soldierSelectionBox.Items.AddRange(new string[] { "Hitpoints", "Speed" });
                soldierSelectionBox.DropDownClosed += new EventHandler(soldier_selection_click);
                this.Controls.Add(soldierSelectionBox);
                soldierSelectionBox.Visible = false;
            }
            btnX += margin + btnWidth;
        }
    }
    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.components = new System.ComponentModel.Container();

        this.graphicalTimer = new System.Windows.Forms.Timer(this.components);
        this.graphicalTimer.Enabled = false;
        this.graphicalTimer.Interval = 16;
        this.graphicalTimer.Tick += new System.EventHandler(this.graphicalTimer_Tick);

        this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
        this.ClientSize = new System.Drawing.Size(
                      DrawArea.Width-198, DrawArea.Height-175);
        this.Load += new System.EventHandler(this.Form1_Load);
        this.Closed += new System.EventHandler(this.Form1_Closed);
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
        this.MouseClick += Mouse_Click;
        this.MouseDown += Mouse_Down;
        this.MouseUp += Mouse_Up;
        this.MouseMove += Mouse_Move;
        this.ResumeLayout(false);
    }
    // form load event
    private  void Form1_Load(object sender, System.EventArgs e)
    {
        this.SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.DoubleBuffer,
            true);
        this.UpdateStyles();
    }
    // free up resources on program exit
    private void Form1_Closed(object sender, System.EventArgs e)
    {
        DrawArea.Dispose();
    }
    // Form paint event
    protected abstract void Form1_Paint(object sender, PaintEventArgs e);
    //{
    //    Graphics gr = e.Graphics;
    //    gr.DrawImage(DrawArea, 0, 0, DrawArea.Width, DrawArea.Height);
    //    gr.Dispose();
    //}

    protected abstract void btn_Click(object sender, System.EventArgs e);

    protected abstract void tower_selection_click(object sender, System.EventArgs e);
    protected abstract void soldier_selection_click(object sender, System.EventArgs e);

    protected abstract void status_selection_click(object sender, System.EventArgs e);
    protected virtual void Mouse_Click(object sender, MouseEventArgs e) { }
    protected virtual void Mouse_Down(object sender, MouseEventArgs e) { }
    protected virtual void Mouse_Up(object sender, MouseEventArgs e) { }
    protected virtual void Mouse_Move(object sender, MouseEventArgs e) { }
    protected virtual void graphicalTimer_Tick(object sender, System.EventArgs e) { }
    protected virtual void physicsTimer_Tick(object sender, System.EventArgs e) { }

    protected void ClearArea()
    {
        Graphics gr = Graphics.FromImage(DrawArea);
        gr.Dispose();
    }
}


