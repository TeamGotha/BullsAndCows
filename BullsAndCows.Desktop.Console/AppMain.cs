using Engine;

namespace BullsAndCows.Desktop.Console
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BullsAndCows.Utils;

    static class AppMain
    {
        
        private static Dictionary<string, int> topScoreBoard = new Dictionary<string, int>();
        private static int lastPlayerScore = int.MinValue;
        private static List<KeyValuePair<string, int>> sortedDict = new List<KeyValuePair<string, int>>();

        private static void EnterScoreBoard(int score)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            topScoreBoard.Add(name, score);

            if (score > lastPlayerScore)
            {
                lastPlayerScore = score;
            }

            if (topScoreBoard.Count > 5)
            {
                foreach (KeyValuePair<string, int> player in topScoreBoard)
                {
                    if (player.Value == lastPlayerScore)
                    {
                        topScoreBoard.Remove(player.Key);
                        break;



                    }
                }
            }
            SortAndPrintScoreBoard();
        }
        private static void SortAndPrintScoreBoard()
        {
            foreach (var pair in topScoreBoard)
            {
                sortedDict.Add(new KeyValuePair<string, int>(pair.Key, pair.Value));
            }

            sortedDict.SortList();
            Console.WriteLine("Scoreboard: ");
            
            sortedDict.Clear();
        }
        private static void Main()
        {
            GameEngine engine = GameEngine.Instance;

            engine.Run();

            //string nn = GenerateRandomSecretNumber();
            //string n = null;
            //int count1 = 0;
            //int count2 = 0;

            //    if (n == "help")
            //    {
                    
            //    }
            //    else if (n == "restart")
            //    {
            //        Console.WriteLine();
            //        StartGame();
            //        count1 = 0;
            //        nn = GenerateRandomSecretNumber();
            //        continue;
            //    }
            //    else if (n == "top")
            //    {
            //        if (topScoreBoard.Count == 0)
            //        {
            //            Console.WriteLine("Top scoreboard is empty.");
            //        }
            //        else
            //        {
            //            SortAndPrintScoreBoard();
            //        }
            //        continue;
            //    }
            //    else if (n == "exit")
            //    {
            //        Console.WriteLine("Good bye!");
            //        break;
            //    }
            //    else if (n.Length != 4 || proverka(n) == false)
            //    {
            //        Console.WriteLine("Incorrect guess or command!");
            //        continue;
            //    }
            //    count1++;
            //    int bulls = 0;
            //    int cows = 0;
            //    CalculateBullsAndCows(nn, n, ref bulls, ref cows);
            //    if (n == nn)
            //    {
            //        if (count2 > 0)
            //        {
            //            Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts and {1} cheats.", count1, count2);
            //            Console.WriteLine("You are not allowed to enter the top scoreboard.");
            //            SortAndPrintScoreBoard();
            //            Console.WriteLine();
            //            StartGame();
            //            count1 = 0;
            //            count2 = 0;
            //            nn = GenerateRandomSecretNumber();
            //        }
            //        else
            //        {
            //            Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts.", count1);
            //            EnterScoreBoard(count1);
            //            count1 = 0;
            //            Console.WriteLine();
            //            StartGame();
            //            nn = GenerateRandomSecretNumber();
            //        }
            //        continue;
            //    }
            //    Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bulls, cows);
            //}
        }
    }
}
