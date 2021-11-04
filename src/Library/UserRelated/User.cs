using System;
namespace Bot
{
    /// <summary>
    /// Clase encargada de representar al usuario (componiendo name, id y role). Esta cumple con el patron SRP y Expert 
    /// </summary>
    public class User
    {
        private string name;
        private int id;
        /// <summary>
        /// atributo de la clase User con get y set.
        /// </summary>
        /// <value></value>
        public Role Role { set; get; }

        /// <summary>
        /// Propiedad para tener get público el atributo id.
        /// </summary>
        /// <value>int</value>
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        /// <summary>
        /// Propiedad para tener get público el atributo name.
        /// </summary>
        /// <value></value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        ///Método constructor de la clase User que se encarga de asignar los atributos
        ///name, id y role que usará la clase.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="role"></param>
        public User(string name, int id, Role role)
        {
            this.name = name;
            this.id = id;
            this.Role = role;
        }
    }
}