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
    public class TokenHandler : AbstractHandler
    {
        private Dictionary<UserInfo, TokenGenerationData> dataPerUser = new Dictionary<UserInfo, TokenGenerationData>();

        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public TokenHandler(AbstractHandler succesor) : base(succesor)
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

            if (!user.HasPermission(Permission.GenerateToken))
            {
                response = string.Empty;
                return false;
            }

            if (user.HandlerState == Bot.State.Start && request.Text.Equals("/crearinvitacion"))
            {
                user.HandlerState = Bot.State.ConfirmingCompanyName;
                response = "Procedamos a crear la empresa para el token. \nDinos el nombre del empresa.\nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingCompanyName)
            {
                Company company = SessionRelated.Instance.GetCompanyByName(request.Text);
                if (company != null)
                {
                    response = $"Esa empresa ya está registrada y su token es: \n{SessionRelated.Instance.GetTokenByCompany(company)}\nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.Start;
                    return true;
                }
                else
                {
                    this.dataPerUser.Remove(user);
                    this.dataPerUser.Add(user, new TokenGenerationData(request.Text));
                    response = "Genial, tenemos el nombre de la empresa. \nAhora dinos el rubro.\nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.ConfirmingCompanyHeader;
                    return true;
                }
            }
            else if (user.HandlerState == Bot.State.ConfirmingCompanyHeader)
            {
                TokenGenerationData tgd = this.dataPerUser[user];
                tgd.Heading = request.Text;
                response = "Genial, tenemos el rubro de la empresa. \nAhora dinos la ciudad donde se ubica la empresa.\nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.ConfirmingCompanyCity;
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingCompanyCity)
            {
                TokenGenerationData tgd = this.dataPerUser[user];
                tgd.City = request.Text;
                response = "Genial, tenemos la ciudad de la empresa. \nAhora dinos la direccion de la empresa.\nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.ConfirmingCompanyAddress;
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingCompanyAddress)
            {
                TokenGenerationData tgd = this.dataPerUser[user];
                tgd.Address = request.Text;
                response = "Genial, tenemos la direccion de la empresa. \nAhora dinos el contacto de la empresa (e-mail o telefono).\nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.ConfirmingCompanyContact;
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingCompanyContact)
            {
                TokenGenerationData tgd = this.dataPerUser[user];
                string token = TokenGenerator.Instance.GenerateToken().ToString();
                user.HandlerState = Bot.State.Start;
                response = $"Genial, tenemos el contacto de la empresa. \nEmpresa creada con exito. El token para esta empresa es: \n{token}";
                Company company = new Company(tgd.Name, tgd.Heading, new GeoLocation(tgd.Address, tgd.City), request.Text);
                SessionRelated.Instance.DiccUserTokens.Add(token.ToString(), company);
                this.dataPerUser.Remove(user);
                return true;
            }

            response = string.Empty;
            return false;
        }

        class TokenGenerationData
        {
            public string Name { get; set; }
            public string Heading { get; set; }
            public string Address { get; set; }
            public string City { get; set; }

            public TokenGenerationData(string name)
            {
                this.Name = name;
            }
        }
    }
}