using System.IO;
using System;

namespace Bot
{
    /// <summary>
    /// Clase que se encarga de administrar la Serialización, es decir, extraer las instancias a persistir
    /// y grabarlas en archivos JSON. Cumple con el patrón de diseño SRP porque es la única responsabilidad de la clase, a tal punto
    /// de que el único método publico es SerializeProgram.
    /// </summary>
    public class SerializeManager 
    {
        private const string PathContainerPublication = @"..\..\docs\PublicationDataBase.json";
        private const string PathContainerAllUsers = @"..\..\docs\UserDataBase.json";
        private const string PathContainerDiccUserTokens = @"..\..\docs\DiccUserTokensDataBase.json";
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

        public bool SerializeSessionRelated()
        {
            try
            {
                string jsonAllUsersToSave;
                string jsonDiccUserTokensToSave;
                (jsonAllUsersToSave, jsonDiccUserTokensToSave) = SessionRelated.Instance.ConvertObjectToSaveToJson();
                File.WriteAllText(PathContainerAllUsers, jsonAllUsersToSave);
                File.WriteAllText(PathContainerDiccUserTokens, jsonDiccUserTokensToSave);
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool SerializePublications()
        {
            try
            {
                string jsonToSave = PublicationSet.Instance.ConvertObjectToSave();
                File.WriteAllText(PathContainerPublication, jsonToSave);
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
