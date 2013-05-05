namespace Minesweeper
{
    using System;
    using System.Linq;

    public class Engine
    {
        public static void PlayMines()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            Random randomMines;
            string[,] minichki;
            int row;
            int col;
            int minesCounter;
            int revealedCellsCounter;
            bool isBoomed;

        // oxo glei glei
        // i go to si imam :)
        start:
            Field.Zapochni(out minichki, out row, out col, out isBoomed, out minesCounter, out randomMines, out revealedCellsCounter);

            Field.FillWithRandomMines(minichki, randomMines);

            GameRenderer.PrintInitialMessage();

            while (true)
            {
                GameRenderer.Display(minichki, isBoomed);
            enterRowCol:
                Console.Write("Enter row and column: ");
                string line = Console.ReadLine();
                line = line.Trim();

                if (IsMoveEntered(line))
                {
                    string[] inputParams = line.Split();
                    row = int.Parse(inputParams[0]);
                    col = int.Parse(inputParams[1]);

                    if ((row >= 0) && (row < minichki.GetLength(0)) && (col >= 0) && (col < minichki.GetLength(1)))
                    {
                        bool hasBoomedMine = Field.Boom(minichki, row, col);
                        if (hasBoomedMine)
                        {
                            isBoomed = true;
                            GameRenderer.Display(minichki, isBoomed);
                            Console.Write("\nBooom! You are killed by a mine! ");
                            Console.WriteLine("You revealed {0} cells without mines.", revealedCellsCounter);

                            Console.Write("Please enter your name for the top scoreboard: ");
                            string currentPlayerName = Console.ReadLine();
                            scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                            Console.WriteLine();
                            goto start;
                        }

                        bool winner = PichLiSi(minichki, minesCounter);
                        if (winner)
                        {
                            Console.WriteLine("Congratulations! You are the WINNER!\n");

                            Console.Write("Please enter your name for the top scoreboard: ");
                            string currentPlayerName = Console.ReadLine();
                            scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                            Console.WriteLine();
                            goto start;
                        }

                        revealedCellsCounter++;
                    }
                    else
                    {
                        Console.WriteLine("Enter valid Row/Col!\n");
                    }
                }
                else if (IsInputCorrect(line))
                {
                    switch (line)
                    {
                        case "top":
                            {
                                scoreBoard.PrintScoreBoard();
                                goto enterRowCol;
                            }

                        case "exit":
                            {
                                Console.WriteLine("\nGood bye!\n");
                                Environment.Exit(0);
                                break;
                            }

                        case "restart":
                            {
                                Console.WriteLine();
                                goto start;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Command!");
                }
            }
        }

        private static bool IsInputCorrect(string line)
        {
            if (line.Equals("top") || line.Equals("restart") || line.Equals("exit"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsMoveEntered(string line)
        {
            bool validMove = false;
            try
            {
                string[] inputParams = line.Split();
                int row = int.Parse(inputParams[0]);
                int col = int.Parse(inputParams[1]);

                validMove = true;
            }
            catch
            {
                validMove = false;
            }

            return validMove;
        }

        private static bool PichLiSi(string[,] matrix, int cntMines)
        {
            bool isWinner = false;
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((matrix[i, j] != string.Empty) && (matrix[i, j] != "*"))
                    {
                        counter++;
                    }
                }
            }

            if (counter == matrix.Length - cntMines)
            {
                isWinner = true;
            }

            return isWinner;
        }
    }
}
