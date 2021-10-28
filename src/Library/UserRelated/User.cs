using System;
namespace Bot
{

    /// <summary>
    /// clase que representa al Usuario en el sistema
    /// </summary>
    public class User
    {

        public string Username { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// se registra al usuario
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        /// <summary>
        /// metodo para cambiar nombre
        /// </summary>
        /// <param name="newName"></param>
        public void ChangeUsername(string newName)
        {
            this.Username = newName;
        }
        /// <summary>
        /// metodo para cambiar contraseña
        /// </summary>
        /// <param name="newPassword"></param>
        public void ChangePassword(string newPassword)
        {
            this.Password = newPassword;
        }
    }
}