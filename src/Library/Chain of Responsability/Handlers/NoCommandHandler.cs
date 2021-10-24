namespace Bot
{
    public class NoCommandHandler : AbstractHandler
    {
        public NoCommandHandler(NoCommandCondition condition) : base(condition) {}
        protected override void handleRequest(Message request)
        {
            Commands commands = new Commands();
            if (!(commands.CommandsList.Contains(request.Text)))
            {
                System.Console.WriteLine("Disculpa pero no te entiendo! :(");
                System.Console.WriteLine("Intenta escribir \"Comandos\" para verificar los comandos");
            }
        }
    }
}