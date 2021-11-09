using System;
namespace Bot
{
    /// <summary>
    /// clase abstracta Role.
    /// </summary>
    public abstract class Role
    {
        private string name;
        private int id;

        /// <summary>
        /// constructor de la clase Role
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public Role(String name, int id)
        {
            this.name = name;
            this.id = id;
        }
        /// <summary>
        /// Propiedad para tener get público el atributo name.
        /// </summary>
        /// <value></value>
        public String Name
        {
            get
            {
                return this.name;
            }
        }
    }
}