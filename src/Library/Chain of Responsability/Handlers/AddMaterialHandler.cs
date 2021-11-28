using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de agregar un material a una publicación
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class AddMaterialHandler : AbstractHandler
    {
        private Dictionary<UserInfo, MaterialData> materialData = new Dictionary<UserInfo, MaterialData>();

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
                this.materialData.Remove(user);
                this.materialData.Add(user, new MaterialData(request.Text));
                response = "Envía el nombre del material";
                user.HandlerState = Bot.State.AskingMaterialNameToAdd;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialNameToAdd)
            {
                MaterialData md = this.materialData[user];
                md.MaterialName = request.Text;
                response = "Envía la cantidad del material";
                user.HandlerState = Bot.State.AskingMaterialQuantityToAdd;
                return true;
            }
            else if ((user.HandlerState == Bot.State.AskingMaterialQuantityToAdd))
            {
                MaterialData md = this.materialData[user];
                md.MaterialQuantity = Int32.Parse(request.Text);
                response = "Envía el precio del material";
                user.HandlerState = Bot.State.AskingMaterialPriceToAdd;
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingMaterialPriceToAdd)
            {
                MaterialData md = this.materialData[user];
                md.MaterialPrice = Int32.Parse(request.Text);

                md.Material = new Material(md.MaterialName, md.MaterialQuantity, md.MaterialPrice);
                foreach (Publication publication in PublicationSet.Instance.ListPublications)
                {
                    if (publication.Title == md.PublicationTitle)
                    {
                        publication.AddMaterial(md.Material);
                    }
                }
                response = "Se ha agregado el material a la publicación. Si quieres agregar otro material envía \"/agregarmaterial\".\nEnvía \"/cancelar\" para cancelar la operación.";
                user.HandlerState = Bot.State.Start;
                return true;
            }
            response = string.Empty;
            return false;
        }
        class MaterialData
        {
            public string MaterialName { get; set; }
            public int MaterialQuantity { get; set; }
            public int MaterialPrice { get; set; }
            public Material Material { get; set; }
            public string PublicationTitle { get; set; }
            public MaterialData(string publicationTitle)
            {
                this.PublicationTitle = publicationTitle;
            }
        }
    }
}