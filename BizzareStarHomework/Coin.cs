using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace BizzareStarHomework
{
    public partial class Coin : Component
    {

        int[] coinPoses = new int[2];
        private bool isCollected;
        int playerPoints, enemyPoints = 0;

        public Coin()
        {
            InitializeComponent();
        }

        public void CreateCoin(int[] playerPoses, int[] enemyPoses, int[] stalkerPoses)
        {
            Random random = new Random();

            coinPoses[0] = (random.Next(0, Console.WindowWidth / 2 - 3)) + Console.WindowWidth / 4;
            coinPoses[1] = (random.Next(0, Console.WindowHeight / 2 - 2)) + Console.WindowHeight / 4 + 1;

            if (coinPoses[0] != playerPoses[0] || coinPoses[1] != playerPoses[1])
            {
                if (coinPoses[0] != enemyPoses[0] || coinPoses[1] != enemyPoses[1])
                {
                    if (coinPoses[0] != stalkerPoses[0] || coinPoses[1] != stalkerPoses[1])
                    {
                        Console.SetCursorPosition(coinPoses[0], coinPoses[1]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('$');
                    }
                }
            }
            ReloadPoints();
        }

        public bool IsCollected(string creature, int[] poses)
        {
            if (coinPoses[0] == poses[0] && coinPoses[1] == poses[1])
            {
                isCollected = true;
                switch (creature)
                {
                    case "player":
                        playerPoints++;
                        break;
                    case "enemy":
                        enemyPoints++;
                        break;
                }
                ReloadPoints();
            }
            else
            {
                isCollected = false;
            }
            return isCollected;
        }

        private void ReloadPoints()
        {
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 - 2);
            Console.Write($"Ваши очки: {playerPoints, -31} Очки соперника: {enemyPoints}");

            if (playerPoints == 5)
            {
                Program.StopGame("PointWin");
            } else if(enemyPoints == 5)
            {
                Program.StopGame("PointLoose");
            }
        }
    }
}
