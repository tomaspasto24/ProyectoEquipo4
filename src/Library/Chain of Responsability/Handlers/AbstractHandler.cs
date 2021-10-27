namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        public AbstractHandler(ICondition condition)
        {
            this.Condition = condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AbstractHandler Succesor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public ICondition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public void Handler(Message request)
        {
            if (this.Condition.VerifyCondition(request))
            {
                // Delegar al handler
                this.HandleRequest(request);
                return;
            }
            if (this.Succesor != null)
            {
                // Delegar al siguiente handler
                this.Succesor.Handler(request);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void AddSuccesor(AbstractHandler handler)
        {
            // Agregar el sucesor
            this.Succesor = handler;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        protected abstract void HandleRequest(Message request);
    }
}