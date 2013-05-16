namespace Minesweeper
{
    using System;

    public interface IScoreBoard
    {
        /// <summary>
        /// Adds a new player to the scoreboard.
        /// </summary>
        /// <param name="playerName">Player's nickname.</param>
        /// <param name="playerScore">Player's score.</param>
        void AddPlayer(string name, int score);

        /// <summary>
        /// Generates the scoreboard with the specified number of top players and returns it as string.
        /// </summary>
        /// <returns>The generated scoreboard as string.</returns>
        string Generate();
    }
}
