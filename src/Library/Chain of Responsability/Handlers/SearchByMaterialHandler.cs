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
    public class SearchByMaterialHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public SearchByMaterialHandler(AbstractHandler succesor) : base(succesor)
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

            if (request.Text.Equals("/pormaterial") && (user.HandlerState == Bot.State.Searching))
            {
                response = "Ingresa el nombre del material que quieres buscar.\nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.SearchingByMaterial;
                return true;
            }
            else if (user.HandlerState == Bot.State.SearchingByMaterial)
            {
                if (SearchByMaterial.Instance.Search(request.Text).Equals(string.Empty))
                {
                    response = $"No hay publicaciones que contengan {request.Text}. \nIntenta con un material distinto o en caso de que quieras salir: \"/cancelar\"";
                    return true;
                }
                else
                {
                    response = $"Estas son las publicaciones que contienen {request.Text}\n" + SearchByMaterial.Instance.Search(request.Text) + "\nSi te interesa alguna publicación envia el titulo, en caso contrario \"/cancelar\"";
                    user.HandlerState = Bot.State.InterestedInPublication;
                    return true;
                }
            }
            else if ((user.HandlerState == Bot.State.InterestedInPublication))
            {

                foreach (Publication publication in PublicationSet.Instance.ListPublications)
                {
                    if (publication.Title == request.Text)
                    {
                        publication.SetInterestedPerson(entrepreneurInfo);
                        publication.ClosePublication();
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