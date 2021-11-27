using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase encargada de representar al usuario (componiendo name, id y role). Esta cumple con el patron SRP y Expert.
    /// </summary>
    public class UserInfo
    {
        public static List<Permission> AdminPermissions = new List<Permission>()
        {
            Permission.None,
            Permission.GenerateToken,
        };

        public static List<Permission> DefaultPermissions = new List<Permission>()
        {
            Permission.None,
            Permission.Register,
            Permission.Undertake,
        };

        public static List<Permission> UserCompanyPermissions = new List<Permission>()
        {
            Permission.None,
            Permission.SalesReport,
            Permission.Publish,
            Permission.AddMaterial,
        };

        public static List<Permission> EntrepreneurPermissions = new List<Permission>()
        {
            Permission.None,
            Permission.Register, // TODO preguntar cambio de emprendedor -> userCompany
            Permission.Search,
            Permission.PurchasesReport,
            Permission.ContactCompany,
            Permission.Data,
        };

        public List<Permission> Permissions { get; set; } = new List<Permission>();
        public string Name { get; set; }
        public long Id { get; set; }
        public State HandlerState { get; set; }

        [JsonConstructor]
        public UserInfo() { }
        /// <summary>
        /// Método constructor de la clase User que se encarga de asignar los atributos
        /// name, id y role que usará la clase.
        /// </summary>
        /// <param name="name">El nombre del usuario.</param>
        /// <param name="id">El id del usuario.</param>
        /// <param name="role">El role del usuario</param>
        public UserInfo(string name, long id)
        {
            this.Name = name;
            this.Id = id;
            this.Permissions = UserInfo.DefaultPermissions;
        }
        public bool HasPermission(Permission permission)
        {
            return this.Permissions.Contains(permission);
        }
    }
}