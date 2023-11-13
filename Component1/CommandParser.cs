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
                    if (commandArray.Length != 3 || !int.TryParse(commandArray[1], out _) || !int.TryParse(commandArray[2], out _))
                    {
                        throw new ArgumentException("Invalid syntax for 'moveto' command.");
                    }
                    break;

                case "drawto":
                    if (commandArray.Length != 3 || !int.TryParse(commandArray[1], out _) || !int.TryParse(commandArray[2], out _))
                    {
                        throw new ArgumentException("Invalid syntax for 'drawto' command.");
                    }
                    break;

                case "save":
                    if (commandArray.Length != 2)
                    {
                        throw new ArgumentException("Enter save name");
                    }
                    break;

                case "load":
                    if (commandArray.Length != 2)
                    {
                        throw new ArgumentException("Enter save name");
                    }
                    break;

                case "setcolour":
                    // 'setcolour' command can take either a preset color or RGB values
                    if (commandArray.Length != 2 && commandArray.Length != 4)
                    {
                        throw new ArgumentException("Invalid syntax for 'setcolour' command.");
                    }

                    // Check for preset color
                    if (commandArray.Length == 2)
                    {
                        // Add more preset color checks as needed
                        string presetColor = commandArray[1].ToLower();
                        if (presetColor != "red" && presetColor != "blue" && presetColor != "green")
                        {
                            throw new ArgumentException("Invalid preset color for 'setcolour' command.");
                        }
                    }
                    else // Check for RGB values
                    {
                        if (!int.TryParse(commandArray[1], out _) || !int.TryParse(commandArray[2], out _) || !int.TryParse(commandArray[3], out _))
                        {
                            throw new ArgumentException("Invalid RGB values for 'setcolour' command.");
                        }
                    }
                    break;

                case "drawrectangle":
                    if (commandArray.Length != 3 && commandArray.Length != 4
                        || !int.TryParse(commandArray[1], out _)
                        || !int.TryParse(commandArray[2], out _)
                        || (commandArray.Length == 4 && !bool.TryParse(commandArray[3], out _)))
                    {
                        throw new ArgumentException("Invalid syntax for 'DrawRectangle' command.");
                    }
                    break;

                case "drawcircle":
                    if (commandArray.Length != 2 && commandArray.Length != 3
                        || !int.TryParse(commandArray[1], out _)
                        || (commandArray.Length == 3 && !bool.TryParse(commandArray[2], out _)))
                    {
                        throw new ArgumentException("Invalid syntax for 'DrawCircle' command.");
                    }
                    break;

                case "drawtriangle":
                    if (commandArray.Length != 2 && commandArray.Length != 3
                        || !int.TryParse(commandArray[1], out _)
                        || (commandArray.Length == 3 && !bool.TryParse(commandArray[2], out _)))
                    {
                        throw new ArgumentException("Invalid syntax for 'DrawTriangle' command.");
                    }
                    break;

                // Add more cases for other commands

                default:
                    throw new ArgumentException("Invalid command.");
            }
        }

        public void ExecuteCommand(string command)
        {
            try
            {
                var g = formInstance.pictureBox1.CreateGraphics();

                // Split the command into an array of strings
                string[] commandArray = command.Split(' ');

                // Convert the commandArray to lowercase for case-insensitive comparison
                string[] lowerCaseCommandArray = commandArray.Select(cmd => cmd.ToLower()).ToArray();

                CheckSyntax(lowerCaseCommandArray);

                if (lowerCaseCommandArray.Length > 0)
                {
                    switch (lowerCaseCommandArray[0])
                    {
                        case "clear":
                            // clear command
                            ClearPictureBox();
                            SendMessage("Canvas has been cleared.");
                            break;

                        case "reset":
                            // Reset command
                            Reset();
                            SendMessage("Coordinates reset to the center.");
                            break;

                        case "moveto":
                            // moveto command
                            if (lowerCaseCommandArray.Length == 3 && int.TryParse(lowerCaseCommandArray[1], out int movetoX) && int.TryParse(lowerCaseCommandArray[2], out int movetoY))
                            {
                                currentX = movetoX;
                                currentY = movetoY;
                                SendMessage($"Moved to X: {currentX}, Y: {currentY}");
                            }
                            else
                            {
                                throw new ArgumentException("Invalid parameters for 'moveto' command.");
                            }
                            break;

                        case "drawto":
                            // drawto commands
                            if (lowerCaseCommandArray.Length == 3 && int.TryParse(lowerCaseCommandArray[1], out int drawtoX) && int.TryParse(lowerCaseCommandArray[2], out int drawtoY))
                            {
                                // Create an instance of Pen
                                Pen pen = new Pen(currentColor);

                                // Draw a line from the current position to the specified end point
                                formInstance.pictureBox1.CreateGraphics().DrawLine(pen, currentX, currentY, drawtoX, drawtoY);

                                // Update the current position
                                currentX = drawtoX;
                                currentY = drawtoY;

                                SendMessage($"Line drawn to X: {drawtoX}, Y: {drawtoY}");
                            }
                            else
                            {
                                throw new ArgumentException("Invalid parameters for 'drawto' command.");
                            }
                            break;

                        case "save":
                            // save command
                            if (commandArray.Length == 2)
                            {
                                string filename = $"{commandArray[1]}.txt";

                                SaveFile(filename, formInstance.textBox2.Lines);
                            }
                            else
                            {
                                throw new ArgumentException("Saved multiline box");
                            }
                            break;

                        case "load":
                            // load command
                            break;

                        case "setcolour":
                            // setcolour logic
                            
                            break;

                        
                        // Add drawing logic here for other commands
                        case "drawrectangle":
                            // drawing logic for drawrectangle command

                            break;

                        case "drawcircle":
                            // draw circle
                            break;

                        case "drawtriangle":
                            // draw triangle
                            break;

                        default:
                            throw new ArgumentException("Invalid command.");
                    }
                }
                else
                {
                    throw new ArgumentException("Empty command.");
                }
            }
            catch (Exception ex)
            {
                // Report the exception
                SendMessage($"Error: {ex.Message}");
            }
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
