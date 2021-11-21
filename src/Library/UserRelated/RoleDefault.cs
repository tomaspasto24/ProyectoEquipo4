using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase RoleAdmin que implementa la interfaz IRole
    /// </summary>
    public class RoleDefault : IRole
    {

        private List<Permission> permissions = new List<Permission>(){
            Permission.None,
            Permission.Register,
            Permission.Undertake,
        };

        
        public bool HasPermission(Permission perm)
        {
            return this.permissions.Contains(perm);
        }


        public override string ToString()
        {
            return "Default";
        }
    }
}