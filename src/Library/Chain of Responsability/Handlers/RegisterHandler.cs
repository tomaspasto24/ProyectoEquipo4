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
        protected override void handleRequest(Message request)
        {
            UserRelated userData = new SessionRelated().ReturnInfo(request.UserId);
            userData.Channel.SendMessage("Register :)");
        }
    }
}