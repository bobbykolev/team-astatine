using System;
using Minesweeper;
using Wintellect.PowerCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace TestMinesweeper
{
    [TestClass]
    public class TestScoreBoard
    {
        [TestMethod]
        public void TestAddPlayerInScoreBoard()
        {
            OrderedMultiDictionary<int, string> orderedMultiDictionary = new OrderedMultiDictionary<int, string>(true);
            orderedMultiDictionary.Add(7, "Mi6o");
            string playerName = "Mi6o";
            int playerScore = 7;
            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.ReferenceEquals(scoreBoard, orderedMultiDictionary);
        }

        [TestMethod]
        public void TestGenerateScoreBoard()
        {            
            ScoreBoard scoreBoard = new ScoreBoard();
            int currentPlayer = 1;
            string playerName = "Pesho";
            int playerScore = 10;
            string output = "\n\rScoreboard:\n\r" + currentPlayer + ". Pesho --> " + playerScore + " cells" + Environment.NewLine;
            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.AreEqual(output, scoreBoard.Generate());
        }
    }
}
