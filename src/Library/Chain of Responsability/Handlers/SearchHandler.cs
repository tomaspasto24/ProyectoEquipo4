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
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public SearchHandler(AbstractHandler succesor) : base(succesor)
        {
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            
            if (!user.UserRole.HasPermission(Permission.Search))
            {   
                response = string.Empty;
                return false;
            }

            if (request.Text.Equals("/busqueda") && (user.HandlerState == Bot.State.Start))
            {
                response = "Por favor dinos por qué quieres buscar. \n Envía \"/pormaterial\" para buscar por material. \nEnvia \"/porubicacion\" para buscar por ubicación. \nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.Searching;
                return true;
            }
            
            response = string.Empty;
            return false;
        }
    }
}