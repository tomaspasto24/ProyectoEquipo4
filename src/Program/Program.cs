using System;

namespace Bot
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public static class Program
    {
        public static void Main()
        {
            ConsoleBot consoleBot = new ConsoleBot();
            consoleBot.StartCommunication();
        }
    }
}