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
            SessionRelated.Instance.LoadFromJson(); // Deserializar session related.
            PublicationSet.Instance.LoadFromJson(); // Deserializar lista publicaciones.

            TelegramBot botardo = TelegramBot.Instance;

            botardo.StartCommunication();

            Console.WriteLine($"Bot is up!");

            // Esperamos a que el usuario aprete Enter en la consola para terminar el bot.
            Console.ReadLine();

            //Prueba Publications.
            // GeoLocation location = new GeoLocation("Universidad Católica", "Montevideo");
            // Company companyTest = new Company("Prueba1", "Prueba", location, "0922877272");
            // Material material = new Material("MaterialTest", 12, 0);
            // Publication publicationTest1 = new Publication("Prueba1", companyTest, location, material);
            // PublicationSet.Instance.AddElement(publicationTest1);

            // foreach(Publication item in PublicationSet.Instance.ListPublications)
            // {
            //     System.Console.WriteLine(item.Title);
            // }

            //Prueba Publications.

            //Prueba Session Related.
            // GeoLocation location = new GeoLocation("Universidad Católica", "Montevideo");
            // Company companyTest = new Company("Prueba1", "Prueba", location, "0922877272");
            // SessionRelated.Instance.DiccUserTokens.Add(TokenGenerator.Instance.GenerateToken().ToString(), companyTest);

            // foreach(Company item in SessionRelated.Instance.DiccUserTokens.Values)
            // {
            //     System.Console.WriteLine(item.Name);
            // }
            //Prueba Session Related.


            PublicationSet.Instance.ConvertToJson(); // Serializar lista publicaciones.
            SessionRelated.Instance.ConvertToJson(); // Serializar session related.
        }
    }
}

