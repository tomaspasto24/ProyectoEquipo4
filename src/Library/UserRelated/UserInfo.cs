namespace Bot
{
    /// <summary>
    /// Clase encargada de representar al usuario (componiendo name, id y role). Esta cumple con el patron SRP y Expert.
    /// </summary>
    public class UserInfo
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public IRole UserRole { get; set; }
        public State HandlerState { get; set; }

        /// <summary>
        /// Método constructor de la clase User que se encarga de asignar los atributos
        /// name, id y role que usará la clase.
        /// </summary>
        /// <param name="name">El nombre del usuario.</param>
        /// <param name="id">El id del usuario.</param>
        /// <param name="role">El role del usuario</param>
        public UserInfo(string name, long id, IRole role)
        {
            this.Name = name;
            this.Id = id;
            this.UserRole = role;
        }
    }
}