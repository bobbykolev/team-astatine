namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
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
            bool hasExploded = false;

            while (true)
            {
                ConsoleIOManager.PrintInitialMessage();
                StartNewGame(gameField, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref hasExploded);

                ConsoleIOManager.PrintGameField(gameField, hasExploded);

                bool rowAndColAreValid = false;
                string input = string.Empty;

                while (true)
                {
                    input = ConsoleIOManager.GetUserInput();

                    if (IsMoveEntered(input))
                    {
                        string[] inputParams = input.Split();
                        row = int.Parse(inputParams[0]);
                        col = int.Parse(inputParams[1]);

                        rowAndColAreValid = ValidateRowAndCol(gameField, row, col);

                        if (rowAndColAreValid)
                        {
                            bool cellHasMine = gameField.IsMine(row, col);

                            if (cellHasMine)
                            {
                                hasExploded = true;
                                ConsoleIOManager.PrintGameField(gameField, hasExploded);
                                ConsoleIOManager.PrintExplosionMessage(revealedCellsCounter);

                                string currentPlayerName = ConsoleIOManager.GetUserNickname();
                                scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                                ConsoleIOManager.PrintScoreBoard(scoreBoard);
                                break;
                            }

                            bool winner = IsWinner(gameField.Matrix, minesCounter);

                            if (winner)
                            {
                                ConsoleIOManager.PrintWinnerMessage();

                                string currentPlayerName = ConsoleIOManager.GetUserNickname();
                                scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                                break;
                            }

                            gameField.Update(row, col);
                            revealedCellsCounter++;

                            ConsoleIOManager.PrintGameField(gameField, cellHasMine);
                        }
                        else
                        {
                            ConsoleIOManager.PrintInvalidCommandMessage();
                        }
                    }
                    else if (IsInputCorrect(input))
                    {
                        bool isRestart = false;

                        switch (input)
                        {
                            case "top":
                                {
                                    ConsoleIOManager.PrintScoreBoard(scoreBoard);
                                    continue;
                                }

                            case "exit":
                                {
                                    ConsoleIOManager.PrintQuitMessage();
                                    return;
                                }

                            case "restart":
                                {
                                    isRestart = true;
                                    break;
                                }
                        }

                        if (isRestart)
                        {
                            break;
                        }
                    }
                    else
                    {
                        ConsoleIOManager.PrintInvalidCommandMessage();
                    }
                }
            }
        }

        static List<int> rowsList = new List<int>();
        static List<int> colsList = new List<int>();

        private static bool ValidateRowAndCol(Field gameField, int row, int col)
        {
            bool validRow = 0 <= row && row < gameField.Rows;
            bool validCol = 0 <= col && col < gameField.Cols;

            bool validRowAndCol = validRow && validCol;

            rowsList.Add(row);
            colsList.Add(col);
            //chack for visited row/col
            for (int count = 0; count < rowsList.Count - 1; count++)
            {
                if (colsList[count] == col && rowsList[count] == row)
                {
                    validRowAndCol = false;
                }
            }

            return validRowAndCol;
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
            catch (FormatException fe)
            {
                validMove = false;
            }

            return validMove;
        }

        private static bool IsWinner(string[,] matrix, int cntMines)
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

        private static void StartNewGame(Field gameField, ref int row, ref int col, ref int minesCounter,
            ref int revealedCellsCounter, ref bool isBoomed)
        {
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            isBoomed = false;

            gameField.Initialize();
            gameField.FillWithRandomMines();

            rowsList.Clear();
            colsList.Clear();
        }
    }
}
