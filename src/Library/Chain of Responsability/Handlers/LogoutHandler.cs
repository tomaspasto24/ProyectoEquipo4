namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class LogoutHandler : AbstractHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public LogoutHandler(LogoutCondition condition) : base(condition){}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        protected override void HandleRequest(Message request)
        {
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            userData.Channel.SendMessage(request.UserId, "Logout :)");
        }
    }
}