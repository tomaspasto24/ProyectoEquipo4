using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de crear un token para una empresa
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class TokenHandler : AbstractHandler
    {
        private Dictionary<UserInfo, TokenGenerationData> dataPerUser = new Dictionary<UserInfo, TokenGenerationData>();

        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public TokenHandler(AbstractHandler succesor) 
        : base(succesor)
        {
        }

        /// <summary>
        /// Intenta procesar el mensaje recibido y devuelve una respuesta.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
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
            public TokenGenerationData(string name)
            {
                this.Name = name;
            }
            
            public string Name { get; set; }
            
            public string Heading { get; set; }
            
            public string Address { get; set; }
            
            public string City { get; set; }
        }
    }
}