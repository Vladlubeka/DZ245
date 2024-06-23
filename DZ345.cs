using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawCircleApp
{
    public partial class Form1 : Form
    {
        private Point? centerPoint = null;
        private Point? circumferencePoint = null;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Text = "Draw Circle App";
            this.MouseClick += new MouseEventHandler(Form1_MouseClick);
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (centerPoint == null)
            {
                centerPoint = e.Location;
                lblPoints.Text = $"Points: Center ({centerPoint.Value.X}, {centerPoint.Value.Y})";
            }
            else if (circumferencePoint == null)
            {
                circumferencePoint = e.Location;
                float radius = CalculateDistance((Point)centerPoint, (Point)circumferencePoint);
                float area = (float)Math.PI * radius * radius;
                lblPoints.Text += $", Circumference ({circumferencePoint.Value.X}, {circumferencePoint.Value.Y})";
                lblRadius.Text = $"Radius: {radius:F2}";
                lblArea.Text = $"Area: {area:F2}";
                this.Invalidate();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            centerPoint = null;
            circumferencePoint = null;
            lblPoints.Text = "Points:";
            lblRadius.Text = "Radius:";
            lblArea.Text = "Area:";
            this.Invalidate(); 
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (centerPoint != null && circumferencePoint != null)
            {
                float radius = CalculateDistance((Point)centerPoint, (Point)circumferencePoint);
                e.Graphics.DrawEllipse(Pens.Black, centerPoint.Value.X - radius, centerPoint.Value.Y - radius, 2 * radius, 2 * radius);
            }
        }

        private float CalculateDistance(Point p1, Point p2)
        {
            return (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}