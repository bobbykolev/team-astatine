namespace Minesweeper
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    class ScoreBoard
    {
        // - moga da izpolzvam OrderedMultiDictionary!
        // - xaxax ne sym li gyzar? a?
        private OrderedMultiDictionary<int, string> scoreBoard;

        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        public void AddPlayer(string playerName, int playerScore)
        {
            if (!scoreBoard.ContainsKey(playerScore))
            {
                scoreBoard.Add(playerScore, playerName);
            }
            else
            {
                scoreBoard[playerScore].Add(playerName);
            }
        }

        // TODO: renamend (Visualize etc); return the scoreboard as string, not to print on the console
        public void PrintScoreBoard()
        {
            bool FirstFive = false;
            int currentCounter = 1;

            Console.WriteLine();
            if (this.scoreBoard.Values.Count == 0)
            {
                Console.WriteLine("Scoreboard empty!");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                //tui e magiq!
                //kvo da se prai - pone bachka
                foreach (int key in this.scoreBoard.Keys.OrderByDescending(obj => obj))
                {


                    foreach (string person in this.scoreBoard[key])
                    {
                        if (currentCounter < 6)
                        //nedei se zamislq za tui 6!
                        //vqrno e i nqma kak da stane inache
                        //moje da go priemesh kato vid kod
                        {
                            Console.WriteLine("{0}. {1} --> {2} cells", currentCounter, person, key);
                            currentCounter++;
                        }
                        else
                        {
                            FirstFive = true;
                            break;
                        }
                    }
                    if (FirstFive)
                    {
                        break;
                    }
                }


            }
            Console.WriteLine();
        }
    }
}
