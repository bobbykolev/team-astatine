namespace Minesweeper
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    /// <summary>
    /// Class for printing players' scores
    /// </summary>
    class ScoreBoard
    {       /// <summary>
        /// const for visualisation maximum player on scoreboard
        /// </summary>
   
        private const int MAX_SHOWNED_PLAYERS_ON_SCOREBOARD = 5;
     /// <summary>
        /// OrderedMultiDictionary for keeping player's nickname and score
        /// </summary>
     
        private readonly OrderedMultiDictionary<int, string> scoreBoard;
   /// <summary>
        /// Constructor which make an instance of OrderedMultiDictionary
        /// </summary>
       
        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }
 /// <summary>
        /// Method which add new player on scoreboard
        /// </summary>
        /// <param name="playerName">player's nickname as string</param>
        /// <param name="playerScore">player's score as int</param>
        pub
        public void AddPlayer(string playerName, int playerScore)           if ((playerName == null) || (playerName == string.Empty))
            {
                throw new ArgumentNullException("You cannot play without a nickname");
            }

            if

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
        public void PrintScoreBoard()
        {
            Console.WriteLine();

            if (this.scoreBoard.Values.Count == 0)
            {
                Console.WriteLine("Scoreboard empty!");
            }
            else
            {
                Console.WriteLine("Scoreboard:");

                int currentPlayer = 1; orderedScoreDescending = this.scoreBoard.Keys.OrderByDescending(obj => obj);
                foreach (int key in orderedScoreDescending)
                {
  
                {
                    foreach (string person in this.scoreBoard[key]){
                        if (currentPlayer <= MAX_SHOWNED_PLAYERS_ON_SCOREBOARD)
                     
                        {       Console.WriteLine("{0}. {1} --> {2} cells", currentPlayer, person, key);
                     
                            currentPlayer++;
                        }
                    }
                }
            }

            Console.WriteLine();
        }
    }
}
