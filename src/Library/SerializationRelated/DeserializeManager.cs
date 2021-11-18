using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Bot
{
    /// <summary>
    /// Clase que se encarga de administrar la deserialización, es decir, extraer todos las clases de los archivos JSON
    /// y llevarlas al programa.
    /// </summary>
    public class DeserializeManager 
    {
        private const string PathContainerCompany = @"..\..\..\..\..\docs\CompanyDataBase.json";
        private const string PathContainerPublication = @"..\..\..\..\..\docs\PublicationDataBase.json";
        private const string PathContainerToken = @"..\..\..\..\..\docs\TokenDataBase.json";
        private const string PathContainerSessionRelated = @"..\..\..\..\..\docs\UserDataBase.json";
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

        /// <summary>
        /// Método principal de la clase que se encarga de accionar los métodos de Deserialización y obtener sus
        /// resultados booleanos.
        /// </summary>
        /// <returns><c>True</c> en caso de que todo el proceso de deserialización este correcto, <c>False</c> en caso contrario.</returns>
        public bool DeserializeProgram()
        {
            bool conditionCompanies = this.DeserializeCompanies();
            bool conditionPublications = this.DeserializePublications();
            bool conditionToken = this.DeserializeToken();
            bool conditionSessionRelated = this.DeserializeSessionRelated();

            return conditionCompanies && conditionPublications && conditionToken && conditionSessionRelated;
        }

        private bool DeserializeCompanies()
        {
            if (File.Exists(PathContainerCompany))
            {
                string json = File.ReadAllText(PathContainerCompany);

                JsonSerializerOptions options = new ()
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true,
                };

                IList<Company> listCompanies = JsonSerializer.Deserialize<IList<Company>>(json, options);
                foreach (Company company in listCompanies)
                {
                    CompanySet.Instance.AddElement(company);
                }

                return true;
            }
            else
            {
                File.Create(PathContainerCompany);
                return false;
            }
        }

        private bool DeserializePublications()
        {
            if (File.Exists(PathContainerPublication))
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
            else
            {
                File.Create(PathContainerPublication);
                return false;
            }
        }

        private bool DeserializeToken()
        {
            if (File.Exists(PathContainerToken))
            {
                string json = File.ReadAllText(PathContainerToken);

                JsonSerializerOptions options = new () 
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true,
                };

                TokenGenerator tokenDeserialize = JsonSerializer.Deserialize<TokenGenerator>(json, options);
                TokenGenerator.Instance.tkn = tokenDeserialize.tkn;
                return true;
            }
            else
            {
                File.Create(PathContainerToken);
                return false;
            }
        }

        private bool DeserializeSessionRelated()
        {
            if (File.Exists(PathContainerSessionRelated))
            {
                string json = File.ReadAllText(PathContainerSessionRelated);

                JsonSerializerOptions options = new () 
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true,
                };

                SessionRelated sessionRelatedDeserialize = JsonSerializer.Deserialize<SessionRelated>(json, options);
                SessionRelated.Instance.AllUsers = sessionRelatedDeserialize.AllUsers;
                SessionRelated.Instance.DiccUserTokens = sessionRelatedDeserialize.DiccUserTokens;
                return true;
            }
            else
            {
                File.Create(PathContainerSessionRelated);
                return false;
            }
        }
    }
}
