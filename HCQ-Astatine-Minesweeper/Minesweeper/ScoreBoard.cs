namespace Minesweeper
{
    using System;
    using System.Linq;
    using System.Text;
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
        /// <returns>a string which shows table with players' score result</returns>
        public string PrintScoreBoard()
        {
            if (this.scoreBoard.Values.Count == 0)
            {
                string emptyScoreBoard = "\n\rScoreboard empty!";

                //just return string which said table is empty, no players were played
                return emptyScoreBoard;
            }
            else
            {
                //a stringbuilder will keep the entire table
                StringBuilder scoreBoard = new StringBuilder();
                scoreBoard.Append("\n\rScoreboard:"); //this is the header of the table

                int currentPlayer = 1; orderedScoreDescending = this.scoreBoard.Keys.OrderByDescending(obj => obj);
                foreach (int key in orderedScoreDescending)
                {
  
                {
                    foreach (string person in this.scoreBoard[key]){
                        if (currentPlayer <= MAX_SHOWNED_PLAYERS_ON_SCOREBOARD)
                     
                        {       Console.WriteLine("{0}//add every loop the current player which have score. The maximum players in table are 5
                            scoreBoard.Append(string.Format("\n\r{0}. {1} --> {2} cells", currentPlayer, person, key)  
                            currentPlayer++;
                        }
                    }
                }
                
                //the entire scoreboard table is here and it is made to be a string with method ToString();
                //Now the game is printing nothing, cause no Console is used.
                return scoreBoard.ToString();
            }
        }
    }
}
