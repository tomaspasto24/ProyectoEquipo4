using System;

namespace Bot
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Programa principal que inicia la comunicacion con los bots.
        /// </summary>
        public static void Main()
        {
            ConsoleBot consoleBot = new ConsoleBot();
            consoleBot.StartCommunication();
        }
    }
}