namespace Minesweeper
{
    using System;
    using System.Linq;

    /// <summary>
    /// Class which render the entire game
    /// </summary>
    class GameRenderer
    {
        /// <summary>
        /// Method prints the "welcome" visaulisation and give an instructions to the player
        /// </summary>
        public static void PrintInitialMessage()
        {
            string startMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use   'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            Console.WriteLine(startMessage + "\n");
        }

        // TODO: Rename; push an object of type 
        public static void Display(string[,] tableAsMatrix, bool boomed)
        {
            Console.WriteLine();
            Console.WriteLine("     0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int row = 0; row < tableAsMatrix.GetLength(0); row++)
            {
                Console.Write("{0} | ", row);
                for (int col = 0; col < tableAsMatrix.GetLength(1); col++)
                {                    
                    string currentSymbolInMatrix = tableAsMatrix[row, col];
                    RenderPlayingTable(currentSymbolInMatrix, boomed);
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("   ---------------------");
        }

        /// <summary>
        /// Method renders the playing table
        /// </summary>
        /// <param name="currentSymbolInMatrix">consist the current symbol on table</param>
        /// <param name="boomed">shows if player alredy find a mine</param>
        private static void RenderPlayingTable(string currentSymbolInMatrix, bool boomed)
        {
            //TO DO: to think about make it with switch-case
            if (!(boomed) && ((currentSymbolInMatrix == string.Empty) || (currentSymbolInMatrix == "*")))
            {
                Console.Write(" ?");
            }
            else if (!(boomed) && (currentSymbolInMatrix != string.Empty) && (currentSymbolInMatrix != "*"))
            {
                Console.Write(" {0}", currentSymbolInMatrix);
            }
            else if ((boomed) && (currentSymbolInMatrix == string.Empty))
            {
                Console.Write(" -");
            }
            else if ((boomed) && (currentSymbolInMatrix != string.Empty))
            {
                Console.Write(" {0}", currentSymbolInMatrix);
            }
        }
    }
}
