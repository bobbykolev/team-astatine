namespace TestMinesweeper
{
    using System;
    using Minesweeper;
    using Wintellect.PowerCollections;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestScoreBoard
    {
        [TestMethod]
        public void TestAddPlayerInScoreBoard()
        {
            OrderedMultiDictionary<int, string> orderedMultiDictionary = new OrderedMultiDictionary<int, string>(true);
            orderedMultiDictionary.Add(10, "Pesho");
            string playerName = "Pesho";
            int playerScore = 10;
            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.AreEqual(scoreBoard, orderedMultiDictionary);
        }

        [TestMethod]
        public void TestGenerateScoreBoard()
        {
            string output = "\n\rScoreboard:\n\r1. Pesho --> 10 cells\n";
            ScoreBoard scoreBoard = new ScoreBoard();
            string playerName = "Pesho";
            int playerScore = 10;
            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.AreEqual(output, scoreBoard.Generate());
        }
    }
}
