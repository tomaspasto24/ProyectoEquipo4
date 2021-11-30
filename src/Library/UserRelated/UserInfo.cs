using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Clase contenedora de la información del usuario.
    /// Patrones y principios:
    /// Esta cumple con el patron SRP, ya que la unica razon de cambio que podría tener la clase, sería cambiar la forma
    /// de guardar la información del usuario.
    /// Cumple con Expert ya que, es experta en el manejo de la información de un usuario.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Lista de permisos que tiene un administrador.
        /// </summary>
        /// <typeparam name="Permission">Permisos.</typeparam>
        [JsonInclude]
        public static List<Permission> AdminPermissions = new List<Permission>()
        {
            Permission.None,
            Permission.GenerateToken,
        };

        /// <summary>
        /// Lista de permisos que tiene un usuario default.
        /// </summary>
        /// <typeparam name="Permission">Permisos.</typeparam>
        [JsonInclude]
        public static List<Permission> DefaultPermissions = new List<Permission>()
        {
            Permission.None,
            Permission.Register,
            Permission.Undertake,
        };

        /// <summary>
        /// Lista de permisos que tiene un usuario de empresa.
        /// </summary>
        /// <typeparam name="Permission">Permisos.</typeparam>
        [JsonInclude]
        public static List<Permission> UserCompanyPermissions = new List<Permission>()
        {
            Permission.None,
            Permission.SalesReport,
            Permission.Publish,
            Permission.AddMaterial,
        };

        /// <summary>
        /// Lista de permisos que tiene un emprendedor.
        /// </summary>
        /// <typeparam name="Permission">Permisos.</typeparam>
        [JsonInclude]
        public static List<Permission> EntrepreneurPermissions = new List<Permission>()
        {
            Permission.None,
            Permission.Search,
            Permission.PurchasesReport,
            Permission.ContactCompany,
            Permission.Data,
        };

        /// <summary>
        /// Lista que contiene los permisos que tiene el usuario.
        /// </summary>
        /// <typeparam name="Permission">Permisos.</typeparam>
        [JsonInclude]
        public List<Permission> Permissions { get; set; } = new List<Permission>();
        
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        /// <value>Nombre.</value>
        public string Name { get; set; }
        
        /// <summary>
        /// Id del usuario.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Constructor UserInfo sin implementación para poder ser utilizado por la etiqueta JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public UserInfo() { }

        /// <summary>
        /// Crea una nueva instancia de la clase UserInfo, asignando su nombre y el id.
        /// </summary>
        /// <param name="name">Nombre del usuario en cuestión.</param>
        /// <param name="id">Id del usuario en cuestión.</param>
        public UserInfo(string name, long id)
        {
            this.Name = name;
            this.Id = id;
            this.Permissions = UserInfo.DefaultPermissions;
        }

        /// <summary>
        /// Estado del usuario que cambia según los comandos que utilice.
        /// </summary>
        public State HandlerState { get; set; }

        /// <summary>
        /// Verifica si un usuario tiene o no cierto permiso.
        /// </summary>
        /// <param name="permission">El permiso en cuestión.</param>
        /// <returns>true si tiene el permiso, falso en caso contrario.</returns>
        public bool HasPermission(Permission permission)
        {
            return this.Permissions.Contains(permission);
        }
    }
}