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
        protected override void handleRequest(Message request)
        {
            Commands commands = new Commands();
            if (!(commands.CommandsList.Contains(request.Text)))
            {
                UserRelated userData = new SessionRelated().ReturnInfo(request.UserId);
                userData.Channel.SendMessage("Disculpa pero no te entiendo! :(");
                userData.Channel.SendMessage("Intenta escribir \"/comandos\" para verificar los comandos");
            }
        }
    }
}