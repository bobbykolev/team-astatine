namespace Minesweeper
{
    using System;
    using System.Linq;

    class Field
    {
        private const int NUMBER_OF_MINES = 15;
        private const int MINES_FIELD_ROWS = 5;
        private const int MINES_FIELD_COLS = 10;

        public static void FillWithRandomMines(string[,] mines, Random randomMines)
        {
            int minesCounter = 0;
            while (minesCounter < NUMBER_OF_MINES)
            {
                int randomRow = randomMines.Next(0, 5);
                int randomCol = randomMines.Next(0, 10);
                if (mines[randomRow, randomCol] == "")
                {
                    mines[randomRow, randomCol] += "*";
                    minesCounter++;
                }
            }
        }

        // TODO: Rename (ConstructBoard etc); out parameters
        public static void Zapochni(out string[,] mines, out int row,
        out int col, out bool isBoomed, out int minesCounter, out Random randomMines, out int revealedCellsCounter)
        {
            randomMines = new Random();
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            isBoomed = false;
            mines = new string[MINES_FIELD_ROWS, MINES_FIELD_COLS];

            for (int i = 0; i < mines.GetLength(0); i++)
            {
                for (int j = 0; j < mines.GetLength(1); j++)
                {
                    mines[i, j] = "";
                }
            }
        }

        // TODO: Rename (UpdateAfterTurn etc)
        public static bool Boom(string[,] matrica, int minesRow, int minesCol)
        {
            bool isKilled = false;
            int[] dRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dCol = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int minesCounter = 0;
            if (matrica[minesRow, minesCol] == "*")
            {
                isKilled = true;
            }

            // This has to be reffactored
            if ((matrica[minesRow, minesCol] != "") && (matrica[minesRow, minesCol] != "*"))
            {
                Console.WriteLine("Illegal Move!");
            }

            if (matrica[minesRow, minesCol] == "")
            {
                for (int direction = 0; direction < 8; direction++)
                {
                    int newRow = dRow[direction] + minesRow;
                    int newCol = dCol[direction] + minesCol;
                    if ((newRow >= 0) && (newRow < matrica.GetLength(0)) &&
                        (newCol >= 0) && (newCol < matrica.GetLength(1)) &&
                        (matrica[newRow, newCol] == "*"))
                    {
                        minesCounter++;
                    }
                }
                matrica[minesRow, minesCol] += Convert.ToString(minesCounter);
            }
            return isKilled;
        }
    }
}
