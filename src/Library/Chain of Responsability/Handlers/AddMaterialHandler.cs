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
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public AddMaterialHandler(AbstractHandler succesor) : base(succesor)
        {
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            //TODO Cambiar con un administrador de datos para el handler
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            string materialName = string.Empty;
            int materialQuantity = 0;
            int materialPrice = 0;
            Material material = null;
            string publicationTitle = string.Empty;
            
            if (!user.UserRole.HasPermission(Permission.AddMaterial))
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