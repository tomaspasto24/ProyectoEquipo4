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
        public AbstractBot Channel { get; set; }
        public User User { get; set; }

        private static UserRelated instance;
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

        public void ChangeRoleToUserCompany(Company company)
        {
            RoleUserCompany newRole = new RoleUserCompany(company, this.User.Name, this.User.Id);
            this.User.Role = newRole;
        }

        public void ChangerRoleToAdmin()
        {
            RoleAdmin newRole = new RoleAdmin(this.User.Name, this.User.Id);
            this.User.Role = newRole;
        }

        public void ChangeRoleToEntrepreneur(string heading, GeoLocation location, string certification, string specialization)
        {
            RoleEntrepreneur newRole = new RoleEntrepreneur(this.User.Name, this.User.Id, heading, location, certification, specialization);
            this.User.Role = newRole;
        }
    }
}