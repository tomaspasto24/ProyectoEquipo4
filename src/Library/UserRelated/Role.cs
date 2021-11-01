using System;
namespace Bot
{
    public abstract class Role : User
    {
        /// <summary>
        /// cambiar name e id que recibe base
        /// </summary>
        public Role(String name, int id) : base(name, id)
        {
            throw new NotImplementedException();
        }
    }
}