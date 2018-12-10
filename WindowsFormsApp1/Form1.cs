using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Solution1 _solution1;
        private Pen _pen = new Pen(new SolidBrush(Color.Black));
        private int _initial = 10455;
        private int _count = 0;

        public Form1()
        {
            InitializeComponent();

            _solution1 = new Solution1();
            _solution1.FindSolution();

            for (int i = 1; i <= _initial; i++)
                _solution1.Move();

            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            foreach (var item in _solution1.Items)
            {
                Rectangle rect = new Rectangle(new Point(item.X, item.Y), new Size(1, 1));
                path.AddRectangle(rect);
            }

            e.Graphics.DrawPath(_pen, path);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            _solution1.Move();
            _count++;
            label1.Text = (_initial + _count).ToString();

            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }

    public class Solution1 : BaseSolution
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public override void FindSolution()
        {
            var lines = _stream.ReadStringDocument();
            var patttern = @"position=<(.*), (.*)> velocity=<(.*),(.*)>";

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var groups = Regex.Match(line, patttern).Groups;
                var item = new Item
                {
                    X = int.Parse(groups[1].Value),
                    Y = int.Parse(groups[2].Value),
                    XVelocity = int.Parse(groups[3].Value),
                    YVelocity = int.Parse(groups[4].Value),
                };
                Items.Add(item);
            }
        }

        public void Move()
        {
            foreach (var item in Items)
                item.Move(item);
        }                
    }

    public class Item
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }

        public void Move(Item item)
        {
            X += XVelocity;
            Y += YVelocity;
        }
    }
}
