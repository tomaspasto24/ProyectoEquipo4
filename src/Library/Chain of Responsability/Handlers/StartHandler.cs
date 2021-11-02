namespace Bot
{
    /// <summary>
    /// Handler para saludar al usuario
    /// </summary>
    public class StartHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase StartHandler
        /// </summary>
        /// <param name="condition">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public StartHandler(AbstractHandler succesor) : base(succesor) { }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            Command commands = new Command();
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            
            if (request.Text.ToLower().Equals("/hola"))
            {
                response = "¡Bienvenido al bot del equipo 4! \n ¿Qué desea hacer?:\n" 
                            + commands.ReturnCommands(123) +
                            "\n Si deseas salir, solo escribe Exit. Si quieres ver los comandos, escribe Comandos";
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}