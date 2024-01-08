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
    public struct Variablestorage
    {
        // Public fields for string and int
        public string Text;
        public int Number;

        // Constructor to initialize the struct
        public Variablestorage(string variableName, int variableValue)
        {
            Text = variableName;
            Number = variableValue;
        }

        // Method to return a value tuple with string and int values
        public (string Text, int Number) DisplayValues()
        {
            return (Text, Number);
        }
    }

    /// <summary>
    /// Parses and executes commands for drawing shapes and interacting with a drawing canvas.
    /// </summary>
    public class CommandParser
    {
        private Form1 formInstance;
        private int currentX;
        private int currentY;
        public Color currentColor { get; private set; } // Store the current color
        public List<Variablestorage> variable = new List<Variablestorage>();
        public string commands;

        private bool isRecordingCommands = false;
        private bool isvalidif;
        private List<string> recordedCommands = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParser"/> class.
        /// </summary>
        /// <param name="formInstance">The instance of the main form associated with the parser.</param>

        public CommandParser(Form1 formInstance)
        {
            this.formInstance = formInstance;
            Reset();
            currentColor = Color.Black; // Set initial colour
        }

        /// <summary>
        /// Resets the current coordinates to the top left of the picture box.
        /// </summary>
        public void Reset()
        {
            // set current coordinates to the top left of the picturebox
            currentX = 0;
            currentY = 0;
        }

        /// <summary>
        /// Clears the picture box.
        /// </summary>
        public void ClearPictureBox()
        {
            formInstance.pictureBox1.Image = null;

        }

        public void commandlist()
        {
            commands = formInstance.textBox2.Text;
        }

        private bool ContainsOperator(string condition)
        {
            // List of valid operators
            string[] validOperators = { "=", "<", ">", "<=", ">=" };

            // Check if the given operatorString is in the list of valid operators
            return validOperators.Contains(condition);
        }

        private void ValidateSingleCommandSyntax(string[] commandArray, int expectedLength)
        {
            if (commandArray.Length != expectedLength)
            {
                throw new ArgumentException($"Invalid syntax for '{commandArray[0]}' command.");
            }
        }

        private void ValidateIntParameters(string[] commandArray, params int[] indices)
        {
            foreach (var index in indices)
            {
                if (!int.TryParse(commandArray[index], out _))
                {
                    throw new ArgumentException($"Invalid parameter '{commandArray[index]}' for the command.");
                }
            }
        }

        private void ValidateSetColourSyntax(string[] commandArray)
        {
            if (commandArray.Length != 2 && commandArray.Length != 4)
            {
                throw new ArgumentException("Invalid syntax for 'setcolour' command.");
            }

            if (commandArray.Length == 2)
            {
                // Check preset color
                string presetColor = commandArray[1].ToLower();
                if (presetColor != "red" && presetColor != "blue" && presetColor != "green")
                {
                    throw new ArgumentException("Invalid preset color for 'setcolour' command.");
                }
            }
            else
            {
                // Check RGB values
                ValidateIntParameters(commandArray, 1, 2, 3);
            }
        }

        private void ValidateDrawRectangleSyntax(string[] commandArray)
        {
            if (commandArray.Length != 3 && commandArray.Length != 4
                || !int.TryParse(commandArray[1], out _)
                || !int.TryParse(commandArray[2], out _)
                || (commandArray.Length == 4 && !bool.TryParse(commandArray[3], out _)))
            {
                throw new ArgumentException("Invalid syntax for 'drawrectangle' command.");
            }
        }

        private void ValidateDrawCircleSyntax(string[] commandArray)
        {
            if (commandArray.Length != 2 && commandArray.Length != 3
                || !int.TryParse(commandArray[1], out _)
                || (commandArray.Length == 3 && !bool.TryParse(commandArray[2], out _)))
            {
                throw new ArgumentException("Invalid syntax for 'drawcircle' command.");
            }
        }

        private void ValidateDrawTriangleSyntax(string[] commandArray)
        {
            if (commandArray.Length != 3 && commandArray.Length != 4
                || !int.TryParse(commandArray[1], out _)
                || (commandArray.Length == 4 && !bool.TryParse(commandArray[3], out _)))
            {
                throw new ArgumentException("Invalid syntax for 'drawtriangle' command.");
            }
        }

        private void ValidateVariableSyntax(string[] commandArray)
        {
            if (commandArray.Length != 3 || !int.TryParse(commandArray[2], out _))
            {
                throw new ArgumentException("Invalid syntax for 'var' command.");
            }
        }

        private void ValidateIfSyntax(string[] commandArray)
        {
            if (commandArray.Length != 4 || !ContainsOperator(commandArray[2]))
            {
                throw new ArgumentException("Invalid syntax for 'if' command.");
            }

        }

        /// <summary>
        /// Checks the syntax of the provided command array.
        /// Acts like a gate keeper
        /// </summary>
        /// <param name="commandArray">The array containing the command and its parameters.</param>
        public void CheckSyntax(string[] commandArray)
        {
            if (commandArray.Length == 0)
            {
                throw new ArgumentException("empty command");
            }

            string command = commandArray[0];
            command = command.Replace(" ", "");

            // Add more checks for other valid commands and their required attributes
            switch (command)
            {
                case "clear":
                case "reset":
                    ValidateSingleCommandSyntax(commandArray, 1);
                    break;

                case "moveto":
                case "drawto":
                    ValidateSingleCommandSyntax(commandArray, 3);
                    ValidateIntParameters(commandArray, 1, 2);
                    break;

                case "save":
                case "load":
                    ValidateSingleCommandSyntax(commandArray, 2);
                    break;

                case "setcolour":
                    ValidateSetColourSyntax(commandArray);
                    break;

                // Add more cases for other commands

                case "drawrectangle":
                    ValidateDrawRectangleSyntax(commandArray);
                    break;

                case "drawcircle":
                    ValidateDrawCircleSyntax(commandArray);
                    break;

                case "drawtriangle":
                    ValidateDrawTriangleSyntax(commandArray);
                    break;

                case "var":
                    ValidateVariableSyntax(commandArray);
                    break;

                case "if":
                    ValidateIfSyntax(commandArray);
                    break;

                case "endif":
                    ValidateSingleCommandSyntax(commandArray,1);
                    break;

                default:
                    throw new ArgumentException("Invalid command.");
            }
        }

        /// <summary>
        /// Executes the provided command.
        /// Runs CheckSyntax method that means this method can trust the command array
        /// </summary>
        /// <param name="command">The command to be executed.</param>
        public void ExecuteCommand(string command)
        {
            try
            {
                var g = formInstance.pictureBox1.CreateGraphics();

                string[] commandArray = null;
                string[] lowerCaseCommandArray = null;

                if (command.Contains("(") || command.Contains(")")) 
                {
                    commandArray = command.Split('(', ')');
                } else
                {
                    commandArray = command.Split(' ');
                    lowerCaseCommandArray = commandArray.Select(cmd => cmd.ToLower()).ToArray();
                }

                // Split the command into an array of strings
                // string[] commandArray = command.Split(' ');

                // Convert the commandArray to lowercase for case-insensitive comparison
                lowerCaseCommandArray = commandArray.Select(cmd => cmd.ToLower()).ToArray();

                // Create a list of StringIntPair structs
                // List<Variablestorage> variable = new List<Variablestorage>();

                

                CheckSyntax(lowerCaseCommandArray);

                /*if (isRecordingCommands)
                {
                    // Record the command
                    recordedCommands.Add(command);

                    // Check for "endif" keyword to stop recording
                    if (lowerCaseCommandArray.Length > 0 && lowerCaseCommandArray[0] == "endif")
                    {
                        isRecordingCommands = false;

                        // Execute the recorded commands if the condition is met
                        if (EvaluateCondition())
                        {
                            foreach (var recordedCommand in recordedCommands)
                            {
                                ExecuteSingleCommand(recordedCommand);
                            }
                        }

                        // Clear the recorded commands for the next "if" block
                        recordedCommands.Clear();
                    }
                    else
                    {
                        // Check for "if" keyword to start recording
                        if (lowerCaseCommandArray.Length > 0 && lowerCaseCommandArray[0] == "if")
                        {
                            isRecordingCommands = true;
                        }
                        else
                        {
                            // Execute the command
                            ExecuteSingleCommand(command);
                        }
                    }
                }*/
                

                if (lowerCaseCommandArray.Length > 0)
                {
                    switch (lowerCaseCommandArray[0])
                    {
                        case "clear":
                            // clear command
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
                                ClearPictureBox();
                                SendMessage("Canvas has been cleared.");
                            }
                            break;

                        case "reset":
                            // Reset command
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
                                Reset();
                                SendMessage("Coordinates reset to the center.");
                            }
                            break;

                        case "moveto":
                            // moveto command
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
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
                            }
                            break;

                        case "drawto":
                            // drawto commands
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
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
                            if (commandArray.Length == 2)
                            {
                                string filename = $"{commandArray[1]}.txt";

                                OpenFile(filename);
                                // SaveFile(filename, formInstance.textBox2.Lines);
                            }
                            else
                            {
                                throw new ArgumentException("now drawing saved method");
                            }
                            break;

                        case "setcolour":
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
                                if (lowerCaseCommandArray.Length == 2)
                                {
                                    // Set color based on preset color
                                    SetColorByPreset(lowerCaseCommandArray[1]);
                                    SendMessage($"Color set to {lowerCaseCommandArray[1]}");
                                }
                                else if (lowerCaseCommandArray.Length == 4)
                                {
                                    // Set color based on RGB values
                                    SetColorByRGB(int.Parse(lowerCaseCommandArray[1]), int.Parse(lowerCaseCommandArray[2]), int.Parse(lowerCaseCommandArray[3]));
                                    SendMessage($"Color set to RGB({lowerCaseCommandArray[1]}, {lowerCaseCommandArray[2]}, {lowerCaseCommandArray[3]})");
                                }
                                else
                                {
                                    throw new ArgumentException("Invalid parameters for 'setcolour' command.");
                                }
                            }
                            // setcolour logic

                            break;

                        
                        // Add drawing logic here for other commands
                        case "drawrectangle":
                            // drawing logic for drawrectangle command
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
                            if (lowerCaseCommandArray.Length == 4)
                            {
                                Rectangle rectangle = new Rectangle(currentColor, currentX, currentY, int.Parse(lowerCaseCommandArray[1]), int.Parse(lowerCaseCommandArray[2]), true);
                                rectangle.Draw(g);  // GraphicsObject is assumed to be an instance of Graphics

                            }
                            else
                            {
                                Rectangle rectangle = new Rectangle(currentColor, currentX, currentY, int.Parse(lowerCaseCommandArray[1]), int.Parse(lowerCaseCommandArray[2]), false);
                                rectangle.Draw(g);  // GraphicsObject is assumed to be an instance of Graphics
                            }
                            SendMessage("Rectangle drawn.");
                            }

                            break;

                        case "drawcircle":
                            // draw circle
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
                            if (lowerCaseCommandArray.Length == 3)
                            {
                                Circle circle = new Circle(currentColor, currentX, currentY, int.Parse(lowerCaseCommandArray[1]), true);
                                circle.Draw(g);
                            }
                            else
                            {
                                Circle circle = new Circle(currentColor, currentX, currentY, int.Parse(lowerCaseCommandArray[1]), false);
                                circle.Draw(g);
                            }
                            SendMessage("Circle drawn.");
                            }

                            break;

                        case "drawtriangle":
                            // draw triangle
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
                            if (lowerCaseCommandArray.Length == 3)
                            {
                                Triangle triangle = new Triangle(currentColor, currentX, currentY, int.Parse((lowerCaseCommandArray[1])), true);
                                triangle.Draw(g);
                            }
                            else
                            {
                                Triangle triangle = new Triangle(currentColor, currentX, currentY, int.Parse((lowerCaseCommandArray[1])), false);
                                triangle.Draw(g);
                            }
                            SendMessage("Triangle drawn.");
                            }

                            break;

                        case "var":
                            // declare variable
                            if (isRecordingCommands)
                            {
                                recordedCommands.Add(command);
                            }
                            else
                            {
                            if (lowerCaseCommandArray.Length == 3)
                            {
                                String variableName = lowerCaseCommandArray[1];
                                int variableValue = int.Parse(lowerCaseCommandArray[2]);
                                variable.Add(new Variablestorage(variableName, variableValue));
                            }
                            SendMessage("valid variable");
                            }

                            break;

                        case "if":
                            // declare if statement
                            isRecordingCommands = true;
                            if (lowerCaseCommandArray[2] == "=")
                            {
                                if (variable.Any(item => item.Text == lowerCaseCommandArray[1]))
                                {
                                    if (int.TryParse(commandArray[3], out int number))
                                    {
                                        SendMessage("variable 1 exist and = used and second item is a number");
                                        Variablestorage foundvariable = variable.FirstOrDefault(item => item.Text == commandArray[1]);
                                        if (foundvariable.Number == number)
                                        {
                                            isvalidif= true;
                                            SendMessage("end of if");
                                        }
                                    }
                                    else
                                    {
                                        SendMessage("variable 1 exist and = used and second item is a variable");
                                    }
                                }
                                else
                                {
                                    SendMessage("no variable by that name");
                                }
                                
                            }
                            else if (lowerCaseCommandArray[2] == "<")
                            {
                                SendMessage("< used");
                            }
                            else if (lowerCaseCommandArray[2] == ">")
                            {
                                SendMessage("> used");
                            }
                            else if (lowerCaseCommandArray[2] == "<=")
                            {
                                SendMessage("<= used");
                            }
                            else if (lowerCaseCommandArray[2] == ">=")
                            {
                                SendMessage(">= used");
                            }
                            else
                            {
                                SendMessage("Valid if statement");
                            }

                            break;

                        case "endif":
                            isRecordingCommands = false;
                            if (isvalidif)
                            {
                                foreach (string line in recordedCommands)
                                {
                                    ExecuteCommand(line);
                                }
                            }
                            recordedCommands.Clear();
                            break;

                        default:
                            // check if first string is a variable name
                            if (variable.Any(item => item.Text == lowerCaseCommandArray[0]))
                            {
                                Console.WriteLine(" exists in the list.");
                            }
                            else
                            {
                                throw new ArgumentException("Invalid command.");
                            }
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

        
        /// <summary>
        /// Gets the current X-coordinate.
        /// </summary>
        /// <returns>The current X-coordinate.</returns>
        public int GetCurrentX()
        {
            return currentX;
        }

        /// <summary>
        /// Gets the current Y-coordinate.
        /// </summary>
        /// <returns>The current Y-coordinate.</returns>
        public int GetCurrentY()
        {
            return currentY;
        }

        /// <summary>
        /// Displays a message in the associated form's label.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        public void SendMessage(string message)
        {
            // Display the message in the label with the name 'label1'
            formInstance.label1.Text = message;
        }

        /// <summary>
        /// Sets the current color based on a preset color name.
        /// </summary>
        /// <param name="presetColor">The name of the preset color (e.g., "red", "blue", "green", "black").</param>
        /// <exception cref="ArgumentException">Thrown when an invalid preset color is provided.</exception>
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
                case "black":
                    currentColor = Color.Black;
                    break;
                default:
                    throw new ArgumentException($"Invalid preset color: {presetColor}");
            }
        }

        /// <summary>
        /// Sets the current color based on RGB values.
        /// </summary>
        /// <param name="r">The red component of the color (0-255).</param>
        /// <param name="g">The green component of the color (0-255).</param>
        /// <param name="b">The blue component of the color (0-255).</param>
        /// <exception cref="ArgumentException">Thrown when invalid RGB values are provided.</exception>

        private void SetColorByRGB(int r, int g, int b)
        {
            // Validate RGB values
            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
            {
                throw new ArgumentException("Invalid RGB values.");
            }
            currentColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Opens a file and executes the commands stored in it.
        /// </summary>
        /// <param name="save">The name of the file to be opened.</param>
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
            else
            {
                throw new FileNotFoundException($"File not found: {save}", save);
            }
        }


        /// <summary>
        /// Saves the provided commands to a file with the specified name.
        /// </summary>
        /// <param name="savename">The name of the file to be saved.</param>
        /// <param name="commands">The array of commands to be saved.</param>
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
