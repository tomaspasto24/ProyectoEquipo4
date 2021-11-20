using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System;

namespace Bot
{
    /// <summary>
    /// Clase que se encarga de administrar la deserialización, es decir, extraer todos las clases de los archivos JSON
    /// y llevarlas al programa. Cumple con el patrón de diseño SRP porque es la única responsabilidad de la clase, a tal punto
    /// de que el único método publico es DeserializeProgram.
    /// </summary>
    public class DeserializeManager 
    {
        private const string PathContainerCompany = @"..\..\docs\CompanyDataBase.json";
        private const string PathContainerPublication = @"..\..\docs\PublicationDataBase.json";
        private const string PathContainerToken = @"..\..\docs\TokenDataBase.json";
        private const string PathContainerAllUsers = @"..\..\docs\UserDataBase.json";
        private const string PathContainerDiccUserTokens = @"..\..\docs\DiccUserTokensDataBase.json";
        private static DeserializeManager instance;

        private DeserializeManager() { }

        /// <summary>
        /// Obtiene el acceso a la propia instancia de la clase DeserializeManager,
        /// en caso de que la variable instance no este creada, la crea y la retorna. En caso 
        /// contrario de que anteriormente este creada simplemente la retorna, asi se asegura de que
        /// siempre se use la misma variable instancia.
        /// </summary>
        /// <returns>Instancia Deserialize.</returns>
        public static DeserializeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DeserializeManager();
                }

                return instance;
            }
        }

        public bool DeserializeSessionRelated()
        {
            JsonSerializerOptions options = new () 
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true,
            };

            try
            {
                string jsonUsers = File.ReadAllText(PathContainerAllUsers);

                List<UserInfo> allUsersList = JsonSerializer.Deserialize<List<UserInfo>>(jsonUsers, options);
                foreach (UserInfo user in allUsersList)
                {
                    SessionRelated.Instance.AddNewUser(user);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }

            try
            {
                string jsonDiccUserTokens = File.ReadAllText(PathContainerDiccUserTokens);
                Dictionary<string, Company> diccUserTokens = JsonSerializer.Deserialize<Dictionary<string, Company>>(jsonDiccUserTokens, options);
                foreach (string idKey in diccUserTokens.Keys)
                {
                    SessionRelated.Instance.DiccUserTokens.Add(idKey, diccUserTokens[idKey]);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        private bool DeserializePublications()
        {
            try
            {
                string json = File.ReadAllText(PathContainerPublication);

                JsonSerializerOptions options = new () 
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true,
                };

                IList<Publication> listPublications = JsonSerializer.Deserialize<IList<Publication>>(json, options);
                foreach (Publication publication in listPublications)
                {
                    PublicationSet.Instance.AddElement(publication);
                }

                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
