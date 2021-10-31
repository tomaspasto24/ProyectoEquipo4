namespace Bot
{
    /// <summary>
    /// Handler que se encarga de solicitar el código generado de invitación para registrar el usuario
    /// Empresa.
    /// </summary>
    public class RegisterUserCompanyHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de RegisterUserCompanyHandler.
        /// </summary>
        /// <param name="condition">RegisterUserCompanyCondition para asignarlo al handler de la clase.</param>
        /// <returns></returns>
        public RegisterUserCompanyHandler(RegisterUserCompanyCondition condition) : base(condition) { }

        /// <summary>
        /// HandleRequest 
        /// </summary>
        /// <param name="request"></param>
        protected override void HandleRequest(Message request)
        {               
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            userData.Channel.SendMessage(request.UserId, "Por favor, introducir código generado a continuación:");
            
        }
    }
}