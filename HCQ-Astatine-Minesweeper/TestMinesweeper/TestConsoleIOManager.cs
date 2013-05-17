using System;
using Minesweeper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestMinesweeper
{
    [TestClass]
    public class TestioManager
    {
        private readonly IIOManager ioManager = new ConsoleIOManager();

        [TestMethod]
        public void TestInitialMessage()
        {
            string welcomeMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines.";
            string commands = "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            string outputMessage = ioManager.PrintInitialMessage();
            Assert.AreEqual(outputMessage, (welcomeMessage + " " + commands));
        }

        [TestMethod]
        public void TestPrintGameFieldNoBoomed()
        {
            using (ConsoleRedirector cr = new ConsoleRedirector())
            {
                Field gameField = new Field();
                bool hasBoomed = false;
                string scoreBoard = @"     0 1 2 3 4 5 6 7 8 9
   ---------------------
0 |  ? ? ? ? ? ? ? ? ? ?|
1 |  ? ? ? ? ? ? ? ? ? ?|
2 |  ? ? ? ? ? ? ? ? ? ?|
3 |  ? ? ? ? ? ? ? ? ? ?|
4 |  ? ? ? ? ? ? ? ? ? ?|
   --------------------";
                Assert.IsFalse(cr.ToString().Contains(scoreBoard));
                ioManager.PrintGameField(gameField, hasBoomed);
                Assert.IsTrue(cr.ToString().Contains(scoreBoard));
            }
        }

        [TestMethod]
        public void TestPrintExplosionMessage()
        {
            using (ConsoleRedirector cr = new ConsoleRedirector())
            {
                int score = 5;
                Assert.IsFalse(cr.ToString().Contains("\nBooom! You are killed by a mine!You revealed " +
                    score + " cells without mines."));
                ioManager.PrintExplosionMessage(score);
                Assert.IsTrue(cr.ToString().Contains("\nBooom! You are killed by a mine!You revealed " +
                    score + " cells without mines."));
            }
        }

        [TestMethod]
        public void TestPrintWinnerMessage()
        {
            using (ConsoleRedirector cr = new ConsoleRedirector())
            {
                Assert.IsFalse(cr.ToString().Contains("Congratulations! You are the WINNER!\nPlease enter your name for the top scoreboard: "));
                ioManager.PrintWinnerMessage();
                Assert.IsTrue(cr.ToString().Contains("Congratulations! You are the WINNER!\nPlease enter your name for the top scoreboard: "));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPrintScoreBoardOnNull()
        {
            ioManager.PrintScoreBoard(null);
        }

        [TestMethod]
        public void TestPrintInvalidCommandMessage()
        {
            using (ConsoleRedirector cr = new ConsoleRedirector())
            {
                Assert.IsFalse(cr.ToString().Contains("Invalid row/col entered! Try again!"));
                ioManager.PrintInvalidCommandMessage();
                Assert.IsTrue(cr.ToString().Contains("Invalid row/col entered! Try again!"));
            }
        }

        [TestMethod]
        public void TestPrintQuitMessage()
        {
            using (ConsoleRedirector cr = new ConsoleRedirector())
            {
                Assert.IsFalse(cr.ToString().Contains("\nGood bye!\n"));
                ioManager.PrintQuitMessage();
                Assert.IsTrue(cr.ToString().Contains("\nGood bye!\n"));
            }
        }

        [TestMethod]
        public void TestGetUserInput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("12",
                    Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ioManager.GetUserInput();

                    string expected = string.Format(
                        "Enter row and column: ", Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetUserInputNotValidInputWithOnlyOneNumber()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("1",
                    Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ioManager.GetUserInput();

                    string expected = string.Format(
                        "Enter row and column: ", Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod]
        public void TestGetUserNickname()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("Test Console.ReadLine()",
                    Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ioManager.GetUserNickname();

                    string expected = string.Format("Please enter your name: ");
                    string expected2 = string.Format(Environment.NewLine);
                    Assert.AreEqual<string>(expected + expected2, sw.ToString());
                }
            }
        }
    }
}
