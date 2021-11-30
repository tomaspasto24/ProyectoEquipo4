using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de mostrar la lista de comandos, segun los permisos que tenga el usuario
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class CommandHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public CommandHandler(AbstractHandler succesor) 
        : base(succesor) { }

        /// <summary>
        /// Intenta procesar el mensaje recibido y devuelve una respuesta.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message request, out string response)
        {
            if (request.Text.Equals("/comandos"))
            {
                response = $"Estos son todos los comandos: \n" + Command.GetCommands(request.UserId);
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}