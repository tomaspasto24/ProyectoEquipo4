namespace Bot
{
    /// <summary>
    /// Clase encargada de representar al usuario (componiendo name, id y role). Esta cumple con el patron SRP y Expert.
    /// </summary>
    public class User
    {
        private string name;
        private int id;
        private Role userRole;

        /// <summary>
        /// Método constructor de la clase User que se encarga de asignar los atributos
        /// name, id y role que usará la clase.
        /// </summary>
        /// <param name="name">El nombre del usuario.</param>
        /// <param name="id">El id del usuario.</param>
        /// <param name="role">El rol del usuario.</param>
        public User(string name, int id, Role role)
        {
            this.name = name;
            this.id = id;
            this.userRole = role;
        }

        /// <summary>
        /// Obtiene el atributo Id de la clase usuario.
        /// </summary>
        /// <value>El Id del usuario.</value>
        public int Id
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Obtiene el valor de la propiedad privada name del usuario.
        /// </summary>
        /// <value>Nombre del usuario.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Obtiene o establece el valor de la propiedad role del usuario.
        /// </summary>
        /// <value>El rol del usuario.</value>
        public Role Role
        {
            get
            {
                return this.userRole;
            }

            set
            {
                this.userRole = value;
            }
        }
        /// <summary>
        /// Metodo para cambiar el rol del usuario a UserCompany
        /// </summary>
        /// <param name="company">La company que va a tener asociada</param>
        public void ChangeRoleToUserCompany(Company company)
        {
            RoleUserCompany newRole = new RoleUserCompany(company, this.Name, this.Id);
            this.Role = newRole;
        }

        /// <summary>
        /// Metodo para cambiar el rol del usuario a Admin
        /// </summary>
        public void ChangerRoleToAdmin()
        {
            RoleAdmin newRole = new RoleAdmin(this.Name, this.Id);
            this.Role = newRole;
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
            RoleEntrepreneur newRole = new RoleEntrepreneur(this.Name, this.Id, heading, location, certification, specialization);
            this.Role = newRole;
        }
    }
}