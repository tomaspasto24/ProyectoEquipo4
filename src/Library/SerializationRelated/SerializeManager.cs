using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Clase serializadora de los objetos que se desean persistir fuera del bot.
    /// </summary>
    public class SerializeManager
    {
        private const string Path = @"..\..\docs\DataBase.json";
        private static JsonSerializerOptions options = new ()
        {
            ReferenceHandler = MyReferenceHandler.Instance,
            WriteIndented = true,
        };
        private static SerializeManager instance;
        private List<Publication> listPublicationsToSerialize = new List<Publication>();
        private Dictionary<string, Company> diccUserCompanyTokensToSerialize = new Dictionary<string, Company>();


        /// <summary>
        /// Constructor sin implementación y público (a diferencia de las demás clases Singleton) para poder ser usado por
        /// el atributo JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public SerializeManager() { }

        /// <summary>
        /// Obtiene o establece la instancia de la clase SerializeManager para cumplir con el patrón creacional Singleton (Ver Readme).
        /// </summary>
        /// <value>Instancia SerializeManager.</value>
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

            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la lista de publicaciones calco a la lista de publicaciones de PublicationSet, para ser Serializada/Deserializada.
        /// </summary>
        /// <value>Lista Publicación.</value>
        [JsonInclude]
        public List<Publication> ListPublicationsToSerialize
        {
            get
            {
                return this.listPublicationsToSerialize;
            }
            set
            {
                this.listPublicationsToSerialize = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el diccionario de tokens y empresas, para ser Serializada/Deserializada.
        /// </summary>
        /// <value>Diccionario string, Company.</value>
        [JsonInclude]
        public Dictionary<string, Company> DiccUserCompanyTokensToSerialize
        {
            get
            {
                return this.diccUserCompanyTokensToSerialize;
            }

            set
            {
                this.diccUserCompanyTokensToSerialize = value;
            }
        }

        /// <summary>
        /// Método que se encarga de traer los objetos que se desean persistir para poder guardarlos en el archivo DataBase.json
        /// contenida en la carpeta Docs.
        /// </summary>
        public void SerializeObjects()
        {
            this.listPublicationsToSerialize.Clear();
            this.diccUserCompanyTokensToSerialize.Clear();
            try
            {
                this.listPublicationsToSerialize = PublicationSet.Instance.ListPublications as List<Publication>;
                this.diccUserCompanyTokensToSerialize = SessionRelated.Instance.DiccUserTokens as Dictionary<string, Company>;
                string json = JsonSerializer.Serialize(SerializeManager.Instance, options);
                File.WriteAllText(Path, json);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Un argumento ingresado no es compatible, error: {e.Message}");
            }
        }

        /// <summary>
        /// Método que se encarga de leer el archivo DataBase.json y deserializar los objetos que se quieren persistir para setear
        /// en sus clases respectivas.
        /// </summary>
        public void DeserializeObjects()
        {
            string json = File.ReadAllText(Path);
            if (string.IsNullOrEmpty(json))
            {
                System.Console.WriteLine("No hay ningún dato en el archivo JSON correspondiente, la deserialización no se ejecuta.");
            }
            else
            {
                this.listPublicationsToSerialize.Clear();
                this.diccUserCompanyTokensToSerialize.Clear();
                try
                {
                    SerializeManager manager = JsonSerializer.Deserialize<SerializeManager>(json, options);
                    Console.WriteLine(manager.listPublicationsToSerialize.Count);
                    PublicationSet.Instance.ListPublications = manager.listPublicationsToSerialize;
                    Console.WriteLine(manager.listPublicationsToSerialize.Count);
                    SessionRelated.Instance.DiccUserTokens = manager.diccUserCompanyTokensToSerialize;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"Un argumento ingresado es de tipo null, error: {e.Message}");
                }
            }
        }
    }
}