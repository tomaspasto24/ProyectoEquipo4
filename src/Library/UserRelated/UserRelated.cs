namespace Bot
{
    /*
    Patrones y principios:
    Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP.
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    */
    /// <summary>
    /// Clase UserRelated que contiene informacion acerca del usuario.
    /// </summary>
    public class UserRelated
    {
        /// <summary>
        /// Almacena el canal de interaccion con el bot
        /// </summary>
        /// <value>Canal de interaccion</value>
        public AbstractBot Channel { get; set; }
        /// <summary>
        /// Almacena el usuario
        /// </summary>
        /// <value>Usuario</value>
        public User User { get; set; }

        private static UserRelated instance;
        /// <summary>
        /// Metodo getter para instanciar instance en caso de que sea null para tener una unica instancia de la clase y que sea de acceso global.
        /// </summary>
        /// <value>La instancia inicializada</value>
        public static UserRelated Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserRelated();
                }
                return instance;
            }
        }

        /// <summary>
        /// Constructor de la clase UserRelated.
        /// </summary>
        private UserRelated()
        {
            this.Channel = null;
            this.User = null;
        }

        /// <summary>
        /// Metodo para cambiar el rol del usuario a UserCompany
        /// </summary>
        /// <param name="company">La company que va a tener asociada</param>
        public void ChangeRoleToUserCompany(Company company)
        {
            RoleUserCompany newRole = new RoleUserCompany(company, this.User.Name, this.User.Id);
            this.User.Role = newRole;
        }

        /// <summary>
        /// Metodo para cambiar el rol del usuario a Admin
        /// </summary>
        public void ChangerRoleToAdmin()
        {
            RoleAdmin newRole = new RoleAdmin(this.User.Name, this.User.Id);
            this.User.Role = newRole;
        }

        /// <summary>
        /// Metodo para cambiar el rol del usuario a Emprendedor
        /// </summary>
        /// <param name="heading">Rubro</param>
        /// <param name="location">Locacion del usuario</param>
        /// <param name="certification">Certificacion</param>
        /// <param name="specialization">Especializacion</param>
        public void ChangeRoleToEntrepreneur(string heading, GeoLocation location, string certification, string specialization)
        {
            RoleEntrepreneur newRole = new RoleEntrepreneur(this.User.Name, this.User.Id, heading, location, certification, specialization);
            this.User.Role = newRole;
        }
    }
}