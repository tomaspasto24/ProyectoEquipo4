using System;

namespace Bot
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Programa principal que inicia la comunicacion con los bots.
        /// </summary>
        static void Main(string[] args)
        {
            ConsoleBot.Instance.StartCommunication();
        }
    }
}