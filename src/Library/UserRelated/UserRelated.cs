namespace Bot
{
    /// <summary>
    /// Clase UserRelated que contiene informacion acerca del usuario.
    /// </summary>
    public class UserRelated
    {
        public AbstractBot Channel { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Constructor de la clase UserRelated.
        /// </summary>
        public UserRelated()
        {
            this.Channel = null;
            this.User = null;
        }

        public void ChangeRoleToUserCompany(Company company)
        {
            RoleUserCompany newRole = new RoleUserCompany(company, User.Name, User.Id);
            this.User.Role = newRole;
        }
    }
}