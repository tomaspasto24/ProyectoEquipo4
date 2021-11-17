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
    public class ConvertUserToEntrepreneurHandler : AbstractHandler
    {

        private Dictionary<UserInfo, EntrepeneurData> entrepreneurData = new Dictionary<UserInfo, EntrepeneurData>();

        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public ConvertUserToEntrepreneurHandler(AbstractHandler succesor) : base(succesor)
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

            if (user.UserRole is RoleDefault)
            {
                if (request.Text == null)
                {
                    throw new NullReferenceException("El mensaje no puede estar vacio, ni ser una imagen o video");
                }

                if (user.HandlerState == Bot.State.Start && request.Text.ToLower().Equals("/emprender"))
                {
                    user.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
                    response = "Por favor, dinos tu rubro. \nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (user.HandlerState == Bot.State.ConfirmingHeadingEntrepreneur)
                {
                    if (request.Text.ToLower().Equals("/cancelar"))
                    {
                        user.HandlerState = Bot.State.Start;
                        response = "Operación cancelada.";
                        return true;
                    }
                    user.HandlerState = Bot.State.ConfirmingCityEntrepreneur;
                    this.entrepreneurData.Add(user, new EntrepeneurData(request.Text));
                    response = "Rubro registrado. Ahora dinos en que ciudad vives. \nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (user.HandlerState == Bot.State.ConfirmingCityEntrepreneur)
                {
                    if (request.Text.ToLower().Equals("/cancelar"))
                    {
                        user.HandlerState = Bot.State.Start;
                        response = "Operación cancelada.";
                        return true;
                    }
                    user.HandlerState = Bot.State.ConfirmingAdressEntrepreneur;
                    EntrepeneurData ed = this.entrepreneurData[user];
                    ed.City = request.Text;
                    response = "Ciudad registrada. Ahora dinos tu direccion. \nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (user.HandlerState == Bot.State.ConfirmingAdressEntrepreneur)
                {
                    if (request.Text.ToLower().Equals("/cancelar"))
                    {
                        user.HandlerState = Bot.State.Start;
                        response = "Operación cancelada.";
                        return true;
                    }
                    user.HandlerState = Bot.State.Start;
                    EntrepeneurData ed = this.entrepreneurData[user];
                    response = "Direccion registrada. Ahora eres un emprendedor!";
                    RoleEntrepreneur roleEntrepreneur = new RoleEntrepreneur(ed.Heading, new GeoLocation(request.Text, ed.City));
                    user.UserRole = roleEntrepreneur;
                    return true;
                }
            }

            // TODO limpiar la clase EntrepeneurData

            response = string.Empty;
            return false;
        }
        class EntrepeneurData
        {
            public string Heading { get; set; }
            public string City { get; set; }

            public EntrepeneurData(string heading)
            {
                this.Heading = heading;
            }
        }
    }
}