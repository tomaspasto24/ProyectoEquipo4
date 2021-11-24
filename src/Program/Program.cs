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
            SerializeManager.Instance.DeserializeObjects(); // Deserializa
            TelegramBot botardo = TelegramBot.Instance;

            botardo.StartCommunication();

            Console.WriteLine($"Bot is up!");

            // Esperamos a que el usuario aprete Enter en la consola para terminar el bot.
            Console.ReadLine();

            // Prueba Publications.
            // GeoLocation location = new GeoLocation("Universidad Católica", "Montevideo");
            // Company companyTest = new Company("Prueba1", "Prueba", location, "0922877272");
            // Material material = new Material("MaterialTest", 12, 0);
            // Publication publicationTest1 = new Publication("Prueba1", companyTest, location, material);
            // publicationTest1.AddMaterial(new Material("Material2", 20, 2020));
            // PublicationSet.Instance.AddElement(publicationTest1);
            // System.Console.WriteLine("Publicacion:");
            foreach(Publication item in PublicationSet.Instance.ListPublications)
            {
                System.Console.WriteLine(item.Title);
            }

            // //Prueba Publications.


            // // Prueba Session Related.
            // // GeoLocation location = new GeoLocation("Universidad Católica", "Montevideo");
            // // Company companyTest = new Company("Prueba1", "Prueba", location, "0922877272");
            // companyTest.AddOwnPublication(new Publication("PruebaEMPRESA5", companyTest, location, material));
            // companyTest.AddOwnPublication(new Publication("PruebaEMPRESA6", companyTest, location, material));
            // SessionRelated.Instance.DiccUserTokens.Add(TokenGenerator.Instance.GenerateToken().ToString(), companyTest);
            System.Console.WriteLine("Empresa:");
            foreach(Company item in SessionRelated.Instance.DiccUserTokens.Values)
            {
                System.Console.WriteLine(item.Name);
            }
            //Prueba Session Related.
            SerializeManager.Instance.SerializeObjects(); // Serialización.
        }
    }
}

