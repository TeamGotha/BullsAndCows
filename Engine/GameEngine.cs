using System.Linq;

namespace Engine
{
    using System;
    using System.Collections.Generic;

    using BullsAndCows.Utils;
    using System.Text;

    public class GameEngine
    {
        private const int NumberLength = 4;
        private static readonly char[] DefaultCheatNumber = { 'X', 'X', 'X', 'X' };

        private static GameEngine instance = new GameEngine();
        private readonly ConsoleRenderer renderer;
        private Scoreboard scoreboard;
        private char[] cheatNumber = GameEngine.DefaultCheatNumber.ToArray();
        private string secretNumber;
        private int bulls;
        private int cows;
        private int cheatCounter;
        private int attempts;


        private GameEngine()
        {
            this.renderer = new ConsoleRenderer();
            this.scoreboard = new Scoreboard();
        }

        public static GameEngine Instance
        {
            get { return GameEngine.instance; }
        }

        public void Run()
        {
            this.StartGame();
            this.secretNumber = RandomGenerator.GenerateRandomSecretNumber();

            while (true)
            {
                var turnMessage = ConfigReader.GetConfigString("Turn Message");
                renderer.PrintLineMessage(turnMessage);

                string input = this.ReadInput();

                switch (input)
                {
                    case "help":
                        {
                            var digits = this.ProceedHelpCommand(this.secretNumber, this.cheatNumber);
                            var message = ConfigReader.GetConfigString("Help Command Message");
                            this.renderer.PrintLineMessage(message, String.Join(String.Empty, digits));
                            this.cheatCounter += 1;
                            continue;
                        }
                        break;
                    case "restart":
                        {
                            this.PlayAgain();
                            return;
                        }
                    case "top":
                        {

                        }
                        break;
                    case "exit":
                        {
                            string goodbyeMessage = ConfigReader.GetConfigString("Goodbye Message");

                            this.renderer.PrintLineMessage(goodbyeMessage);
                            return;
                        }
                    default:
                        {
                            if (!this.CheckInput(input))
                            {
                                string errorMsg = ConfigReader.GetConfigString("Error Message");
                                renderer.PrintLineMessage(errorMsg);
                                continue;
                            }
                            this.CalculateBullsAndCows(this.secretNumber, input, ref this.bulls, ref this.cows);
                            if (this.secretNumber == input)
                            {
                                if (this.attempts > 0)
                                {
                                    //Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts and {1} cheats.", count1, count2);
                                    //Console.WriteLine("You are not allowed to enter the top scoreboard.");
                                    //SortAndPrintScoreBoard();
                                    //Console.WriteLine();
                                    PlayAgain();
                                }
                                else
                                {
                                    //Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts.", count1);
                                    //EnterScoreBoard(count1);
                                    PlayAgain();
                                }
                                continue;
                            }
                            this.attempts += 1;
                        }
                        break;
                }
            }
        }

        private void StartGame()
        {
            string welcomeMessage = ConfigReader.GetConfigString("Welcome Message");
            string helpMessage = ConfigReader.GetConfigString("Help Message");

            renderer.PrintLineMessage(welcomeMessage);
            renderer.PrintLineMessage(helpMessage);
        }

        private void CalculateBullsAndCows(string secretNumber, string guessNumber, ref int bulls, ref int cows)
        {
            var bullIndexes = new List<int>();
            var cowIndexes = new List<int>();
            for (int i = 0; i < secretNumber.Length; i++)
            {
                if (guessNumber[i].Equals(secretNumber[i]))
                {
                    bullIndexes.Add(i);
                    bulls += 1;
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

        private char[] RevealNumberAtRandomPosition(string secretnumber, char[] cheatNumber)
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
            }
        }

        private string ReadInput()
        {
            return Console.ReadLine();
        }

        private bool CheckInput(string input)
        {
            if (input.Length != NumberLength)
            {
                return false;
            }

            for (int i = 0; i < NumberLength; i++)
            {
                if (!Char.IsDigit(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private void PlayAgain()
        {
            this.secretNumber = null;
            this.bulls = default(int);
            this.cows = default(int);
            this.cheatCounter = default(int);
            this.cheatNumber = DefaultCheatNumber.ToArray();

            this.Run();
        }

        private char[] ProceedHelpCommand(string secret, char[] cheatNumber)
        {
            char[] revealedDigits = RevealNumberAtRandomPosition(secret, cheatNumber);
            StringBuilder revealedNumber = new StringBuilder();
            for (int i = 0; i < NumberLength; i++)
            {
                revealedNumber.Append(revealedDigits[i]);
            }

            return revealedDigits;
        }
    }
}
