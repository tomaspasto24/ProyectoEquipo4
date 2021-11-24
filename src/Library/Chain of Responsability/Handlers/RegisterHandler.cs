using System;

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
    public class RegisterHandler : AbstractHandler
    {
        /// <summary>
        /// Los datos que va obteniendo el comando en los diferentes estados.
        /// </summary>
        public RegisterData Data { get; private set; } = new RegisterData();

        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public RegisterHandler(AbstractHandler succesor) : base(succesor)
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
            

            if (!user.HasPermission(Permission.Register))
            {
                response = string.Empty;
                return false;
            }

            if ((user.HandlerState == Bot.State.Start) && (request.Text.Equals("/registro")))
            {
                user.HandlerState = Bot.State.ConfirmingToken;
                response = "Inserta tu token de usuario empresa: ";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingToken)
            {
                if (SessionRelated.Instance.DiccUserTokens.ContainsKey(request.Text))
                {
                    user.HandlerState = Bot.State.Start;
                    this.Data.Token = request.Text;
                    user.Permissions = UserInfo.UserCompanyPermissions;
                    /*user.UserRole = new UserCompanyInfo(SessionRelated.Instance.GetCompanyByToken(request.Text)); */ // TODO revisar esto
                    response = "Token verificado, ahora eres un usuario empresa! :)";
                    return true;
                }
                else
                {
                    this.Data.Token = request.Text;
                    user.HandlerState = Bot.State.Start;
                    response = "Disculpa, no hemos encontrado ese token :(";
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }

        /// <summary>
        /// Clase para almacenar la data relacionada al registro
        /// </summary>
        public class RegisterData
        {
            /// <summary>
            /// El token que se ingresa en el estado ConfirmingToken
            /// </summary>
            public string Token { get; set; }
        }
    }
}