namespace Bot
{
    /// <summary>
    /// Tipo enumerado que define el estado de la conversacion con el bot.
    /// </summary>
    public enum State
    {
        Start,
        Chatting,
        AttendingRequest
    }

    /// <summary>
    /// Clase UserRelated que contiene informacion acerca del usuario.
    /// </summary>
    public class UserRelated
    {
        public State State { get; set; }
        public AbstractBot Channel { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Constructor de la clase UserRelated.
        /// </summary>
        public UserRelated()
        {
            this.State = State.Start;
            this.Channel = null;
            this.User = null;
        }
    }
}