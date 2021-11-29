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
    /// Handler que se encarga de crear una publicación para una empresa
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class PublishHandler : AbstractHandler
    {
        private Dictionary<UserInfo, PublishData> publishData = new Dictionary<UserInfo, PublishData>();
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public PublishHandler(AbstractHandler succesor) : base(succesor)
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
                pd.MaterialName = request.Text;
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
                PublishData pd = this.publishData[user];
                pd.MaterialPrice = Int32.Parse(request.Text);
                pd.Material = new Material(pd.MaterialName, pd.MaterialQuantity, pd.MaterialPrice);
                Publication publication = new Publication(pd.Title, pd.PublishingCompany, pd.LocationCompany, pd.Material);
                PublicationSet.Instance.AddElement(publication);
                (pd.PublishingCompany).AddOwnPublication(publication);
                response = "Se ha creado la publicación con el material indicado. Si quieres agregar otro material envía \"/agregarmaterial\". \n Envíe \"/cancelar\" si quiere terminar la publicación.";
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