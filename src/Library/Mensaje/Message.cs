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
        public Message(int id, string message)
        {
            this.UserId = id;
            this.Text = message;
        }
        /// <summary>
        /// Id del usuario que esta charlando
        /// </summary>
        public int UserId {get; set;}

        /// <summary>
        /// Mensaje del usuario
        /// </summary>
        public string Text {get; set;}
    }
}