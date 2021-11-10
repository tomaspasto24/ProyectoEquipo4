namespace Bot
{
    /*
    Patrones y principios:
    Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP.
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    */
    /// <summary>
    /// Clase UserRelated que contiene informacion acerca del usuario.
    /// </summary>
    public class UserRelated
    {
        /// <summary>
        /// Almacena el canal de interaccion con el bot
        /// </summary>
        /// <value>Canal de interaccion</value>
        public AbstractBot Channel { get; set; }
        /// <summary>
        /// Almacena el usuario
        /// </summary>
        /// <value>Usuario</value>
        public User User { get; set; }

        private static UserRelated instance;
        /// <summary>
        /// Metodo getter para instanciar instance en caso de que sea null para tener una unica instancia de la clase y que sea de acceso global.
        /// </summary>
        /// <value>La instancia inicializada</value>
        public static UserRelated Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserRelated();
                }
                return instance;
            }
        }

        /// <summary>
        /// Constructor de la clase UserRelated.
        /// </summary>
        private UserRelated()
        {
            this.Channel = null;
            this.User = null;
        }
    }
}