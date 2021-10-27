namespace Bot
{
    /// <summary>
    /// Handler que se encarga del Login del usuario
    /// </summary>
    public class LoginHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de LoginHandler
        /// </summary>
        /// <param name="condition">LoginCondition para asignarlo al handler de la clase</param>
        /// <returns></returns>
        public LoginHandler(LoginCondition condition) : base(condition) { }

        /// <summary>
        /// HandleRequest 
        /// </summary>
        /// <param name="request"></param>
        protected override void HandleRequest(Message request)
        {               
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            userData.Channel.SendMessage(request.UserId, "Por favor, introduce tu usuario y tu contraseña");
        }
    }
}