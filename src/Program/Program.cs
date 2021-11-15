using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Library;

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
        public static void Main()
        {
            // TODO HACER LOS COMENTARIOS DE TODO
            // ConsoleBot.Instance.StartCommunication(); // iniciar comunicación por consola

            TelegramBot botardo = TelegramBot.Instance;

            botardo.StartCommunication();

            Console.WriteLine($"Bot is up!");

            // Esperamos a que el usuario aprete Enter en la consola para terminar el bot.
            Console.ReadLine();
        }
    }
}
