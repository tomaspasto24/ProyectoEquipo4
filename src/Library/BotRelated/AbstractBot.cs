namespace Bot
{
    /// <summary>
    /// Bot abstracto del cual heredarán los bots concretos.
    /// </summary>
    public abstract class AbstractBot
    {
        private AbstractHandler handler;
        
        /// <summary>
        /// Constructor de la clase AbstractBot
        /// </summary>
        protected AbstractBot()
        {
            this.handler = Setup.HandlerSetup();
        }

        /// <summary>
        /// Metodo publico y abstracto para comenzar la comunicacion entre el usuario y el canal y el bot o la consola.
        /// </summary>
        public abstract void StartCommunication();

        /// <summary>
        /// Metodo publico y abstracto para setear el canal de comunicacion entre el usuario y el bot.
        /// </summary>
        public void ChangeChannel(int id, AbstractBot channel)
        {
            SessionRelated.Instance.SetChatChannel(id, channel);
        }

        /// <summary>
        /// Metodo para enviar el mensaje por el canal donde se esta comunicando
        /// </summary>
        /// <param name="id">id del usuario con el que dialoga el bot</param>
        /// <param name="text">mensaje que se quiere enviar al usuario</param>
        public abstract void SendMessage(int id, string text);

        /// <summary>
        /// Metodo para delegar el mensaje recibido.
        /// </summary>
        /// <param name="text">text es el mensaje recibido, de tipo Message. Ademas del mensaje contiene el User Id del usuario</param>
        public void HandleMessage(Message text)
        {
            handler.Handle(text);
        }
    }
}