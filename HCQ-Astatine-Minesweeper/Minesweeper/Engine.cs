namespace Minesweeper
{
    using System;
    using System.Linq;

    public class Engine
    {
        public static void PlayMines()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            Field gameField = new Field();

            int row = 0;
            int col = 0;
            int minesCounter = 0;
            int revealedCellsCounter = 0;
            bool isBoomed = false;

            GameInitialization(gameField, scoreBoard, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
        }
  
        private static void GameInitialization(Field gameField, ScoreBoard scoreBoard, ref int row, ref int col, ref int minesCounter, ref int revealedCellsCounter, ref bool isBoomed)
        {
            StartNewGame(gameField, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
            gameField.FillWithRandomMines();

            ConsoleIOManager.PrintInitialMessage();

            string input = string.Empty;

            while (true)
            {
                ConsoleIOManager.PrintGameField(gameField, isBoomed);
                input = ConsoleIOManager.GetUserInput();

                if (IsMoveEntered(input))
                {
                    FindTheWinner(input, gameField, scoreBoard, minesCounter, ref row, ref col, ref isBoomed, ref revealedCellsCounter);
                }
                else if (IsInputCorrect(input))
                {
                    if (input == "top")
                    {
                        ConsoleIOManager.PrintScoreBoard(scoreBoard);
                        GameInitialization(gameField, scoreBoard, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                    }
                    else if (input == "exit")
                    {
                        ConsoleIOManager.PrintQuitMessage();
                        break;
                    }
                
                    else if (input == "restart")
                    {
                        GameInitialization(gameField, scoreBoard, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                    }

                }
               else
                {
                    Console.WriteLine("Invalid Command!");
                }
            }
        }
  
        private static void FindTheWinner(string input, Field gameField, ScoreBoard scoreBoard, int minesCounter, ref int row, ref int col, ref bool isBoomed, ref int revealedCellsCounter)
        {
            string[] inputParams = input.Split();
            row = int.Parse(inputParams[0]);
            col = int.Parse(inputParams[1]);

            if ((row >= 0) && (row < gameField.Rows) && (col >= 0) && (col < gameField.Cols))
            {
                bool hasBoomedMine = gameField.CheckIfIsMine(row, col);

                if (hasBoomedMine)
                {
                    isBoomed = true;
                    ConsoleIOManager.PrintGameField(gameField, isBoomed);
                    ConsoleIOManager.PrintExplosionMessage(revealedCellsCounter);

                    string currentPlayerName = ConsoleIOManager.GetUserNickname();
                    scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                    GameInitialization(gameField, scoreBoard, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                }

                bool winner = IsItWinner(gameField.Matrix, minesCounter);

                if (winner)
                {
                    ConsoleIOManager.PrintWinnerMessage();

                    string currentPlayerName = ConsoleIOManager.GetUserNickname();
                    scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                    GameInitialization(gameField, scoreBoard, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                }

                gameField.Update(row, col);
                revealedCellsCounter++;
            }
            else
            {
                ConsoleIOManager.PrintInvalidCommandMessage();
            }
        }

        private static bool IsInputCorrect(string input)
        {
            if (input.Equals("top") || input.Equals("restart") || input.Equals("exit"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsMoveEntered(string input)
        {
            bool validMove = false;
            try
            {
                string[] inputParams = input.Split();
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

        private static bool IsItWinner(string[,] matrix, int cntMines)
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

        private static void StartNewGame(Field gameField, ref int row,  ref int col, ref int minesCounter, 
            ref int revealedCellsCounter, ref bool isBoomed)
        {
            gameField.Clear();
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            isBoomed = false;
        }
    }
}
