namespace BullsAndCows.Utils
{
    using System;
    using System.Text;

    public static class RandomGenerator
    {
        public static string GenerateRandomSecretNumber()
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
    }
}
