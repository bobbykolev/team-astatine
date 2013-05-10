namespace Minesweeper
{
    using System;
    using System.Linq;

    class GameRenderer
    {
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
            for (int i = 0; i < tableAsMatrix.GetLength(0); i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < tableAsMatrix.GetLength(1); j++)
                {
                    if (!(boomed) && ((tableAsMatrix[i, j] == "") || (tableAsMatrix[i, j] == "*")))
                    {
                        Console.Write(" ?");
                    }
                    if (!(boomed) && (tableAsMatrix[i, j] != "") && (tableAsMatrix[i, j] != "*"))
                    {
                        Console.Write(" {0}", tableAsMatrix[i, j]);
                    }
                    if ((boomed) && (tableAsMatrix[i, j] == ""))
                    {
                        Console.Write(" -");
                    }
                    if ((boomed) && (tableAsMatrix[i, j] != ""))
                    {
                        Console.Write(" {0}", tableAsMatrix[i, j]);
                    }

                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   ---------------------");
        }
    }
}
