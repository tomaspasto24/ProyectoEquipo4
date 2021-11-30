using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de realizar una búsqueda de publicaciones con el filtro de material
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class SearchByMaterialHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public SearchByMaterialHandler(AbstractHandler succesor) : base(succesor)
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
                    response = $"Estas son las publicaciones que contienen \"{request.Text}\"\n" + SearchByMaterial.Instance.Search(request.Text) + "\nSi te interesa alguna publicación envia el titulo, en caso contrario: /cancelar";
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
                        response = "Si se quiere contactar con la empresa envíe su nombre. \nEnvia \"/cancelar\" para cancelar la operación";
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