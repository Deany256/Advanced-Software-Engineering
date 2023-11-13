using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Component1.tests
{
    [TestClass]
    public class CommandParserTests
    {
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
