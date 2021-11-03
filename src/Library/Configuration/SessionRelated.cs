using System.Collections.Generic;

namespace Bot
{
    /*
    Patrones y principios:
    Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP.
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    Cumple con el patrón Singleton, esto lo que hace es que, garantiza que haya una única instancia de la clase y de esta forma se obtiene
    un acceso global a esta instancia.
    */
    /// <summary>
    /// Clase SessionRelated que se ocupa de administrar la lista de usuarios y sus respectivos id's.
    /// </summary>
    public class SessionRelated
    {
        /// <summary>
        /// Lista de todos los usuarios
        /// </summary>
        public static List<User> AllUsers;

        /// <summary>
        /// Diccionario que contiene la id que se relaciona con un usuario
        /// </summary>
        public static Dictionary<int, UserRelated> DiccUserRelated;

        /// <summary>
        /// Diccionario que contiene el token que se relaciona con la empresa
        /// </summary>
        public static Dictionary<string, Company> DiccUserTokens = new Dictionary<string, Company>();

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
        /// Constructor de la clase SessionRelated
        /// </summary>
        private SessionRelated()
        {
            AllUsers = new List<User>();
            DiccUserRelated = new Dictionary<int, UserRelated>();
            DiccUserTokens = new Dictionary<string, Company>();
        }

        /// <summary>
        /// Metodo para agregar un nuevo usuario
        /// </summary>
        /// <param name="name">Nombre del usuario</param>
        /// <param name="id">Id del usuario</param>
        /// <param name="role">Role del usuariro</param>
        public void AddNewUser(string name, int id, Role role)
        {
            if (UsernameExists(id))
            {
                return;
            }
            AllUsers.Add(new User(name, id, role));
        }

        /// <summary>
        /// Metodo para borrar un usuario
        /// </summary>
        /// <param name="user">Usuaurio a borrar</param>
        public void DeleteUser(User user)
        {
            AllUsers.Remove(user);
        }

        /// <summary>
        /// Metodo para verificar si existe un usuario
        /// </summary>
        /// <param name="id">Id del usuario a verificar</param>
        /// <returns>true o false</returns>
        public static bool UsernameExists(int id)
        {
            foreach (User user in AllUsers)
            {
                if (user.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Metodo para cambiar el canal de comunicacion entre el bot y el usuario
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <param name="channel">Canal que se va a usar</param>
        public void SetChatChannel(int id, AbstractBot channel)
        {
            ReturnInfo(id).Channel = channel;
        }

        /// <summary>
        /// Metodo para obtener la informacion relacionada a un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>La informacion del usuario</returns>
        public UserRelated ReturnInfo(int id)
        {
            UserRelated info;
            // TryGetValue: Intenta devolver en info, el valor que tiene asignado la key id.
            if (DiccUserRelated.TryGetValue(id, out info))
            {
                return info;
            }
            info = UserRelated.Instance;
            DiccUserRelated.Add(id, info);
            return info;
        }

        public Company ReturnCompany(string token)
        {
            Company company;
            if (DiccUserTokens.TryGetValue(token, out company))
            {
                return company;
            }
            return null;
        }
    }
}