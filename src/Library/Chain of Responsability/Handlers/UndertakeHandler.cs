using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de convertir un usuario Default en Entrepreneur
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class UndertakeHandler : AbstractHandler
    {
        /// <summary>
        /// Diccionario que contiene cada usuario intentando emprender con su respectiva información.
        /// </summary>
        /// <typeparam name="UserInfo">El usuario que intenta emprender</typeparam>
        /// <typeparam name="EntrepeneurData">La información del usuario</typeparam>
        private Dictionary<UserInfo, EntrepeneurData> entrepreneurData = new Dictionary<UserInfo, EntrepeneurData>();

        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public UndertakeHandler(AbstractHandler succesor) : base(succesor)
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

            if (!user.HasPermission(Permission.Undertake))
            {
                response = string.Empty;
                return false;
            }

            if (user.HandlerState == Bot.State.Start && request.Text.Equals("/emprender"))
            {
                user.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
                response = "Por favor, dinos tu rubro. \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingHeadingEntrepreneur)
            {
                user.HandlerState = Bot.State.ConfirmingCityEntrepreneur;
                this.entrepreneurData.Remove(user);
                this.entrepreneurData.Add(user, new EntrepeneurData(request.Text));
                response = "Rubro registrado. Ahora dinos en que ciudad vives. \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingCityEntrepreneur)
            {
                user.HandlerState = Bot.State.ConfirmingAdressEntrepreneur;
                EntrepeneurData ed = this.entrepreneurData[user];
                ed.City = request.Text;
                response = "Ciudad registrada. Ahora dinos tu direccion. \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingAdressEntrepreneur)
            {
                user.HandlerState = Bot.State.Start;
                EntrepeneurData ed = this.entrepreneurData[user];
                response = "Direccion registrada. \nAhora eres un emprendedor!";
                EntrepreneurInfo entrepreneurInfo = new EntrepreneurInfo(ed.Heading, new GeoLocation(request.Text, ed.City));
                user.Permissions = UserInfo.EntrepreneurPermissions;
                SessionRelated.Instance.DiccEntrepreneurInfo.Add(user, entrepreneurInfo);
                return true;
            }

            response = string.Empty;
            return false;
        }
        /// <summary>
        /// Clase interna encargada de alamacenar información de emprendedores.
        /// </summary>
        class EntrepeneurData
        {
            /// <summary>
            /// Rubro del usuario
            /// </summary>
            public string Heading { get; set; }
            /// <summary>
            /// Ciudad del usuario
            /// </summary>
            public string City { get; set; }
            /// <summary>
            /// Crea una nueva instancia de este contenedor de información y define el rubro.
            /// </summary>
            /// <param name="heading">El rubro del usuario</param>
            public EntrepeneurData(string heading)
            {
                this.Heading = heading;
            }
        }
    }
}