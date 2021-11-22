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
    public class PublishHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public PublishHandler(AbstractHandler succesor) : base(succesor)
        {
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            // TODO
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            
            if (!user.UserRole.HasPermission(Permission.Publish))
            {
                response = string.Empty;
                return false;
            }
            if (request.Text.Equals("/publicar") && (user.HandlerState == Bot.State.Start)) 
            {
                response = "Envía el título de la nueva publicación \nEnvía \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.AskingPublicationName;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingPublicationName)
            {
                string título = request.Text;
                response = "Envía el nombre empresa \nEnvía \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.AskingCompanyName;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingCompanyName)
            {
                Company publishingCompany = new Company();
                foreach (Company company in CompanySet.Instance.ListCompanies)
                {
                    if (company.Name == request.Text)
                    {
                        publishingCompany = company;
                    }
                }
                response = "Envía la ubicación de la empresa \nEnvía \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.AskingCompanyLocation;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingCompanyLocation)
            {
                GeoLocation locationCompany = new GeoLocation(request.Text, "Montevideo");
                response = "Envía el nombre del material";
                user.HandlerState = Bot.State.AskingMaterialName;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialName)
            {
                string materialName =request.Text;
                response = "Envía la cantidad del material";
                user.HandlerState = Bot.State.AskingMaterialQuantity;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialQuantity)
            {
                //int materialQuantity = request;
                response = "Envía el precio del material";
                user.HandlerState = Bot.State.AskingMaterialPrice;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialPrice)
            {
                //int materialQuantity = request;
                response = "";
                //user.HandlerState = Start;
                //TODO crear publicacion con datos adquiridos
            }


            response = string.Empty;
            return false;
        }
    }
}