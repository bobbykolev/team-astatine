namespace Minesweeper
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the main application from which will start the game.
    /// </summary>
    public class Minesweeper
    {
        internal static void Main()
        {
            IEngine engine = new Engine();
            engine.Play();
        }
    }
}
