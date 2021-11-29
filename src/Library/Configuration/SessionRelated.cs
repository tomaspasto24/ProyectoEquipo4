using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Linq;

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
        [JsonInclude]
        public IList<UserInfo> AllUsers = new List<UserInfo>();

        /// <summary>
        /// Diccionario que contiene el token que se relacion con la compañía
        /// </summary>
        /// <typeparam name="string">El token en cuestión</typeparam>
        /// <typeparam name="Company">La compañía en cuestión</typeparam>
        public IDictionary<string, Company> DiccUserTokens = new Dictionary<string, Company>();

        /// <summary>
        /// Diccionario que contiene la UserInfo que se relaciona con la EntrepreneurInfo
        /// </summary>
        /// <typeparam name="UserInfo">El UserInfo</typeparam>
        /// <typeparam name="EntrepreneurInfo">La EntrepreneurInfo</typeparam>
        public Dictionary<UserInfo, EntrepreneurInfo> DiccEntrepreneurInfo = new Dictionary<UserInfo, EntrepreneurInfo>();
        /// <summary>
        /// Diccionario que contiene la UserInfo que se relaciona con la AdminInfo
        /// </summary>
        /// <typeparam name="UserInfo">La UserInfo</typeparam>
        /// <typeparam name="AdminInfo">La AdminInfo</typeparam>
        public IDictionary<UserInfo, AdminInfo> DiccAdminInfo = new Dictionary<UserInfo, AdminInfo>();
        /// <summary>
        /// Diccionario que contiene la UserInfo que se relaciona con la UserCompanyInfo
        /// </summary>
        /// <typeparam name="UserInfo">La UserInfo</typeparam>
        /// <typeparam name="UserCompanyInfo">La UserCompanyInfo</typeparam>
        /// <returns></returns>
        public IDictionary<UserInfo, UserCompanyInfo> DiccUserCompanyInfo = new Dictionary<UserInfo, UserCompanyInfo>();

        private static SessionRelated instance;

        /// <summary>
        /// Obtiene DiccUserTokens como una lista
        /// </summary>
        [JsonInclude]
        public List<KeyValuePair<string, Company>> DiccUserTokensToSerialize
        {
            get { return this.DiccUserTokens.ToList(); }
            set { this.DiccUserTokens = value.ToDictionary(x => x.Key, x => x.Value); }
        }

        /// <summary>
        /// Obtiene DiccEntrepreneurInfo como una lista
        /// </summary>
        [JsonInclude]
        public List<KeyValuePair<UserInfo, EntrepreneurInfo>> DiccEntrepreneurInfoToSerialize
        {
            get { return this.DiccEntrepreneurInfo.ToList(); }
            set { this.DiccEntrepreneurInfo = value.ToDictionary(x => x.Key, x => x.Value); }
        }
        /// <summary>
        /// Obtiene DiccAdminInfo como una lista
        /// </summary>
        [JsonInclude]
        public List<KeyValuePair<UserInfo, AdminInfo>> DiccAdminInfoToSerialize
        {
            get { return this.DiccAdminInfo.ToList(); }
            set { this.DiccAdminInfo = value.ToDictionary(x => x.Key, x => x.Value); }
        }
        /// <summary>
        /// Obtiene DiccUserCompanyInfo como una lista
        /// </summary>
        [JsonInclude]
        public List<KeyValuePair<UserInfo, UserCompanyInfo>> DiccUserCompanyInfoToSerialize
        {
            get { return this.DiccUserCompanyInfo.ToList(); }
            set { this.DiccUserCompanyInfo = value.ToDictionary(x => x.Key, x => x.Value); }
        }
        /// <summary>
        /// Obtiene una única instancia de esta clase
        /// </summary>
        /// <value>La única instancia de esta clase.</value>
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
            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Constructor de la clase SessionRelated sin implementación y de acceso publico para poder ser usado por 
        /// la etiqueta JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public SessionRelated() { }

        /// <summary>
        /// Agrega un nuevo usuario en caso de que no exista
        /// </summary>
        /// <param name="user">El usuario en cuestión</param>
        public void AddNewUser(UserInfo user)
        {
            if (UsernameExists(user.Id))
            {
                return;
            }
            AllUsers.Add(user);
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="user">Usuaurio a borrar</param>
        public void DeleteUser(UserInfo user)
        {
            AllUsers.Remove(user);
        }

        /// <summary>
        /// Verifica si existe un usuario
        /// </summary>
        /// <param name="id">Id del usuario a verificar</param>
        /// <returns>True si existe el usuario, false en caso contrario</returns>
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
        /// Obtiene el usuario a partir de un id
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
        /// <returns>La compañía relacionada al token o nulo en caso de que no haya una compañía con ese token</returns>
        public Company GetCompanyByToken(string token)
        {
            Company company;
            if (DiccUserTokens.TryGetValue(token.ToString(), out company))
            {
                return company;
            }
            return null;
        }

        /// <summary>
        /// Obtiene la compañía a partir del nombre
        /// </summary>
        /// <param name="companyName">El nombre de la compañía</param>
        /// <returns>Si existe, la compañía. Nulo en caso contrario</returns>
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

        /// <summary>
        /// Obtiene un token a partir de la compañía
        /// </summary>
        /// <param name="company">La compañía en cuestión</param>
        /// <returns>Si existe, el token. Nulo en caso contrario</returns>
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

        /// <summary>
        /// Obtiene una EntrepreneurInfo a partir de un UserInfo
        /// </summary>
        /// <param name="user">El UserInfo en cuestión</param>
        /// <returns>En caso de que exista, la EntrepreneurInfo asociada a la UserInfo. Nulo en caso contrario</returns>
        public EntrepreneurInfo GetEntrepreneurInfoByUserInfo(UserInfo user)
        {
            EntrepreneurInfo entrepreneur;
            if (DiccEntrepreneurInfo.TryGetValue(user, out entrepreneur))
            {
                return entrepreneur;
            }
            return null;
        }
        /// <summary>
        /// Obtiene una AdminInfo a partir de un UserInfo
        /// </summary>
        /// <param name="user">El UserInfo en cuestión</param>
        /// <returns>En caso de que exista, la AdminInfo asociada a la UserInfo. Nulo en caso contrario</returns>
        public AdminInfo GetAdminInfoByUserInfo(UserInfo user)
        {
            AdminInfo admin;
            if (DiccAdminInfo.TryGetValue(user, out admin))
            {
                return admin;
            }
            return null;
        }
        /// <summary>
        /// Obtiene una UserCompanyInfo a partir de un UserInfo
        /// </summary>
        /// <param name="user">El UserInfo en cuestión</param>
        /// <returns>En caso de que exista, la UserCompanyInfo asociada a la UserInfo. Nulo en caso contrario</returns>
        public UserCompanyInfo GetUserCompanyByUserInfo(UserInfo user)
        {
            UserCompanyInfo userCompany;
            if (DiccUserCompanyInfo.TryGetValue(user, out userCompany))
            {
                return userCompany;
            }
            return null;
        }
        public UserInfo GetUserInfoByEntrepreneurInfo(EntrepreneurInfo entrepreneurInfo)
        {
            foreach (UserInfo userInfo in DiccEntrepreneurInfo)
            {
                if (DiccEntrepreneurInfo[userInfo].Equals(entrepreneurInfo))
                {
                    return userInfo;
                }
            }
            return null;
        }
    }
}