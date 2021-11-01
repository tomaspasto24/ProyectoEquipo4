namespace Bot
{
    /// <summary>
    /// Clase Setup que contiene el setup de los handlers y sus sucesores, y además, sus respectivas conditions.
    /// </summary>
    public class Setup
    {
        /// <summary>
        /// Método publico para hacer la configuración de los handlers y conditions.
        /// </summary>
        /// <returns>Retorna el primer handler de la cadena de la cadena de handlers.</returns>
        public static AbstractHandler HandlerSetup()
        {
            AbstractHandler start = new StartHandler(new StartCondition());
            AbstractHandler register = new RegisterHandler(new RegisterCondition());
            AbstractHandler commandList = new CommandHandler(new CommandCondition());
            AbstractHandler noCommand = new NoCommandHandler(new NoCommandCondition());

            start.Succesor = register;
            register.Succesor = commandList;
            commandList.Succesor = noCommand;
            
            return start;
        }
    }
}