using System;
namespace Bot
{
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
    }
}