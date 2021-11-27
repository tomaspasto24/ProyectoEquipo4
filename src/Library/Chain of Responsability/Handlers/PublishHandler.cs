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
        private Dictionary<UserInfo, PublishData> publishData = new Dictionary<UserInfo, PublishData>();
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
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            
            
            if (!user.HasPermission(Permission.Publish))
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
                this.publishData.Remove(user);
                this.publishData.Add(user, new PublishData(request.Text));
                response = "Envía el nombre empresa \nEnvía \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.AskingCompanyName;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingCompanyName)
            {
                PublishData pd = this.publishData[user];
                string companyName = request.Text;
                pd.PublishingCompany = SessionRelated.Instance.GetCompanyByName(request.Text);
                response = "Envía la ubicación de la empresa \nEnvía \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.AskingCompanyLocation;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingCompanyLocation)
            {
                PublishData pd = this.publishData[user];
                pd.LocationCompany = new GeoLocation(request.Text, "Montevideo");    
                response = "Envía el nombre del material";
                user.HandlerState = Bot.State.AskingMaterialName;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialName)
            {
                PublishData pd = this.publishData[user];
                pd.MaterialName =request.Text;
                response = "Envía la cantidad del material";
                user.HandlerState = Bot.State.AskingMaterialQuantity;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialQuantity)
            {
                PublishData pd = this.publishData[user];
                pd.MaterialQuantity = Int32.Parse(request.Text);
                response = "Envía el precio del material";
                user.HandlerState = Bot.State.AskingMaterialPrice;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialPrice)
            {

                materialPrice = Int32.Parse(request.Text);
                material = new Material(materialName, materialQuantity, materialPrice);
                Publication publication = new Publication(title, publishingCompany, locationCompany, material);
                response = "Se ha creado la publicación con el material. Si quieres agregar otro material envía \"/agregarmaterial\". \n Envíe \"/cancelar\" si quiere terminar la publicación.";

                PublishData pd = this.publishData[user];
                pd.MaterialPrice = Int32.Parse(request.Text);
                pd.Material = new Material(pd.MaterialName, pd.MaterialQuantity, pd.MaterialPrice);
                Publication publication = new Publication(pd.Title, pd.PublishingCompany, pd.LocationCompany, pd.Material);
                PublicationSet.Instance.AddElement(publication);
                response = "Se ha creado la publicación con el material indicado. Si quieres agregar otro material envía \"/agregarmaterial\". \n Envíe \"cancelar\" si quiere terminar la publicación.";
                user.HandlerState = Bot.State.Start;
                return true;
            }
            response = string.Empty;
            return false;
        }
        class PublishData
        {
            public string MaterialName { get; set; }
            public int MaterialQuantity { get; set; }
            public int MaterialPrice { get; set; }
            public string Title { get; set; }
            public Company PublishingCompany { get; set; }
            public GeoLocation LocationCompany { get; set; }
            public Material Material { get; set; }

            public PublishData(string title)
            {
                this.Title = title;
            }
        }
    }
}