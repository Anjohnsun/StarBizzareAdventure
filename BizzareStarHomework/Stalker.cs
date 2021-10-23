using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace BizzareStarHomework
{
    public partial class Stalker : Component
    {
        public int[] stCoords = new int[2];
        int xDif, yDif;

        public void CreateCoin(int[] playerPoses, int[] enemyPoses, int[] coinPoses)
        {
            Random random = new Random();

            stCoords[0] = (random.Next(0, Console.WindowWidth / 2 - 3)) + Console.WindowWidth / 4;
            stCoords[1] = (random.Next(0, Console.WindowHeight / 2 - 2)) + Console.WindowHeight / 4 + 1;

            if (stCoords[0] != playerPoses[0] || stCoords[1] != playerPoses[1])
            {
                if (stCoords[0] != enemyPoses[0] || stCoords[1] != enemyPoses[1])
                {
                    if (stCoords[0] != coinPoses[0] || stCoords[1] != coinPoses[1])
                    {
                        Console.SetCursorPosition(stCoords[0], stCoords[1]);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('@');
                    }
                }
            }
        }

        public void StalkerMove(int[] playerPoses)
        {
            if (playerPoses[0] > stCoords[0])
            {
                xDif = playerPoses[0] - stCoords[0];
            }

            _ = playerPoses[0] > stCoords[0] ? xDif = playerPoses[0] - stCoords[0] : xDif = stCoords[0] - playerPoses[0];
            _ = playerPoses[1] > stCoords[1] ? yDif = playerPoses[1] - stCoords[1] : yDif = stCoords[1] - playerPoses[1];
        }

    }
}
