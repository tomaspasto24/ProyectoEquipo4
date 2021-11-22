using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


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
        [JsonInclude]
        /// <summary>
        /// Lista de todos los usuarios
        /// </summary>
        public IList<UserInfo> AllUsers;

        [JsonInclude]
        /// <summary>
        /// Diccionario que contiene el token que se relaciona con la empresa
        /// </summary>
        public IDictionary<string, Company> DiccUserTokens;

        private static SessionRelated instance;
        /// <summary>
        /// Metodo getter para instanciar instance en caso de que sea null para tener una unica instancia de la clase y que sea de acceso global.
        /// </summary>
        /// <value>La instancia inicializada</value>
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
            AllUsers = new List<UserInfo>();
            DiccUserTokens = new Dictionary<string, Company>();
        }

        /// <summary>
        /// Metodo para agregar un nuevo usuario
        /// </summary>
        /// <param name="name">Nombre del usuario</param>
        /// <param name="id">Id del usuario</param>
        /// <param name="role">Role del usuariro</param>
        public void AddNewUser(UserInfo user)
        {
            if (UsernameExists(user.Id))
            {
                return;
            }
            AllUsers.Add(user);
        }

        /// <summary>
        /// Metodo para borrar un usuario
        /// </summary>
        /// <param name="user">Usuaurio a borrar</param>
        public void DeleteUser(UserInfo user)
        {
            AllUsers.Remove(user);
        }

        /// <summary>
        /// Metodo para verificar si existe un usuario
        /// </summary>
        /// <param name="id">Id del usuario a verificar</param>
        /// <returns>true o false</returns>
        public bool UsernameExists(long id)
        {
            foreach (UserInfo user in AllUsers)
            {
                if (user.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Metodo para obtener el usuario relacionado al id.
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>El usuario</returns>
        public UserInfo GetUserById(long id)
        {
            return (AllUsers as List<UserInfo>).Find(user => user.Id == id);
        }

        /// <summary>
        /// Metodo para retornar la Company asociada al token generado
        /// </summary>
        /// <param name="token">Token que el usuario inserta</param>
        /// <returns></returns>
        public Company GetCompanyByToken(string token)
        {
            Company company;
            if (DiccUserTokens.TryGetValue(token.ToString(), out company))
            {
                return company;
            }
            return null;
        }

        public Company GetCompanyByName(string companyName)
        {
            foreach (string token in SessionRelated.Instance.DiccUserTokens.Keys)
            {
                if (SessionRelated.Instance.DiccUserTokens[token].Name.ToLower().Equals(companyName.ToLower()))
                {
                    return SessionRelated.Instance.DiccUserTokens[token];
                }
            }
            return null;
        }

        public string GetTokenByCompany(Company company)
        {
            foreach (string token in SessionRelated.Instance.DiccUserTokens.Keys)
            {
                if (SessionRelated.Instance.DiccUserTokens[token].Equals(company))
                {
                    return token;
                }
            }
            return null;
        }

        public (string, string) ConvertObjectToSaveToJson()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true,
            };
            return (JsonSerializer.Serialize(this.AllUsers, options), JsonSerializer.Serialize(this.DiccUserTokens, options));
        }
    }
}