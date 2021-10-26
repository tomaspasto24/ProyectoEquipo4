namespace Bot
{
    /// <summary>
    /// Interfaz IBot que contiene los metodos escenciales para los Bots.
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// StartCommunication es el metodo que se encarga de comenzar la charla entre el usuario y el bot concreto o consola.
        /// </summary>
        void StartCommunication();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        void SendMessage(string id, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        void HandleMessage(Message text);
    }
}