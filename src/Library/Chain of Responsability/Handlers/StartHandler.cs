namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class StartHandler : AbstractHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>

        public StartHandler(StartCondition condition) : base(condition) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        protected override void HandleRequest(Message request)
        {
            Command commands = new Command();
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);
            userData.Channel.SendMessage(request.UserId, "¡Bienvenido al bot del equipo 4!");
            userData.Channel.SendMessage(request.UserId, "¿Qué desea hacer?:\n" + commands.ReturnCommands("Consola"));
            userData.Channel.SendMessage(request.UserId, "Si deseas salir, solo escribe Exit. Si quieres ver los comandos, escribe Comandos");
        }
    }
}