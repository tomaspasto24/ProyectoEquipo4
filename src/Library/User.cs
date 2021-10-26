using System;
namespace Bot
{

    /// <summary>
    /// clase que representa al Usuario en el sistema
    /// </summary>
    public class User
    {
        public string Ci { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// se registra al usuario
        /// 
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        public User(string ci, string name, string location, string telephone, string password)
        {
            if (Utils.IdIsValid(ci))
            {
                this.Ci = ci;
            }
            this.Ci = ci;
            this.Name = name;
            this.Location = location;
            this.Telephone = telephone;
            this.Password = password;
        }
        public void ChangeName(string newName)
        {
            this.Name = newName;
        }
        public void ChangePassword(string newPassword)
        {
            this.Password = newPassword;
        }
    }
}

