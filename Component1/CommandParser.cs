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
            
        }

        private void SetColorByRGB(int r, int g, int b)
        {
            
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
