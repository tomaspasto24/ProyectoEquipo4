namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterHandler : AbstractHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public RegisterHandler(RegisterCondition condition) : base(condition) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        protected override void HandleRequest(Message request)
        {
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            userData.Channel.SendMessage(request.UserId, "Inserta tu token de usuario empresa: ");
            if (SessionRelated.DiccUserRelated.ContainsKey(request.Text))
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