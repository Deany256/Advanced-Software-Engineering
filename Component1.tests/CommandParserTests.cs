using System;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Component1.tests
{
    /// <summary>
    /// Test class for the CommandParser class, which parses and executes commands in a graphical form.
    /// </summary>
    [TestClass]
    public class CommandParserTests
    {
        /// <summary>
        /// Tests the CheckSyntax method with a valid command, expecting no exceptions to be thrown.
        /// </summary>
        [TestMethod]
        public void CheckSyntax_ValidCommand_NoExceptionThrown()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] validCommand = { "moveto", "10", "20" };

            // Act and Assert
            Assert.IsFalse(ThrowsException<ArgumentException>(() => commandParser.CheckSyntax(validCommand)));
        }

        /// <summary>
        /// Tests the CheckSyntax method with an invalid command, expecting an ArgumentException to be thrown.
        /// </summary>
        [TestMethod]
        public void CheckSyntax_InvalidCommand_ThrowsException()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] invalidCommand = { "invalidcommand" };

            // Act and Assert
            Assert.IsTrue(ThrowsException<ArgumentException>(() => commandParser.CheckSyntax(invalidCommand)));
        }

        /// <summary>
        /// Tests the CheckSyntax method with a valid "moveto" command, expecting no exceptions to be thrown.
        /// </summary>
        [TestMethod]
        public void CheckSyntax_ValidMovetoCommand_NoExceptionThrown()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] validCommand = { "moveto", "10", "20" };

            // Act and Assert
            Assert.IsFalse(ThrowsException<ArgumentException>(() => commandParser.CheckSyntax(validCommand)));
        }

        /// <summary>
        /// Tests the CheckSyntax method with an invalid "moveto" command, expecting an ArgumentException to be thrown.
        /// </summary>
        [TestMethod]
        public void CheckSyntax_InvalidMovetoCommand_ThrowsException()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] invalidCommand = { "moveto", "10", "20", "30" }; // Extra parameter

            // Act and Assert
            Assert.IsTrue(ThrowsException<ArgumentException>(() => commandParser.CheckSyntax(invalidCommand)));
        }

        /// <summary>
        /// Tests the CheckSyntax method with a valid "drawcircle" command, expecting no exceptions to be thrown.
        /// </summary>
        [TestMethod]
        public void CheckSyntax_ValidDrawCircleCommand_NoExceptionThrown()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] validCommand = { "drawcircle", "15", "true" }; // Valid drawcircle command

            // Act and Assert
            Assert.IsFalse(ThrowsException<ArgumentException>(() => commandParser.CheckSyntax(validCommand)));
        }

        /// <summary>
        /// Tests the CheckSyntax method with an invalid "drawcircle" command, expecting an ArgumentException to be thrown.
        /// </summary>
        [TestMethod]
        public void CheckSyntax_InvalidDrawCircleCommand_ThrowsException()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] invalidCommand = { "drawcircle", "invalidRadius", "true" }; // Invalid radius parameter

            // Act and Assert
            Assert.IsTrue(ThrowsException<ArgumentException>(() => commandParser.CheckSyntax(invalidCommand)));
        }





        /// <summary>
        /// Tests the ExecuteCommand method with a valid command, expecting no exceptions to be thrown.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_ValidCommand_NoExceptionThrown()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string validCommand = "moveto 10 20";

            // Act and Assert
            Assert.IsFalse(ThrowsException<ArgumentException>(() => commandParser.ExecuteCommand(validCommand)));
        }

        /// <summary>
        /// Tests the ExecuteCommand method with an invalid command, expecting an error message to be set in the form's label.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_InvalidCommand_SendsErrorMessage()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string invalidCommand = "invalidcommand";

            // Act
            commandParser.ExecuteCommand(invalidCommand);

            // Assert
            Assert.AreEqual("Error: Invalid command.", form.label1.Text);
        }

        /// <summary>
        /// Tests the ExecuteCommand method with a valid "moveto" command, expecting the coordinates to be updated.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_ValidMovetoCommand_UpdatesCoordinates()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string validCommand = "moveto 10 20";

            // Act
            commandParser.ExecuteCommand(validCommand);

            // Assert
            Assert.AreEqual(10, commandParser.GetCurrentX());
            Assert.AreEqual(20, commandParser.GetCurrentY());
        }

        /// <summary>
        /// Tests the ExecuteCommand method with a valid "setcolour" command, expecting the color to be set.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_ValidSetColourCommand_SetsColor()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string validCommand = "setcolour red";

            // Act
            commandParser.ExecuteCommand(validCommand);

            // Assert
            Assert.AreEqual(Color.Red, commandParser.currentColor);
        }

        /// <summary>
        /// Tests the ExecuteCommand method with a valid "drawcircle" command, expecting no exceptions to be thrown.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_ValidDrawCircleCommand_DoesNotThrowException()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string validCommand = "drawcircle 15 true"; // Valid drawcircle command

            // Act and Assert
            Assert.IsFalse(ThrowsException<ArgumentException>(() => commandParser.ExecuteCommand(validCommand)));
        }

        /// <summary>
        /// Tests the ExecuteCommand method with a valid "drawrectangle" command, expecting a rectangle to be drawn.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_ValidDrawRectangleCommand_DrawsRectangle()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string validCommand = "drawrectangle 20 30 false"; // Valid drawrectangle command

            // Act
            commandParser.ExecuteCommand(validCommand);

            // Assert
            // Add assertions based on the expected outcome of the command
        }


        /// <summary>
        /// test file for testing
        /// </summary>
        private const string TestFileName = "TestFile.txt";

        /// <summary>
        /// Tests the SaveFile method with a valid file name and commands, expecting the file to be created.
        /// </summary>
        [TestMethod]
        public void SaveFile_ValidFileNameAndCommands_FileIsCreated()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] commands = { "moveto 10 20", "drawcircle 15 true", "setcolour red" };

            // Act
            commandParser.SaveFile(TestFileName, commands);

            // Assert
            Assert.IsTrue(File.Exists(TestFileName));

            // Cleanup
            File.Delete(TestFileName);
        }

        /// <summary>
        /// Tests the OpenFile method with a valid file name and commands, expecting the commands to be executed.
        /// </summary>
        [TestMethod]
        public void OpenFile_ValidFileNameAndCommands_CommandsAreExecuted()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] originalCommands = { "moveto 10 20", "drawcircle 15 true", "setcolour red" };

            // Save commands to a file
            commandParser.SaveFile(TestFileName, originalCommands);

            // Act
            commandParser.OpenFile(TestFileName);

            // Assert
            // You may want to assert the effects of the executed commands on the form or command parser state

            // Cleanup
            File.Delete(TestFileName);
        }

        /// <summary>
        /// Tests the SaveFile method with an invalid file name, expecting an ArgumentException to be thrown.
        /// </summary>
        [TestMethod]
        public void SaveFile_InvalidFileName_ThrowsException()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);
            string[] commands = { "moveto 10 20", "drawcircle 15 true", "setcolour red" };

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => commandParser.SaveFile(string.Empty, commands));
        }

        /// <summary>
        /// Tests the OpenFile method with a non-existent file, expecting a FileNotFoundException to be thrown.
        /// </summary>
        [TestMethod]
        public void OpenFile_NonExistentFile_ThrowsException()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);

            // Act and Assert
            try
            {
                commandParser.OpenFile("NonExistentFile.txt");
                Assert.Fail("Expected FileNotFoundException was not thrown.");
            }
            catch (FileNotFoundException ex)
            {
                // Assert specific properties of the exception if needed
                Assert.AreEqual("NonExistentFile.txt", ex.FileName);
            }
            catch (Exception)
            {
                Assert.Fail("Expected FileNotFoundException was not thrown.");
            }
        }

        /// <summary>
        /// Tests the OpenFile method with an empty file, expecting no commands to be executed.
        /// </summary>
        [TestMethod]
        public void OpenFile_EmptyFile_NoCommandsExecuted()
        {
            // Arrange
            var form = new Form1();
            var commandParser = new CommandParser(form);

            // Create an empty file
            File.WriteAllText(TestFileName, string.Empty);

            // Act
            commandParser.OpenFile(TestFileName);

            // Assert
            // Ensure that no commands are executed or the state remains unchanged

            // Cleanup
            File.Delete(TestFileName);
        }

        /// <summary>
        /// Helper method to check if a specified action throws a specific exception type.
        /// </summary>
        /// <typeparam name="T">The type of exception expected.</typeparam>
        /// <param name="action">The action to execute.</param>
        /// <returns>True if the expected exception is thrown; otherwise, false.</returns>
        private bool ThrowsException<T>(Action action) where T : Exception
        {
            try
            {
                action();
                return false;
            }
            catch (T)
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
