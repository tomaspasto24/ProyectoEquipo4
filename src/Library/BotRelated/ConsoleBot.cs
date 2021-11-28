using System;
namespace Bot
{
    /// <summary>
    /// Implementacion de AbstractBot que utiliza la consola como interfaz de comunicacion
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa los métodos polimórficos StartCommunication y SendMessage.
    /// Cumple con el patrón Singleton, esto lo que hace es que, garantiza que haya una única instancia de la clase y de esta forma se obtiene
    /// un acceso global a esta instancia.
    /// </summary>
    public class ConsoleBot : AbstractBot
    {
        private static ConsoleBot instance;

        /// <summary>
        /// Obtiene la única instancia de esta clase.
        /// </summary>
        /// <value>La única instancia de esta clase.</value>
        public static ConsoleBot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConsoleBot();
                }
                return instance;
            }
        }
        /// <summary>
        /// Constructor por defecto, privado para facilitar la implementación del patron Singleton.
        /// </summary>
        private ConsoleBot() : base() { }

        /// <summary>
        /// Envia un mensaje al usuario con el bot como emisor.
        /// </summary>
        /// <param name="id">Id del usuario destinatario</param>
        /// <param name="text">Mensaje a enviar</param>
        public override void SendMessage(long id, string text)
        {
            System.Console.WriteLine(text);
        }

        /// <summary>
        /// Comienza la comunicacion entre el bot y los usuarios.
        /// </summary>
        public override void StartCommunication()
        {
            IHandler handler =
                new CommandHandler(
                    new ContactHandler(
                        new UndertakeHandler(
                            new PublishHandler(
                                new RegisterHandler(
                                    new SalesReportHandler(
                                        new SearchHandler(
                                            new StartHandler(
                                                new TokenHandler(
                                                    new UserInformationHandler(null)
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                );

            Message message = new Message(1111, string.Empty);
            string response;

            this.SendMessage(123, "Bienvenido al bot de consola! Puedes usar \"/exit\" para terminar la conversacion.");
            while (true)
            {
                message.Text = Console.ReadLine();
                if (message.Text.Equals("/exit", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Salimos");
                    break;
                }

                IHandler result = handler.Handle(message, out response);

                if (result == null)
                {
                    Console.WriteLine("Disculpa no te entiendo :(");
                    Console.WriteLine("Intenta escribir \"/comandos\" para verificar los comandos");
                }
                else
                {
                    Console.WriteLine(response);
                }
            }
        }
    }
}