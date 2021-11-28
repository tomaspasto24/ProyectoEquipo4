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
    public class AddMaterialHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public AddMaterialHandler(AbstractHandler succesor) : base(succesor)
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
            //TODO Cambiar con un administrador de datos para el handler
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            string materialName = string.Empty;
            int materialQuantity = 0;
            int materialPrice = 0;
            Material material = null;
            string publicationTitle = string.Empty;
            
            if (!user.HasPermission(Permission.AddMaterial))
            {
                response = string.Empty;
                return false;
            }
            if (request.Text.Equals("/agregarmaterial") && (user.HandlerState == Bot.State.Start)) 
            {
                response = "Envía el título de la publicación en la que quieres agregar el material";
                user.HandlerState = Bot.State.AddingMaterial;
                return true; 
            }
            else if (user.HandlerState == Bot.State.AddingMaterial)
            {
                publicationTitle = request.Text;
                response = "Envía el nombre del material";
                user.HandlerState = Bot.State.AskingMaterialNameToAdd;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialNameToAdd)
            {
                materialName =request.Text;
                response = "Envía la cantidad del material";
                user.HandlerState = Bot.State.AskingMaterialQuantityToAdd;
                return true;
            }
            else if ((user.HandlerState == Bot.State.AskingMaterialQuantityToAdd))
            {
                materialQuantity = Int32.Parse(request.Text);
                response = "Envía el precio del material";
                user.HandlerState = Bot.State.AskingMaterialPriceToAdd;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialPriceToAdd)
            {
                materialPrice = Int32.Parse(request.Text);
                material = new Material(materialName, materialQuantity, materialPrice);
                foreach (Publication publication in PublicationSet.Instance.ListPublications)
                {
                    if (publication.Title == publicationTitle)
                    {
                        publication.AddMaterial(material);
                    }
                }
                response = "Se ha agregado el material a la publicación. Si quieres agregar otro material envía \"/agregarmaterial\".\nEnvía \"/cancelar\" para cancelar la operación.";
                user.HandlerState = Bot.State.Start;
                return true;
            }
            response = string.Empty;
            return false;
        }
    }
}