using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Component1
{
    class CommandParser
    {
        private Form1 formInstance;
        private int currentX;
        private int currentY;
        private Color currentColor; // Store the current color

        public CommandParser(Form1 formInstance)
        {
            this.formInstance = formInstance;
            Reset();
            currentColor = Color.Black; // Set initial color
        }

        public void Reset()
        {
            // set current coordinates to the top left of the picturebox
            currentX = 0;
            currentY = 0;
        }

        public void ClearPictureBox()
        {
            formInstance.pictureBox1.Image = null;

        }

        public void CheckSyntax(string[] commandArray)
        {
            if (commandArray.Length == 0)
            {
                throw new ArgumentException("empty command");
            }

            string command = commandArray[0];

            // Add more checks for other valid commands and their required attributes
            switch (command)
            {
                case "clear":
                    if (commandArray.Length != 1)
                    {
                        throw new ArgumentException("Invalid syntax for 'clear' command.");
                    }
                    break;

                case "reset":
                    if (commandArray.Length != 1)
                    {
                        throw new ArgumentException("Invalid syntax for 'reset' command.");
                    }
                    break;

                case "moveto":
                    
                    break;

                case "drawto":
                    
                    break;

                case "save":
                    
                    break;

                case "load":
                    
                    break;

                case "setcolour":
                    
                    break;

                case "drawrectangle":
                    
                    break;

                case "drawcircle":
                    
                    break;

                case "drawtriangle":
                    
                    break;

                // Add more cases for other commands

                default:
                    throw new ArgumentException("Invalid command.");
            }
        }

        public void ExecuteCommand(string command)
        {
            
        }

        public int GetCurrentX()
        {
            return currentX;
        }

        public int GetCurrentY()
        {
            return currentY;
        }

        public void SendMessage(string message)
        {
            // Display the message in the label with the name 'label1'
            formInstance.label1.Text = message;
        }

        private void SetColorByPreset(string presetColor)
        {
            // Add more preset color checks as needed
            switch (presetColor.ToLower())
            {
                case "red":
                    currentColor = Color.Red;
                    break;
                case "blue":
                    currentColor = Color.Blue;
                    break;
                case "green":
                    currentColor = Color.Green;
                    break;
                default:
                    throw new ArgumentException($"Invalid preset color: {presetColor}");
            }
        }

        private void SetColorByRGB(int r, int g, int b)
        {
            // Validate RGB values if needed
            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
            {
                throw new ArgumentException("Invalid RGB values.");
            }
            currentColor = Color.FromArgb(r, g, b);
        }
        // Add methods for saving and loading a program to a text file

        // Add methods for syntax checking (checking for valid commands and parameters)

        // ... rest of the class remains the same ...

        public static void FormatCode() { }

        public void OpenFile(string save) 
        {
            if (File.Exists(save))
            {
                using (StreamReader reader = new StreamReader(save))
                {
                    // Read and display each line
                    while (!reader.EndOfStream)
                    {
                        string command = reader.ReadLine();
                        ExecuteCommand(command);
                    }
                }
            }
        }

        public void SaveFile(string savename, string[] commands) 
        {
            using (StreamWriter writer = new StreamWriter(savename)) 
            {
                // Write each string in the array as a separate line
                foreach (string line in commands)
                {
                    writer.WriteLine(line);
                }
            }  
        }
    }
}
