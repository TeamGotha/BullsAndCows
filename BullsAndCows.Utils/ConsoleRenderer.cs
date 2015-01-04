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
    }
}
