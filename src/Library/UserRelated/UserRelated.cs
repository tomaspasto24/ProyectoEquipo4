namespace Bot
{
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
    }
}