namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandHandler : AbstractHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public CommandHandler(CommandCondition condition) : base(condition) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        protected override void handleRequest(Message request)
        {
            Commands commands = new Commands();
            UserRelated userData = new SessionRelated().ReturnInfo(request.UserId);
            userData.Channel.SendMessage($"{commands.ReturnCommands(request.UserId)}");
        }
    }
}