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
        private CommandParser commandParser;

        public string CurrentCommand { get; set; }

        public int x;
        public int y;


        public Form1()
        {
            InitializeComponent();
            commandParser = new CommandParser(this);
            label1.Text = "Confirmation of Actions or Errors will appear here";

            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            // Subscribe to the Paint event
            this.Paint += Form1_Paint;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            var g = Graphics.FromImage(pictureBox1.Image);


            if (textBox2.Text == "" && textBox1.Text == "")
            {
                commandParser.SendMessage("No commands detected.");
            }
            else if (textBox1.Text != "" && textBox2.Text != "")
            {
                string[] commandArray = textBox1.Text.Split(' ');
                if (commandArray[0] == "save")
                {
                    commandParser.ExecuteCommand(textBox1.Text);
                }
            }
            else if (textBox1.Text != "")
            {
                string command = textBox1.Text;
                commandParser.ExecuteCommand(command);

                // Clear the TextBox
                textBox1.Clear();
            }
            else if (textBox2.Text != "")
            {
                string[] lines = textBox2.Lines;

                foreach (string line in lines)
                {
                    commandParser.ExecuteCommand(line);
                }

                // Clear the TextBox
                textBox2.Clear();
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw the cursor at the current position within the bounds of the PictureBox
            // int cursorDrawX = Math.Max(0, Math.Min(cursorX, pictureBox1.Width - 1));
            // int cursorDrawY = Math.Max(0, Math.Min(cursorY, pictureBox1.Height - 1));

            // DrawCursor(Graphics.FromImage(pictureBox1.Image), cursorDrawX, cursorDrawY);
        }

        private void DrawCursor(Graphics g, int X, int Y)
        {
            g.DrawEllipse(Pens.Red, X - 5, Y - 5, 10, 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        public void ClearPictureBox()
        {


        }
    }
}
