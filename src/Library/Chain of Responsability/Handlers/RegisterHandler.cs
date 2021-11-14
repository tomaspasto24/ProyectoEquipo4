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
        /// Indica los diferentes estados que puede tener el comando RegisterHandler.
        /// - Start: El estado inicial del comando. En este estado el comando pide el token de registro
        /// - ConfirmingToken: Luego de pedir el token. En este estado el comando valida si el token ingresado existe y vuelve al estado Start.
        /// </summary>
        public enum RegisterState
        {
            /// Estado antes de mandar el token
            Start,
            /// Estado mientras el bot espera y confirma un token
            ConfirmingToken,
        }

        /// <summary>
        /// Los datos que va obteniendo el comando en los diferentes estados.
        /// </summary>
        public RegisterData Data { get; private set; } = new RegisterData();

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public RegisterState State { get; private set; }

        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public RegisterHandler(AbstractHandler succesor) : base(succesor)
        {
            State = RegisterState.Start;
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);

            if ((State == RegisterState.Start) && (request.Text.Equals("/registro")))
            {
                this.State = RegisterState.ConfirmingToken;
                response = "Inserta tu token de usuario empresa: ";
                return true;
            }
            else if (State == RegisterState.ConfirmingToken)
            {
                if (SessionRelated.Instance.DiccUserTokens.ContainsKey(request.Text))
                {
                    this.State = RegisterState.Start;
                    this.Data.Token = request.Text;
                    user.ChangeRoleToUserCompany(SessionRelated.Instance.GetCompanyByToken(request.Text));
                    response = "Token verificado, ahora eres un usuario empresa! :)";
                    return true;
                }
                else
                {
                    this.Data.Token = request.Text;
                    this.State = RegisterState.Start;
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