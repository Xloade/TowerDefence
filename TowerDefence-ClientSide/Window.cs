using System;
using System.Drawing;
using System.Windows.Forms;

public abstract class Window : Form
{
    protected Bitmap DrawArea;
    protected Color bgColor = Color.Yellow;
    protected Timer graphicalTimer { get; private set; }
    private System.ComponentModel.IContainer components = null;

    public Window(params string[] btnNames) : base()
    {
        DrawArea = new Bitmap(600, 400,
             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        createButtonsLine(btnNames);
        this.Text = "KTU IF 2018";
        InitializeComponent();
    }
    public Window(Color bgColor, string title,
                     int width, int height,
                    params string[] btnNames) : base()
    {
        this.bgColor = bgColor;
        DrawArea = new Bitmap(width, height,
             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        createButtonsLine(btnNames);
        this.Text = title;
        InitializeComponent();
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

    protected virtual void Mouse_Click(object sender, MouseEventArgs e) { }
    protected virtual void graphicalTimer_Tick(object sender, System.EventArgs e) { }
    protected virtual void physicsTimer_Tick(object sender, System.EventArgs e) { }

    protected void ClearArea()
    {
        Graphics gr = Graphics.FromImage(DrawArea);
        gr.Clear(bgColor);
        gr.Dispose();
    }
}


