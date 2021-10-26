using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase SessionRelated que se ocupa de administrar la lista de usuarios y sus respectivos id's.
    /// </summary>
    public class SessionRelated
    {
        public static List<User> AllUsers;
        public static Dictionary<string, UserRelated> DiccUserRelated;

        /// <summary>
        /// 
        /// </summary>
        public SessionRelated()
        {
            this.AllUsers = new List<User>();
            this.DiccUserRelated = new Dictionary<string, UserRelated>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void AddNewUser(string username, string password)
        {
            if (UsernameExists(username))
            {
                return;
            }
            User newUser = new User(username, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            AllUsers.Remove(username);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UsernameExists(string username)
        {
            foreach (User user in AllUsers)
            {
                if (user.username == username)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserRelated ReturnInfo(string id)
        {
            UserRelated info;
            // TryGetValue: Intenta devolver en info, el valor que tiene asignado la key id.
            if (DiccUserRelated.TryGetValue(id, out info))
            {
                return info;
            }
            else
            {
                info = new UserRelated();
                DiccUserRelated.Add(id, info);
            }
            return info;
        }
    }
}