namespace Bot
{
    /// <summary>
    /// Handler que se encarga del registro de un usuario empreas
    /// </summary>
    public class RegisterHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="condition">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public RegisterHandler(RegisterCondition condition) : base(condition) { }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        protected override void HandleRequest(Message request)
        {
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            userData.Channel.SendMessage(request.UserId, "Inserta tu token de usuario empresa: ");
            if (SessionRelated.DiccUserTokens.ContainsKey(request.Text))
            {
                userData.Channel.SendMessage(request.UserId, "Token verificado, ahora eres un usuario empresa! :)");
            }
            else
            {
                userData.Channel.SendMessage(request.UserId, "Disculpa, no hemos encontrado ese token :(");
            }
        }
    }
}