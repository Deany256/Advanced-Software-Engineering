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
    /// <summary>
    /// The main form for interacting with the drawing canvas and executing commands.
    /// </summary>
    public partial class Form1 : Form
    {
        private CommandParser commandParser;

        /// <summary>
        /// Gets or sets the current command entered by the user.
        /// </summary>
        public string CurrentCommand { get; set; }

        /// <summary>
        /// Gets or sets the X-coordinate for the cursor position.
        /// </summary>
        public int x;

        /// <summary>
        /// Gets or sets the Y-coordinate for the cursor position.
        /// </summary>
        public int y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
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

        /// <summary>
        /// Handles the click event for the first button, executing the entered command.
        /// </summary>
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

        /// <summary>
        /// Handles the Paint event of the form, drawing the cursor on the canvas.
        /// Except it doesn't really work
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw the cursor at the current position within the bounds of the PictureBox
            // int cursorDrawX = Math.Max(0, Math.Min(cursorX, pictureBox1.Width - 1));
            // int cursorDrawY = Math.Max(0, Math.Min(cursorY, pictureBox1.Height - 1));

            // DrawCursor(Graphics.FromImage(pictureBox1.Image), cursorDrawX, cursorDrawY);
        }

        /// <summary>
        /// Draws the cursor on the graphics surface at the specified position.
        /// </summary>
        /// <param name="g">The graphics surface on which to draw the cursor.</param>
        /// <param name="X">The X-coordinate of the cursor position.</param>
        /// <param name="Y">The Y-coordinate of the cursor position.</param>
        private void DrawCursor(Graphics g, int X, int Y)
        {
            g.DrawEllipse(Pens.Red, X - 5, Y - 5, 10, 10);
        }

        /// <summary>
        /// Handles the click event for the second button, checking the syntax of entered commands.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = textBox2.Lines;

                foreach (string line in lines)
                {
                    commandParser.CheckSyntax(line.Split(' '));
                    // commandParser.ExecuteCommand(line);
                }
                commandParser.SendMessage("Syntax is Valid");
            }
            catch (Exception ex)
            {
                // Report the exception
                commandParser.SendMessage($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Clears the picture box.
        /// </summary>
        public void ClearPictureBox()
        {
            pictureBox1.Image = null;

        }
    }
}
