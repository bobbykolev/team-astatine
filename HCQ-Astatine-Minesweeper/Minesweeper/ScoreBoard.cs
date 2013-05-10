namespace Minesweeper
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    class ScoreBoard
    {
        private readonly OrderedMultiDictionary<int, string> scoreBoard;

        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        public void AddPlayer(string playerName, int playerScore)
        {
            //TO DO: Implement exception
            if (!scoreBoard.ContainsKey(playerScore))
            {
                scoreBoard.Add(playerScore, playerName);
            }
            else
            {
                scoreBoard[playerScore].Add(playerName);
            }
        }

        public void PrintScoreBoard()
        {
            bool firstName = false;
            int currentCounter = 1;

            Console.WriteLine();
            if (this.scoreBoard.Values.Count == 0)
            {
                Console.WriteLine("Scoreboard empty!");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                foreach (int key in this.scoreBoard.Keys.OrderByDescending(obj => obj))
                {
                    foreach (string person in this.scoreBoard[key])
                    {
                        if (currentCounter < 6)
                        {
                            Console.WriteLine("{0}. {1} --> {2} cells", currentCounter, person, key);
                            currentCounter++;
                        }
                        else
                        {
                            firstName = true;
                            break;
                        }
                    }
                    if (firstName)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
