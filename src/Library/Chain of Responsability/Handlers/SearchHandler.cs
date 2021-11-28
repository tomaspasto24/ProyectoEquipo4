using System;
using System.Collections.Generic;

namespace Bot
{
    /*
    Patrones y principios:
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.    
    A su vez, cumple con el patrón Chain of Responsability.
    */
    /// <summary>
    /// Handler que se encarga del registro de un usuario
    /// </summary>
    public class SearchHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public SearchHandler(AbstractHandler succesor) : base(succesor)
        {
        }

        /// <summary>
        /// Intenta procesar el mensaje recibido y devuelve una respuesta.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario</returns>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            
            if (!user.HasPermission(Permission.Search))
            {   
                response = string.Empty;
                return false;
            }

            if (request.Text.Equals("/busqueda") && (user.HandlerState == Bot.State.Start))
            {
                response = "Por favor dinos el metodo de busqueda que quieres usar. \nEnvía \"/pormaterial\" para buscar por material. \nEnvia \"/porubicacion\" para buscar por ubicación. \nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.Searching;
                return true;
            }
            
            response = string.Empty;
            return false;
        }
    }
}