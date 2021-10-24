namespace Bot
{
    public class StartHandler : AbstractHandler
    {
        public StartHandler(StartCondition condition) : base(condition) {}
        protected override void handleRequest(Message request)
        {
            Commands commands = new Commands();
            System.Console.WriteLine("¡Bienvenido al bot del equipo 4!");
            System.Console.WriteLine("¿Qué desea hacer?:\n" + commands.CommandsList);
            System.Console.WriteLine("Si deseas salir, solo escribe Exit. Si quieres ver los comandos, escribe Comandos");
        }
    }
}