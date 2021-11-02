namespace Bot
{
    /// <summary>
    /// Condición que deberá ser cumplida para que el handler se ejecute.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Valida si el mensaje recibido fue el comando esperado por la condicion.
        /// </summary>
        /// <param name="condition">Mensaje que contiene el texto y el id del usuario</param>
        /// <returns>True o false dependiendo si el texto es igual o no</returns>
        bool VerifyCondition(Message condition);
    }
}