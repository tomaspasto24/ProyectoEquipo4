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
            userData.Channel.SendMessage(request.UserId, "¿Deseas registrarte como emprendedor o como un usuario de empresa? 1 / 2");
        }
    }
}