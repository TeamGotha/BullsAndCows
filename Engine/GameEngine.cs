﻿using System.Linq;

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
                this.bulls = 0;
                this.cows = 0;
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
                        PrintScoreboard();
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
//                                string errorMsg = ConfigReader.GetConfigString("Error Message");
//                                renderer.PrintLineMessage(errorMsg);
                                throw new ArgumentException("Incorrect guess or command!");
                            }
                            this.CalculateBullsAndCows(this.secretNumber, input);
                            this.attempts++;
                            if (this.secretNumber == input)
                            {
                                if (this.cheatCounter > 0)
                                {
                                    var message = ConfigReader.GetConfigString("Win Message");
                                    renderer.PrintLineMessage(message, this.attempts, this.cheatCounter);
                                    var secondMessage = ConfigReader.GetConfigString("Cheater Leaderboard Message");
                                    renderer.PrintLineMessage(secondMessage);
                                    PrintScoreboard();
                                    //Console.WriteLine("You are not allowed to enter the top scoreboard.");
                                    //SortAndPrintScoreBoard();
                                    //Console.WriteLine();
                                    PlayAgain();
                                }
                                else
                                {
                                    var message = ConfigReader.GetConfigString("Win Message");
                                    renderer.PrintLineMessage(message, this.attempts, this.cheatCounter);
                                    var secondMessage = ConfigReader.GetConfigString("Enter Name Message");
                                    renderer.PrintLineMessage(secondMessage);
                                    var name = this.ReadInput();
                                    scoreboard.AddScore(name, this.attempts);
                                    PrintScoreboard();
                                    PlayAgain();
                                }
                                
                            }
                            else
                            {
                                var message = ConfigReader.GetConfigString("Cowns and Bulls Message");
                                renderer.PrintLineMessage(message, this.bulls, this.cows);
                            }
                        }
                        break;
                }
            }
        }

        private void PrintScoreboard()
        {
            var results = scoreboard.Results;
            renderer.PrintHighscore(results);
        }

        private void StartGame()
        {
            string welcomeMessage = ConfigReader.GetConfigString("Welcome Message");
            string helpMessage = ConfigReader.GetConfigString("Help Message");

            renderer.PrintLineMessage(welcomeMessage);
            renderer.PrintLineMessage(helpMessage);
        }

        public int[] CalculateBullsAndCows(string secretNumber, string guessNumber)
        {
            var bullIndexes = new List<int>();
            var cowIndexes = new List<int>();
            for (int i = 0; i < secretNumber.Length; i++)
            {
                if (guessNumber[i].Equals(secretNumber[i]))
                {
                    bullIndexes.Add(i);
                    this.bulls ++;
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
                            this.cows++;
                            break;
                        }
                    }
                }
            }
            return new int[]{this.bulls, this.cows};
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
