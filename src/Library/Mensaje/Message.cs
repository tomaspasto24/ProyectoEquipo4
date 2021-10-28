namespace Bot
{
    /// <summary>
    /// Clase Message que guarda el mensaje enviado por el usuario y además, su id.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Constructor de la clase Message
        /// </summary>
        /// <param name="id">Id del usuario que envía el mensaje</param>
        /// <param name="message">Contenido del mensaje</param>
        public Message(string id, string message)
        {
            this.UserId = id;
            this.Text = message;
        }
        public string UserId {get; set;}
        public string Text {get; set;}
    }
}