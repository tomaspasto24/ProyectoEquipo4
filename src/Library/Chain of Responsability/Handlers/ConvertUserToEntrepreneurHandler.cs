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

        private string heading;

        private string address;

        private string city;

        private string certification;

        private string specialization;

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

            if (!(user.UserRole is RoleDefault))
            {
                throw new IncorrectRoleException("Disculpa no tienes el rol adecuado para utilizar este comando");
            }

            if (request.Text == null)
            {
                throw new NullReferenceException("El mensaje no puede estar vacio, ni ser una imagen o video");
            }
            //TODO testear
            if (request.Text.ToLower().Equals("/cancelar"))
            {
                user.HandlerState = Bot.State.Start;
                response = "Operación cancelada.";
                return true;
            }

            if (user.HandlerState == Bot.State.Start && request.Text.ToLower().Equals("/emprender"))
            {
                user.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
                // response = "Por favor, dinos tu rubro: ";
                response = "Por favor, dinos tu rubro. \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingHeadingEntrepreneur)
            {
                user.HandlerState = Bot.State.ConfirmingCityEntrepreneur;
                heading = request.Text;
                response = "Rubro registrado. Ahora dinos en que ciudad vives. \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingCityEntrepreneur)
            {
                user.HandlerState = Bot.State.ConfirmingAdressEntrepreneur;
                city = request.Text;
                response = "Ciudad registrada. Ahora dinos tu direccion. \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingAdressEntrepreneur)
            {
                user.HandlerState = Bot.State.ConfirmingCertificationEntrepreneur;
                address = request.Text;
                response = "Direccion registrada. Ahora dinos tu certificacion. \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingCertificationEntrepreneur)
            {
                user.HandlerState = Bot.State.ConfirmingSpecializationEntrepeneur;
                certification = request.Text;
                response = "Certificacion registrada. Ahora dinos tu especializacion. \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingSpecializationEntrepeneur)
            {
                user.HandlerState = Bot.State.Start;
                specialization = request.Text;
                response = "Especializacion registrada. Ahora eres un emprendedor!";
                RoleEntrepreneur roleEntrepreneur = new RoleEntrepreneur(heading,
                null); // TODO Preguntar que onda con geolocation, no me deja registrarla CAMBIAR DE NULL A UNA LOCATION
                user.UserRole = roleEntrepreneur;
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}