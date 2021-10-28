namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class NoCommandHandler : AbstractHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public NoCommandHandler(NoCommandCondition condition) : base(condition) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
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