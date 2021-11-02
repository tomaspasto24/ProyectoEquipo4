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
        public StartHandler(StartCondition condition) : base(condition) {}

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        protected override void HandleRequest(Message request)
        {
            Command commands = new Command();
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            userData.Channel.SendMessage(request.UserId, "¡Bienvenido al bot del equipo 4!");
            userData.Channel.SendMessage(request.UserId, "¿Qué desea hacer?:\n" + commands.ReturnCommands("Consola"));
            userData.Channel.SendMessage(request.UserId, "Si deseas salir, solo escribe Exit. Si quieres ver los comandos, escribe Comandos");
        }
    }
}