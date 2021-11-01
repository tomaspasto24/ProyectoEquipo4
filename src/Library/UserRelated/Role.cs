using System;
namespace Bot
{
    public abstract class Role
    {
        private string name;
        private int id;

        /// <summary>
        /// cambiar name e id que recibe base
        /// </summary>
        public Role(String name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}