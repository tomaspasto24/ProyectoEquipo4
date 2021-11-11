namespace Bot
{
    /*
    Patrones y principios:
    Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP.
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    */
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
        }

        /// <summary>
        /// Metodo publico y abstracto para comenzar la comunicacion entre el usuario y el canal y el bot o la consola.
        /// </summary>
        public abstract void StartCommunication();

        /// <summary>
        /// Metodo para enviar el mensaje por el canal donde se esta comunicando
        /// </summary>
        /// <param name="id">id del usuario con el que dialoga el bot</param>
        /// <param name="text">mensaje que se quiere enviar al usuario</param>
        public abstract void SendMessage(int id, string text);
    }
}