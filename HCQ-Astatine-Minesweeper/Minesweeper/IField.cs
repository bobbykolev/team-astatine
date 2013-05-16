namespace Minesweeper
{
    using System;

    public interface IField
    {
        /// <summary>
        /// Returns the number of rows in the field.
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Returns the number of columns in the field.
        /// </summary>
        int Cols { get; }

        /// <summary>
        /// Returns the inner representation of the field.
        /// </summary>
        string[,] Matrix { get; }

        /// <summary>
        /// Initialize each cell of the two dimensional matrix of the field with <see cref="System.String.Empty"/> value. 
        /// Used to avoid creating a new field every time when a new game starts.
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// Fills the field with certain fixed number of mines in random positions.
        /// </summary>
        void FillWithRandomMines();

        /// <summary>
        /// Checks if there is a mine on certain position represented by row and column of the field.
        /// </summary>
        /// <param name="row">The row number of the specified position.</param>
        /// <param name="col">The col number of the specified position.</param>
        bool IsMine(int row, int col);

        /// <summary>
        /// Updates the matrix after a valid move. Puts on the specified position the number of adjacent mines.
        /// </summary>
        /// <param name="row">The row number of the specified position.</param>
        /// <param name="col">The col number of the specified position.</param>
        void Update(int row, int col);
    }
}
