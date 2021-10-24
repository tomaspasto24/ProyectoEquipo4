namespace Bot
{
    public class ConsoleBot : AbstractBot
    {
        public ConsoleBot() : base(){}
        public override void SendMessage(string id, string text)
        {
            System.Console.WriteLine(text);
        }
        public override void StartCommunication()
        {
            System.Console.WriteLine($"Bienvenido al bot de consola! Puedes usar \"Exit\" para terminar la conversacion.");
            while (true)
            {
                string text = System.Console.ReadLine().ToString();
                if (text == "Exit")
                {
                    break;
                }
                Message message = new Message("Consola", text);
                HandleMessage(message);
            }
        }
    }
}