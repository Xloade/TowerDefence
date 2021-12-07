using System;
using System.Drawing;
using System.Windows.Forms;
using TowerDefence_SharedContent;

public abstract class Window : Form
{
    protected Bitmap DrawArea;
    protected Image BgImage;

    protected Timer GraphicalTimer { get; private set; }
    private System.ComponentModel.IContainer components = null;

    protected ComboBox TowerSelectionBox;
    protected ComboBox SoldierSelectionBox;

    private readonly string[] statusNames = new string[] { "Lifepoints", "Tower Currency", "Soldier Currency" };

    protected Label LifePointsText;
    protected Label TowerCurrencyText;
    protected Label SoldierCurrencyText;
    protected ComboBox StatusSelectionBox;

    protected Window(params string[] btnNames) : base()
    {
        DrawArea = new Bitmap(600, 400,
             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        BgImage = new Bitmap(1000, 700,
             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        CreateButtonsLine(btnNames);
        Text = "KTU IF 2021";
        InitializeComponent();
    }

    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    protected Window(string bgImagePath, string title,
                     int width, int height,
                    params string[] btnNames) : base()
    {
        this.BgImage = Image.FromFile(SpritePaths.GetMap("Summer"));
        DrawArea = new Bitmap(width, height,
             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        CreateButtonsLine(btnNames);
        CreateStatusLine();
        this.Text = title;
        InitializeComponent();
    }

    private void CreateStatusLine()
    {
        var margin = 20;
        var x = margin;
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

        StatusSelectionBox = new ComboBox
        {
            Location = new Point(x, DrawArea.Height - 700)
        };
        StatusSelectionBox.Items.AddRange(new string[] { "All", "Lifepoints", "Tower Currency", "Soldier Currency" });
        StatusSelectionBox.DropDownClosed += new EventHandler(Status_selection_click);
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

    private void CreateButtonsLine(params string[] btnNames)
    {
        int margin = 20;
        int btnX = margin;
        foreach (string name in btnNames)
        {
            Button btn = new Button
            {
                Name = name,
                Text = name
            };
            int btnWidth = name.Length * 10 + 4;
            btn.Location = new Point(btnX, DrawArea.Height-200);
            btn.Size = new Size(btnWidth, 20);
            btn.Click += new EventHandler(Btn_Click);
            this.Controls.Add(btn);
            if(name.Equals("Buy tower"))
            {
                TowerSelectionBox = new ComboBox
                {
                    Location = new Point(btnX, DrawArea.Height - 220)
                };
                TowerSelectionBox.Items.AddRange(new string[] { "Minigun", "Rocket", "Laser"});
                TowerSelectionBox.DropDownClosed += new EventHandler(Tower_selection_click);
                this.Controls.Add(TowerSelectionBox);
                TowerSelectionBox.Visible = false;
            } else if(name.Equals("Buy soldier"))
            {
                SoldierSelectionBox = new ComboBox
                {
                    Location = new Point(btnX, DrawArea.Height - 220)
                };
                SoldierSelectionBox.Items.AddRange(new string[] { "Hitpoints", "Speed" });
                SoldierSelectionBox.DropDownClosed += new EventHandler(Soldier_selection_click);
                this.Controls.Add(SoldierSelectionBox);
                SoldierSelectionBox.Visible = false;
            }
            btnX += margin + btnWidth;
        }
    }
    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.components = new System.ComponentModel.Container();

        this.GraphicalTimer = new System.Windows.Forms.Timer(this.components)
        {
            Enabled = false,
            Interval = 16
        };
        this.GraphicalTimer.Tick += new System.EventHandler(this.GraphicalTimer_Tick);

        this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
        this.ClientSize = new System.Drawing.Size(
                      DrawArea.Width-198, DrawArea.Height-175);
        this.Load += new System.EventHandler(this.Form1_Load);
        this.Closed += new System.EventHandler(this.Form1_Closed);
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
        this.MouseClick += Mouse_Click;
        
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

    protected abstract void Btn_Click(object sender, System.EventArgs e);

    protected abstract void Tower_selection_click(object sender, System.EventArgs e);
    protected abstract void Soldier_selection_click(object sender, System.EventArgs e);

    protected abstract void Status_selection_click(object sender, System.EventArgs e);
    protected virtual void Mouse_Click(object sender, MouseEventArgs e) { }
    protected virtual void GraphicalTimer_Tick(object sender, System.EventArgs e) { }
    protected virtual void PhysicsTimer_Tick(object sender, System.EventArgs e) { }

    protected void ClearArea()
    {
        Graphics gr = Graphics.FromImage(DrawArea);
        gr.Dispose();
    }
}


