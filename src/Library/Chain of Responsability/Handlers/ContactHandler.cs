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
    public class ContactHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public ContactHandler(AbstractHandler succesor) : base(succesor)
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

            if (!user.HasPermission(Permission.ContactCompany))
            {
                response = string.Empty;
                return false;
            }
            
            if (request.Text.Equals("/contacto") && user.HandlerState == Bot.State.Start)
            {
                user.HandlerState = Bot.State.AskingCompanyName;
                response = "Por favor dinos con que empresa te quieres contactar. \nEnvia \"/cancelar\" para cancelar la operación.";
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingCompanyName)
            {
                Company company = SessionRelated.Instance.GetCompanyByName(request.Text);
                if (company != null)
                {
                    response = $"{company.ReturnContact()}";
                    user.HandlerState = Bot.State.Start;
                    return true;
                }
                else
                {
                    response = "Disculpa, no encontramos esa Empresa";
                    user.HandlerState = Bot.State.Start;
                    return true;
                }
            }
            
            response = string.Empty;
            return false;
        }
    }
}