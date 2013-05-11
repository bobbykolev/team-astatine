namespace Minesweeper
{
    using System;
    using System.Linq;

    internal class Field
    {
        private const int MinesNumber = 15;
        private const int MatrixRows = 5;
        private const int MatrixCols = 10;

        private string[,] matrix;

        public Field()
        {
            this.matrix = new string[5, 10];
        }

        public int Rows
        {
            get
            {
                return MatrixRows;
            }
        }

        public int Cols
        {
            get
            {
                return MatrixCols;
            }
        }

        public string[,] Matrix
        {
            get
            {
                return this.matrix;
            }
        }

        public void FillWithRandomMines()
        {
            Random random = new Random();
            int minesCounter = 0;

            while (minesCounter < MinesNumber)
            {
                int randomRow = random.Next(0, MatrixRows);
                int randomCol = random.Next(0, MatrixCols);

                if (this.matrix[randomRow, randomCol] == string.Empty)
                {
                    this.matrix[randomRow, randomCol] = "*";
                    minesCounter++;
                }
            }
        }

        public bool CheckIfIsMine(int row, int col)
        {
            ValidateCoordinates(row, col);

            bool cellHasMine = false;

            if (this.matrix[row, col] == "*")
            {
                cellHasMine = true;
            }

            return cellHasMine;
        }

        public void Update(int row, int col)
        {
            ValidateCoordinates(row, col);

            int minesCounter = this.CalculateAdjacentMines(row, col);
            this.matrix[row, col] = minesCounter.ToString();

        }

        public void Clear()
        {
            for (int i = 0; i < MatrixRows; i++)
            {
                for (int j = 0; j < MatrixCols; j++)
                {
                    this.matrix[i, j] = string.Empty;
                }
            }
        }

        private int CalculateAdjacentMines(int row, int col)
        {
            int[] rowDirections = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] colDirections = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int directionsCount = 8;

            int currentRow = 0;
            int currentCol = 0;
            bool coordsAreInRange = false;
            int minesCounter = 0;

            for (int i = 0; i < directionsCount; i++)
            {
                currentRow = rowDirections[i] + row;
                currentCol = colDirections[i] + col;

                coordsAreInRange = (0 <= currentRow && currentRow < MatrixRows) && (0 <= currentCol && currentCol < MatrixCols);

                if (coordsAreInRange && this.matrix[currentRow, currentCol] == "*")
                {
                    minesCounter++;
                }
            }

            return minesCounter;
        }

        private static void ValidateCoordinates(int row, int col)
        {
            if (row < 0 || MatrixRows <= row)
            {
                throw new ArgumentOutOfRangeException(string.Format("Invalid row value: {0}! It must be in the range [0, {1}]!",
                    row, MatrixRows));
            }

            if (col < 0 || MatrixCols <= col)
            {
                throw new ArgumentOutOfRangeException(string.Format("Invalid column value: {0}! It must be in the range [0, {1}]!",
                    col, MatrixCols));
            }
        }
    }
}
