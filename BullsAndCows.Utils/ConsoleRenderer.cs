using System.Collections.Generic;

namespace BullsAndCows.Utils
{
    using System;

    public class ConsoleRenderer : Renderer
    {
        public override void PrintMessage(string message)
        {
            Console.Write(message);
        }

        public override void PrintLineMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintLineMessage(string format, params object[] args)
        {
            var message = String.Format(format, args);

            Console.WriteLine(message);
        }

        public void PrintHighscore(Dictionary<string, int> scores)
        {
            int counter = 0;
            foreach (KeyValuePair<string, int> player in scores)
            {
                counter++;
                Console.WriteLine("{0}. {1} --> {2} guesses", counter, player.Key, player.Value);
            }
        }
    }
}
