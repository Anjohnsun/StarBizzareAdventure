using System;

namespace BizzareStarHomework
{
    class Program
    {
        static bool keepPlaying = true;
        static Coin coin = new Coin();
        static Stalker stalker = new Stalker();

        static void Main(string[] args)
        {
            CreateTable();
            CreatePlayer(Console.WindowWidth / 2, Console.WindowHeight / 2);
            PlayGame();

            Console.SetCursorPosition(0, 23);
        }



        private static void PlayGame()
        {
            int[] enPoses = activateEnemy();    //[0] позиция по иксу, [1] по игреку
            int[] playerPoses = new int[2] { Console.WindowWidth / 2, Console.WindowHeight / 2 };
            //  int playerPoses[0] = Console.WindowWidth / 2;
            //  int playerPoses[1] = Console.WindowHeight / 2;
            coin.CreateCoin(playerPoses, enPoses, stalker.stCoords);
            stalker.CreateStalker(playerPoses, enPoses, coin.coinPoses);

            bool checkCorrectInput;
            int counter = 0;
            int totalWay = 0;
            int way;

            while (keepPlaying)
            {
                ConsoleKey currentKey = Console.ReadKey().Key;
                Console.SetCursorPosition(playerPoses[0], playerPoses[1]);
                Console.Write(' ');
                checkCorrectInput = true;
                switch (currentKey)
                {
                    case ConsoleKey.Escape:
                        keepPlaying = false;
                        break;
                    case ConsoleKey.UpArrow:
                        if (playerPoses[1] - Console.WindowHeight / 4 > 1)
                        {
                            playerPoses[1]--;
                        }
                        else
                        {
                            playerPoses[1] = Console.WindowHeight / 4 * 3 - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Console.WindowHeight / 4 * 3 - playerPoses[1] > 1)
                        {
                            playerPoses[1]++;
                        }
                        else
                        {
                            playerPoses[1] = Console.WindowHeight / 4 + 1;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (playerPoses[0] - Console.WindowWidth / 4 > 2)
                        {
                            playerPoses[0]--;
                        }
                        else
                        {
                            playerPoses[0] = Console.WindowWidth / 4 * 3 - 2;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (Console.WindowWidth / 4 * 3 - playerPoses[0] > 2)
                        {
                            playerPoses[0]++;
                        }
                        else
                        {
                            playerPoses[0] = Console.WindowWidth / 4 + 2;
                        }
                        break;
                    default:
                        checkCorrectInput = false;
                        break;
                }
                CreatePlayer(playerPoses[0], playerPoses[1]);

                //ход игрока закончился
                if (StrangeEnemyAway(playerPoses, enPoses))
                {
                    break;
                }
                if (stalker.StalkerAway(playerPoses, coin.coinPoses, coin))
                {
                    break;
                }
                System.Threading.Thread.Sleep(50);
                //Enemy turn

                Console.SetCursorPosition(enPoses[0], enPoses[1]);
                Console.Write(' ');

                if (counter % 8 == 0 || counter == 0)
                {
                    totalWay = DefineTotalWay();
                }


                if (checkCorrectInput)
                {
                    Random random = new Random();
                    way = random.Next(1, 15);
                    if (way > 4) { way = totalWay; }
                    switch (way)
                    {
                        case 1:
                            if (enPoses[1] - Console.WindowHeight / 4 > 1)
                            {
                                enPoses[1]--;
                            }
                            else
                            {
                                enPoses[1] = Console.WindowHeight / 4 * 3 - 1;
                            }
                            break;
                        case 2:
                            if (Console.WindowHeight / 4 * 3 - enPoses[1] > 1)
                            {
                                enPoses[1]++;
                            }
                            else
                            {
                                enPoses[1] = Console.WindowHeight / 4 + 1;
                            }
                            break;
                        case 3:
                            if (enPoses[0] - Console.WindowWidth / 4 > 3)
                            {
                                enPoses[0]--;
                            }
                            else
                            {
                                enPoses[0] = Console.WindowWidth / 4 * 3 - 2;
                            }
                            break;
                        case 4:
                            if (Console.WindowWidth / 4 * 3 - enPoses[0] > 3)
                            {
                                enPoses[0]++;
                            }
                            else
                            {
                                enPoses[0] = Console.WindowWidth / 4 + 2;
                            }
                            break;
                        default:
                            break;
                    }
                }
                Console.SetCursorPosition(enPoses[0], enPoses[1]);
                Console.Write('#');
                counter++;
                //движение сталкера
                stalker.StalkerMove(playerPoses);

                //проверка на собранные монеты
                if (coin.IsCollected("player", playerPoses))
                {
                    coin.CreateCoin(playerPoses, enPoses, stalker.stCoords);
                }
                else if (coin.IsCollected("enemy", enPoses))
                {
                    coin.CreateCoin(playerPoses, enPoses, stalker.stCoords);
                }
                else if (coin.IsCollected("enemy", stalker.stCoords))
                {
                    coin.CreateCoin(playerPoses, enPoses, stalker.stCoords);
                }
                //проверка на врага
                StrangeEnemyAway(playerPoses, enPoses);
                stalker.StalkerAway(playerPoses, coin.coinPoses, coin);
            }
        }//описан процесс игры



        private static void CreatePlayer(int xPos, int yPos)
        {
            Console.SetCursorPosition(xPos, yPos);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write('*');
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 25);
        }//отрисовка игрока
        public static void CreateTable()
        {
            int widthStart = Console.WindowWidth / 4;
            int heightStart = Console.WindowHeight / 4;

            Console.BackgroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = widthStart; i < widthStart * 3; i++)
            {
                Console.SetCursorPosition(i + 1, heightStart);
                Console.Write('-');
                Console.SetCursorPosition(i + 1, heightStart * 3);
                Console.Write('-');
            }
            for (int i = heightStart - 1; i < heightStart * 3; i++)
            {
                Console.SetCursorPosition(widthStart, i + 1);
                Console.Write('|');
                Console.SetCursorPosition(widthStart * 3, i + 1);
                Console.Write('|');
            }
        }//создание чистого стола
        public static void StopGame(string reason)
        {
            keepPlaying = false;
            Console.Clear();
            CreateTable();
            switch (reason)
            {
                case "StrangeEnemy":
                    CreateTable();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 1);
                    Console.Write("GAME OVER,");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2);
                    Console.Write("STRANGE ENEMY");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2 + 1);
                    Console.Write("IS STRONGER");
                    break;
                case "PointWin":
                    CreateTable();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 1);
                    Console.Write("CONGRATULATIONS,");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2);
                    Console.Write("YOU WON!");
                    break;
                case "PointLoose":
                    CreateTable();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 1);
                    Console.Write("YOU ARE SO SLOW,");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2);
                    Console.Write("YOU ARE LOOSER!");
                    break;
                case "Stalker":
                    CreateTable();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 2 - 1);
                    Console.Write("YOU'RE KILLED BY STALKER");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2);
                    Console.Write("SO SAD!");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }//остановка игры


        //методы странного врага
        private static int[] activateEnemy()
        {
            Random random = new Random();
            int enXPos, enYPos;
            enXPos = (random.Next(0, Console.WindowWidth / 2 - 3)) + Console.WindowWidth / 4;
            enYPos = (random.Next(0, Console.WindowHeight / 2 - 2)) + Console.WindowHeight / 4 + 1;

            if (enXPos == Console.WindowWidth - 1 / 2 && enYPos == Console.WindowHeight)
            {
                activateEnemy();
            }

            Console.SetCursorPosition(enXPos, enYPos);
            Console.Write('#');

            int[] poses = new int[2] { enXPos, enYPos };

            return poses;
        }//генерация странного врага
        public static int DefineTotalWay()
        {
            Random random = new Random();
            int totalWay = random.Next(1, 5);
            return totalWay;
        }//выбор направления для странного врага
        private static bool StrangeEnemyAway(int[] playerPoses, int[] enemyPoses)
        {
            if (playerPoses[0] == enemyPoses[0] && playerPoses[1] == enemyPoses[1])
            {
                StopGame("StrangeEnemy");
                return true;
            }
            return false;
        }//проверка на проигрыш по странному врагу
    }
}

/*Список изменений: 
 * 1) задержка перед ходом соперника
 * 2) изменение рамок игрового поля
 * 3) изменение поведения странного врага(предпочитаемое направление)
 * 4) задержка перед окончанием игры
 * 5) изменение позиции счётчика
 * 6) изменение надписей при окончании игры
 * 7) сталкер может собирать монеты
 * 
 * Нерешённые проблемы: 
 * 1) Ввод непредусмотренных символов  
 * 2) Монетка может появиться в недосягаемой зоне(близко к рамке)   DONE
 * 3) После окончания игры на поле появляется враг    DONE
 * 
 */
