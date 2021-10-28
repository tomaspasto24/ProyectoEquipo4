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

        private static SessionRelated instance;
        public static SessionRelated Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionRelated();
                }
                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private SessionRelated()
        {
            AllUsers = new List<User>();
            DiccUserRelated = new Dictionary<string, UserRelated>();
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
            AllUsers.Add(new User(username, password));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            AllUsers.Remove(user);
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
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetChatChannel(string id, IBot channel)
        {
            ReturnInfo(id).Channel = channel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserRelated ReturnInfo(string token)
        {
            UserRelated info;
            // TryGetValue: Intenta devolver en info, el valor que tiene asignado la key id.
            if (DiccUserRelated.TryGetValue(token, out info))
            {
                return info;
            }
            info = new UserRelated();
            DiccUserRelated.Add(token, info);
            return info;
        }
    }
}