namespace Bot
{
    /// <summary>
    /// Handler para mostrar los comandos que el usuario tiene acceso
    /// </summary>
    public class CommandHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase CommandHandler
        /// </summary>
        /// <param name="condition">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public CommandHandler(AbstractHandler succesor) : base(succesor) { }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            Command commands = new Command();
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);

            if (request.Text.Equals("/comandos"))
            {
                response = $"Estos son todos los comandos: \n{commands.ReturnCommands(request.UserId)}";
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}