namespace Bot
{
    public abstract class AbstractHandler
    {
        public AbstractHandler(ICondition condition)
        {
            this.Condition = condition;
        }
        public AbstractHandler Succesor { get; set; }
        public ICondition Condition { get; set; }
        public void Handler(Message request)
        {
            if (this.Condition.VerifyCondition(request))
            {
                // Delegar al handler
            }
            if (this.Succesor != null)
            {
                // Delegar al siguiente
                this.Succesor.Handler(request);
            }
        }
        public void AddSuccesor(AbstractHandler handler)
        {
            // Agregar el sucesor
            this.Succesor = handler;
        }
        protected abstract void handleRequest(Message request);
    }
}