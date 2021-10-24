namespace Bot
{
    public abstract class AbstractBot : IBot
    {
        protected AbstractBot()
        {
            this.handler = Setup.HandlerSetup();
        }
        public abstract void StartCommunication();
        public abstract void SendMessage(string id, string text);
        public void HandleMessage(Message text)
        {
            handler.Handler(text);
        }
        private AbstractHandler handler;
    }
}