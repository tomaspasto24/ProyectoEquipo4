using System.IO;

namespace Bot
{
    /// <summary>
    /// Clase que se encarga de administrar la serialización, es decir, guardar todos las clases que necesiten
    /// persistencia en formato JSON en su respectivo archivo contenedor.
    /// </summary>
    public class SerializeManager 
    {
        private const string PathContainerCompany = @"..\..\..\..\..\docs\CompanyDataBase.json";
        private const string PathContainerPublication = @"..\..\..\..\..\docs\PublicationDataBase.json";
        private const string PathContainerToken = @"..\..\..\..\..\docs\TokenDataBase.json";
        private const string PathContainerAllUsers = @"..\..\..\..\..\docs\UserDataBase.json";
        private const string PathContainerDiccUserTokens = @"..\..\..\..\..\docs\DiccUserTokensDataBase.json";
        private static SerializeManager instance;

        private SerializeManager() { }
        
        /// <summary>
        /// Obtiene el acceso a la propia instancia de la clase SerializeManager,
        /// en caso de que la variable _instance no este creada, la crea y la retorna. En caso 
        /// contrario de que anteriormente este creada simplemente la retorna, asi se asegura de que
        /// siempre se use la misma variable instancia.
        /// </summary>
        /// <returns>Instancia Deserialize.</returns>
        public static SerializeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SerializeManager();
                }

                return instance;
            }
        }

        /// <summary>
        /// Método principal de la clase que se encarga de accionar los métodos de Serialización y obtener sus
        /// resultados booleanos.
        /// </summary>
        /// <returns><c>True</c> en caso de que todo el proceso de serialización este correcto, <c>False</c> en caso contrario.</returns>
        public bool SerializeProgram()
        {
            bool conditionCompanies = this.SerializeCompanies();
            bool conditionPublications = this.SerializePublications();
            bool conditionToken = this.SerializeToken();
            bool conditionSessionRelated = this.SerializeSessionRelated();

            return conditionCompanies && conditionPublications && conditionToken && conditionSessionRelated;
        }

        private bool SerializeCompanies()
        {
            if (File.Exists(PathContainerCompany))
            {
                string jsonToSave = CompanySet.Instance.ConvertObjectToSaveToJson();
                File.WriteAllText(PathContainerCompany, jsonToSave);
                return true;
            }
            else
            {
                File.Create(PathContainerCompany);
                return false;
            }
        }

        private bool SerializePublications()
        {
            if (File.Exists(PathContainerPublication))
            {
                string jsonToSave = PublicationSet.Instance.ConvertObjectToSaveToJson();
                File.WriteAllText(PathContainerPublication, jsonToSave);
                return true;
            }
            else
            {
                File.Create(PathContainerPublication);
                return false;   
            }
        }

        private bool SerializeToken()
        {
            if (File.Exists(PathContainerToken))
            {
                string jsonToSave = TokenGenerator.Instance.ConvertObjectToSaveToJson();
                File.WriteAllText(PathContainerToken, jsonToSave);
                return true;
            }
            else
            {
                File.Create(PathContainerToken);
                return false;   
            }
        }

        private bool SerializeSessionRelated()
        {
            if (File.Exists(PathContainerAllUsers) && File.Exists(PathContainerDiccUserTokens))
            {
                string jsonAllUsersToSave;
                string jsonDiccUserTokensToSave;
                (jsonAllUsersToSave, jsonDiccUserTokensToSave) = SessionRelated.Instance.ConvertObjectToSaveToJson();
                File.WriteAllText(PathContainerAllUsers, jsonAllUsersToSave);
                File.WriteAllText(PathContainerDiccUserTokens, jsonDiccUserTokensToSave);
                return true;
            }
            else
            {
                return false;   
            }
        }
    }
}
