namespace Bot
{
    /// <summary>
    /// Clase abstracta para los distintos bots.
    /// </summary>
    public abstract class AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase AbstractHandlers
        /// </summary>
        /// <param name="condition">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public AbstractHandler(ICondition condition)
        {
            this.Condition = condition;
        }

        /// <summary>
        /// Handler sucesor
        /// </summary>
        /// <value></value>
        public AbstractHandler Succesor { get; set; }

        /// <summary>
        /// Condicion que se tiene que cumplir para que se ejecute el handler
        /// </summary>
        /// <value></value>
        public ICondition Condition { get; set; }

        /// <summary>
        /// Metodo para manejar las peticiones. Si se cumple la condicion, se ejecuta el handler asociado. Sino lo delega a su sucesor.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        public void Handler(Message request)
        {
            if (this.Condition.VerifyCondition(request))
            {
                // Ejecuta el handler
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
        /// Metodo para agregar un sucesor
        /// </summary>
        /// <param name="handler">Sucesor del handler actual</param>
        public void AddSuccesor(AbstractHandler handler)
        {
            // Agregar el sucesor
            this.Succesor = handler;
        }
        
        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        protected abstract void HandleRequest(Message request);
    }
}