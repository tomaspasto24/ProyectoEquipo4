namespace Bot
{
    /// <summary>
    /// Handler para mostrar un mensaje en caso de que no se ejecute ninguno de los handlers anteriores.
    /// Es el ultimo handler de la cadena
    /// </summary>
    public class NoCommandHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase NoCommandHandler
        /// </summary>
        /// <param name="condition">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public NoCommandHandler(AbstractHandler succesor) : base(succesor) { }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        protected override void HandleRequest(Message request)
        {
            Command commands = new Command();
            if (!(commands.CommandsList.Contains(request.Text)))
            {
                UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
                userData.Channel.SendMessage(request.UserId, "Disculpa pero no te entiendo! :(");
                userData.Channel.SendMessage(request.UserId, "Intenta escribir \"/comandos\" para verificar los comandos");
            }
        }
    }
}