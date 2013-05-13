namespace Minesweeper
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the field of the minesweeper game.
    /// </summary>
    internal class Field
    {
        private const int MinesNumber = 15;
        private const int MatrixRows = 5;
        private const int MatrixCols = 10;

        private readonly string[,] matrix;

        /// <summary>
        /// Constructs a new field - a matrix with fixed size.
        /// </summary>
        public Field()
        {
            this.matrix = new string[5, 10];
        }

        /// <summary>
        /// Returns the number of rows in the field.
        /// </summary>
        public int Rows
        {
            get
            {
                return MatrixRows;
            }
        }

        /// <summary>
        /// Returns the number of columns in the field.
        /// </summary>
        public int Cols
        {
            get
            {
                return MatrixCols;
            }
        }

        /// <summary>
        /// Returns the two dimensional matrix of the field.
        /// </summary>
        public string[,] Matrix
        {
            get
            {
                return this.matrix;
            }
        }

        /// <summary>
        /// Fills the field with certain fixed number of mines in random positions.
        /// </summary>
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

        /// <summary>
        /// Checks if there is a mine on certain position represented by row and column of the field.
        /// </summary>
        /// <param name="row">The row number of the specified position.</param>
        /// <param name="col">The col number of the specified position.</param>
        /// <returns>Returns true if there is a mine on the specified position. Otherwise returns false.</returns>
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

        /// <summary>
        /// Updates the matrix after a valid move. Puts on the specified position the number of adjacent mines.
        /// </summary>
        /// <param name="row">The row number of the specified position.</param>
        /// <param name="col">The col number of the specified position.</param>
        public void Update(int row, int col)
        {
            ValidateCoordinates(row, col);

            int minesCounter = this.CalculateAdjacentMines(row, col);
            this.matrix[row, col] = minesCounter.ToString();

        }

        /// <summary>
        /// Clears the two dimensional matrix of the field. Used to avoid initializing a new field every time when a new game starts.
        /// </summary>
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
