namespace Minesweeper
{
    using System;
    using System.Linq;
    using System.Text;
    using Wintellect.PowerCollections;

    /// <summary>
    /// Represents a scoreboard.
    /// </summary>
    public class ScoreBoard
    {
        private const int MaxShowedPlayersCount = 5;

        private readonly OrderedMultiDictionary<int, string> scoreBoard;

        /// <summary>
        /// Constructs an instance of the class.
        /// </summary>
        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        /// <summary>
        /// Method which adds a new player to the scoreboard.
        /// </summary>
        /// <param name="playerName">Player's nickname.</param>
        /// <param name="playerScore">Player's score.</param>
        public void AddPlayer(string playerName, int playerScore)
        {
            if ((playerName == null) || (playerName == string.Empty))
            {
                throw new ArgumentNullException("You cannot play without a nickname");
            }

            if (!this.scoreBoard.ContainsKey(playerScore))
            {
                this.scoreBoard.Add(playerScore, playerName);
            }
            else
            {
                this.scoreBoard[playerScore].Add(playerName);
            }
        }

        /// <summary>
        /// This method render the scoreboard
        /// </summary>
        /// <returns>a string which shows table with players' score result</returns>
        public string Generate()
        {
            if (this.scoreBoard.Values.Count == 0)
            {
                string emptyScoreBoard = "\n\rScoreboard empty!";

                return emptyScoreBoard;
            }
            else
            {
                StringBuilder scoreBoard = new StringBuilder();
                scoreBoard.Append("\n\rScoreboard:");

                int currentPlayer = 1;
                var orderedScoreDescending = this.scoreBoard.Keys.OrderByDescending(obj => obj);

                foreach (int key in orderedScoreDescending)
                {
                    foreach (string person in this.scoreBoard[key])
                    {
                        if (currentPlayer <= MaxShowedPlayersCount)
                        {
                            scoreBoard.Append(string.Format("\n\r{0}. {1} --> {2} cells", currentPlayer, person, key));
                            currentPlayer++;
                        }
                    }
                }

                scoreBoard.AppendLine();

                return scoreBoard.ToString();
            }
        }
    }
}
