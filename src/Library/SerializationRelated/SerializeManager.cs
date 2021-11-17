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

            return conditionCompanies && conditionPublications;
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
                return false;   
            }
        }
    }
}
