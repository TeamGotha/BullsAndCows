namespace BullsAndCows.Desktop.Console
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BullsAndCows.Utils;

    static class AppMain
    {
        private static char[] cheatNumber = { 'X', 'X', 'X', 'X' };
        private static Dictionary<string, int> topScoreBoard = new Dictionary<string, int>();
        private static int lastPlayerScore = int.MinValue;
        private static List<KeyValuePair<string, int>> sortedDict = new List<KeyValuePair<string, int>>();

        private static readonly ConsoleRenderer Renderer = new ConsoleRenderer();

        private static int SortDictionary(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
        {
            return a.Value.CompareTo(b.Value);
        }

        private static void StartGame()
        {
            string welcomeMessage = ConfigReader.GetConfigString("Welcome Message");
            string helpMessage = ConfigReader.GetConfigString("Help Message");

            Renderer.PrintLineMessage(welcomeMessage);
            Renderer.PrintLineMessage(helpMessage);
        }

        private static bool proverka(string num)
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Char.IsDigit(num[i]))
                {
                    count++;
                }
            }
            if (count == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private static string GenerateRandomSecretNumber()
        {
            StringBuilder secretNumber = new StringBuilder();
            Random rand = new Random();
            while (secretNumber.Length != 4)
            {



                int number = rand.Next(0, 10);
                secretNumber.Append(number.ToString());
            }
            return secretNumber.ToString();
        }

        private static void CalculateBullsAndCows(string secretNumber, string guessNumber, ref int bulls, ref int cows)
        {
            List<int> bullIndexes = new List<int>();
            List<int> cowIndexes = new List<int>();
            for (int i = 0; i < secretNumber.Length; i++)
            {
                if (guessNumber[i].Equals(secretNumber[i]))
                {
                    bullIndexes.Add(i);


                    bulls++;
                }
            }

            for (int i = 0; i < guessNumber.Length; i++)
            {
                for (int index = 0; index < secretNumber.Length; index++)
                {
                    if ((i != index) && !bullIndexes.Contains(index) && !cowIndexes.Contains(index) && !bullIndexes.Contains(i))
                    {
                        if (guessNumber[i].Equals(secretNumber[index]))
                        {
                            cowIndexes.Add(index);
                            cows++;
                            break;
                        }




                    }
                }
            }
        }
        private static char[] RevealNumberAtRandomPosition(string secretnumber, char[] cheatNumber)
        {
            while (true)
            {
                Random rand = new Random();
                int index = rand.Next(0, 4);
                if (cheatNumber[index] == 'X')
                {
                    cheatNumber[index] = secretnumber[index];
                    return cheatNumber;
                }
                else
                {
                    continue;



                }
            }
        }
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

            sortedDict.Sort(SortDictionary);
            Console.WriteLine("Scoreboard: ");
            int counter = 0;
            foreach (KeyValuePair<string, int> player in sortedDict)
            {
                counter++;
                Console.WriteLine("{0}. {1} --> {2} guesses", counter, player.Key, player.Value);
            }
            sortedDict.Clear();
        }
        private static void Main()
        {
            StartGame();

            string nn = GenerateRandomSecretNumber();
            string n = null;
            int count1 = 0;
            int count2 = 0;

            while (true)
            {
                Console.Write("Enter your guess or command: ");
                n = Console.ReadLine();

                if (n == "help")
                {
                    char[] revealedDigits = RevealNumberAtRandomPosition(nn, cheatNumber);
                    StringBuilder revealedNumber = new StringBuilder();
                    for (int i = 0; i < 4; i++)
                    {
                        revealedNumber.Append(revealedDigits[i]);
                    }
                    Console.WriteLine("The number looks like {0}", revealedNumber.ToString());
                    count2++;
                    continue;
                }
                else if (n == "restart")
                {
                    Console.WriteLine();
                    StartGame();
                    count1 = 0;
                    nn = GenerateRandomSecretNumber();
                    continue;
                }
                else if (n == "top")
                {
                    if (topScoreBoard.Count == 0)
                    {
                        Console.WriteLine("Top scoreboard is empty.");
                    }
                    else
                    {
                        SortAndPrintScoreBoard();
                    }
                    continue;
                }
                else if (n == "exit")
                {
                    Console.WriteLine("Good bye!");
                    break;
                }
                else if (n.Length != 4 || proverka(n) == false)
                {
                    Console.WriteLine("Incorrect guess or command!");
                    continue;
                }
                count1++;
                int bulls = 0;
                int cows = 0;
                CalculateBullsAndCows(nn, n, ref bulls, ref cows);
                if (n == nn)
                {
                    if (count2 > 0)
                    {
                        Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts and {1} cheats.", count1, count2);
                        Console.WriteLine("You are not allowed to enter the top scoreboard.");
                        SortAndPrintScoreBoard();
                        Console.WriteLine();
                        StartGame();
                        count1 = 0;
                        count2 = 0;
                        nn = GenerateRandomSecretNumber();
                    }
                    else
                    {
                        Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts.", count1);
                        EnterScoreBoard(count1);
                        count1 = 0;
                        Console.WriteLine();
                        StartGame();
                        nn = GenerateRandomSecretNumber();
                    }
                    continue;
                }
                Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bulls, cows);
            }
        }
    }
}
