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
        protected override void HandleRequest(Message request)
        {
            Command commands = new Command();
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            userData.Channel.SendMessage(request.UserId, $"Estos son todos los comandos: \n{commands.ReturnCommands(request.UserId)}");
        }
    }
}