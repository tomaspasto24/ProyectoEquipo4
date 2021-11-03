using System;
namespace Bot
{

    /// <summary>
    /// Bot concreto de consola que hereda de AbstractBot
    /// </summary>
    public class ConsoleBot : AbstractBot
    {
        private static ConsoleBot instance;
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
        /// Constructor de ConsoleBot que utiliza el constructor de AbstractBot
        /// </summary>
        /// <returns></returns>
        private ConsoleBot() : base() { }

        /// <summary>
        /// Manda un mensaje, en este caso, por consola.
        /// </summary>
        /// <param name="id">Id del usuario con el que dialoga el bot</param>
        /// <param name="text">Mensaje que se quiere enviar al usuario</param>
        public override void SendMessage(int id, string text)
        {
            System.Console.WriteLine(text);
        }

        /// <summary>
        /// Metodo StartCommunication, publico que hace override del metodo declarado en AbstractBot
        /// Es el metodo que da comienzo a la conversacion entre el usuario y el bot concreto o consola.
        /// </summary>
        public override void StartCommunication()
        {
            IHandler handler =
                new StartHandler(
                new RegisterHandler(
                new CommandHandler(null)
            ));

            Message message = new Message(1111, string.Empty);
            string response;

            this.SendMessage(123, "Bienvenido al bot de consola! Puedes usar \"exit\" para terminar la conversacion.");
            while (true)
            {
                message.Text = Console.ReadLine();
                if (message.Text.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
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