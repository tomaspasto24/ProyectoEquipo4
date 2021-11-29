namespace Bot
{
    /// <summary>
    /// Indica los diferentes permisos que requiere un comando y a su vez, los permisos que pueden tener los diferentes usuarios.
    /// </summary>
    public enum Permission
    {
        /// <summary>
        /// Permiso que requieren los comandos destinados a que todos los usuarios puedan acceder a ellos.
        /// </summary>
        None,

        /// <summary>
        /// Permiso que requiere el comando para registrarse como usuario de empresa.
        /// </summary>
        Register,

        /// <summary>
        /// Permiso que requiere el comando para realizar una búsqueda como emprendedor.
        /// </summary>
        Search,

        /// <summary>
        /// Permiso que requiere el comando para obtener un reporte, como emprendedor, de las compras realizadas anteriormente.
        /// </summary>
        PurchasesReport,

        /// <summary>
        /// Permiso que requiere el comando para obtener un reporte, como usuario de empresa, de las ventas realizadas anteriormente.
        /// </summary>
        SalesReport,

        /// <summary>
        /// Permiso que requiere el comando para obtener el contacto de una empresa como emprendedor.
        /// </summary>
        ContactCompany,

        /// <summary>
        /// Permiso que requiere el comando para mostrar y modificar tus datos como emprendedor.
        /// </summary>
        Data,

        /// <summary>
        /// Permiso que requiere el comando para realizar una nueva publicación como usuario de empresa.
        /// </summary>
        Publish,

        /// <summary>
        /// Permiso que requiere el comando para generar un nuevo token para una nueva empresa como admin.
        /// </summary>
        GenerateToken,

        /// <summary>
        /// Permiso que requiere el comando para convertirse en emprendedor siendo un usuario default o nuevo.
        /// </summary>
        Undertake,

        /// <summary>
        /// Permiso que requiere el comando para agregar materiales a una publicación.
        /// </summary>
        AddMaterial
    }
}