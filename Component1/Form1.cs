﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Component1
{
    public partial class Form1 : Form
    {
        private CommandParser commandParser;

        public string CurrentCommand {  get; set; }

        public int x;
        public int y;

        
        public Form1()
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            var g = Graphics.FromImage(pictureBox1.Image);


            
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DrawCursor(Graphics g, int X, int Y)
        {
            g.DrawEllipse(Pens.Red, X-5, Y-5, 10, 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        public void ClearPictureBox()
        {
           

        }
    }
}
