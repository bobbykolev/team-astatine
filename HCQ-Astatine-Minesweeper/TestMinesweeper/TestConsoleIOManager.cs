namespace TestMinesweeper
{
    using System;
    using Minesweeper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestConsoleIOManager
    {
        [TestMethod]
        public void TestInitialMessage()
        {
            string welcomeMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines.";
            string commands = "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            string outputMessage = ConsoleIOManager.PrintInitialMessage();
            Assert.AreEqual(outputMessage,(welcomeMessage + " " + commands));
        }

        //[TestMethod]
        //public void TestUserInput()
        //{
        //    string input = "2, 3";
        //    string checkInput = ConsoleIOManager.GetUserInput();
        //    Assert.AreEqual(input, checkInput);
        //}
    }
}
