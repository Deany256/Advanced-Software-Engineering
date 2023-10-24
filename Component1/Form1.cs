using System;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            var g = Graphics.FromImage(pictureBox1.Image);

            if ("circle".Equals(textBox1.Text))
            {
                g.FillEllipse(Brushes.Aquamarine, 10, 10, 100, 100);
            }
            else if ("square".Equals(textBox1.Text))
            {
                g.FillRectangle(Brushes.Aquamarine, 10, 10, 150, 150);
            }
        }
    }
}
