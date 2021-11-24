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
    public class SearchByLocationHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public SearchByLocationHandler(AbstractHandler succesor) : base(succesor)
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
            EntrepreneurInfo entrepreneurInfo = SessionRelated.Instance.GetEntrepreneurInfoByUserInfo(user);

            if (request.Text.Equals("/porubicacion") && (user.HandlerState == Bot.State.Searching)) 
            {
                response = "Ingresa la direccion por la que quieres buscar.\nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.SearchingByLocation;
                return true;
            }
            else if (user.HandlerState == Bot.State.SearchingByLocation)
            {
                response = entrepreneurInfo.SearchingByLocation(request.Text) + "\nSi te interesa alguna publicación: envía el \"Título\" " ;
                user.HandlerState = Bot.State.InterestedInPublication;
                return true;
            }
            else if ((user.HandlerState == Bot.State.InterestedInPublication))
            {
                foreach (Publication publication in PublicationSet.Instance.ListPublications)
                {
                    if (publication.Title == request.Text)
                    {   
                        PublicationSet.Instance.DeleteElement(publication);
                        response = "Si se quiere contactar con la empresa envíe \"/contacto\" \nEnvia \"/cancelar\" para cancelar la operación";
                        user.HandlerState = Bot.State.AskingCompanyName;
                        return true;
                    }
                }
                response = "No existe una publicación con el título que ingresó, intente nuevamente";
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}