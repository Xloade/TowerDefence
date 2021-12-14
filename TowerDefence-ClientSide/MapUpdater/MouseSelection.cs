using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Speech.Synthesis.TtsEngine;

namespace TowerDefence_ClientSide
{
    public class MouseSelection
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public MouseSelection()
        {
            Selected = false;
            StartPoint = new Point(0, 0);
            EndPoint = new Point(0, 0);
        }
        public int Top
        {
            get
            {
                return StartPoint.Y > EndPoint.Y ? EndPoint.Y : StartPoint.Y;
            }
        }
        public int Bot
        {
            get
            {
                return StartPoint.Y < EndPoint.Y ? EndPoint.Y : StartPoint.Y;
            }
        }
        public int Left
        {
            get
            {
                return StartPoint.X > EndPoint.X ? EndPoint.X : StartPoint.X;
            }
        }
        public int Right
        {
            get
            {
                return StartPoint.X < EndPoint.X ? EndPoint.X : StartPoint.X;
            }
        }

        public bool Selected { get; set; }
    }
}
