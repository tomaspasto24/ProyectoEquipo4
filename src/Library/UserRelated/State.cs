namespace Bot
{
    /// <summary>
    /// Indica los diferentes estados que puede tener un usuario.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Estado por defecto de un usuario.
        /// </summary>
        Start,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar un token para confirmar.
        /// </summary>
        ConfirmingToken,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar un rubro para confirmar.
        /// </summary>
        ConfirmingHeadingEntrepreneur,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar una ciudad para confirmar.
        /// </summary>
        ConfirmingCityEntrepreneur,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar una dirección para confirmar.
        /// </summary>
        ConfirmingAdressEntrepreneur,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar un nombre de una empresa para confirmar.
        /// </summary>
        ConfirmingCompanyName,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar un rubro de una empresa para confirmar.
        /// </summary>
        ConfirmingCompanyHeader,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar una dirección de una empresa para confirmar.
        /// </summary>
        ConfirmingCompanyAddress,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar una ciudad de una empresa para confirmar.
        /// </summary>
        ConfirmingCompanyCity,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar el contacto de una empresa para confirmar.
        /// </summary>
        ConfirmingCompanyContact,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar un nombre de una empresa para continuar con un proceso.
        /// </summary>
        AskingCompanyName,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario mientras edita la información de él mismo.
        /// </summary>
        AskingDataNumber,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar cambiando su ubicación.
        /// </summary>
        ChangingUserUbication,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar cambiando su rubro.
        /// </summary>
        ChangingUserHeader,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar modificando su lista de especializaciones.
        /// </summary>
        ChangingUserSpecializations,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar modificando su lista de certificaciones.
        /// </summary>
        ChangingUserCertifications,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar cambiando su dirección.
        /// </summary>
        ChangingUserAddress,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar cambiando su ciudad.
        /// </summary>
        ChangingUserCity,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar agregando especializaciones.
        /// </summary>
        AddingUserSpecializations,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar eliminando especializaciones.
        /// </summary>
        DeletingUserSpecializations,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar agregando certificaciones.
        /// </summary>
        AddingUserCertification,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar eliminando certificaciones.
        /// </summary>
        DeletingUserCertification,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar realizando una búsqueda de materiales.
        /// </summary>
        Searching,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar realizando una búsqueda con el filtro de materiales.
        /// </summary>
        SearchingByMaterial,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar realizando una búsqueda con el filtro de localización.
        /// </summary>
        SearchingByLocation,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al estar confirmando si está o no interesado en una publicación.
        /// </summary>
        InterestedInPublication,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar un nombre para la publicación que está creando.
        /// </summary>
        AskingPublicationName,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar una localización para la publicación que está creando.
        /// </summary>
        AskingCompanyLocation,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar un nombre para el material de la publicación que está creando.
        /// </summary>
        AskingMaterialName,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar una cantidad para el material de la publicación que está creando.
        /// </summary>
        AskingMaterialQuantity,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar un precio para el material de la publicación que está creando.
        /// </summary>
        AskingMaterialPrice,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar el nombre de un material para agregar a una publicación.
        /// </summary>
        AskingMaterialNameToAdd,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar la cantidad de un material para agregar a una publicación.
        /// </summary>
        AskingMaterialQuantityToAdd,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar el precio de un material para agregar a una publicación.
        /// </summary>
        AskingMaterialPriceToAdd,
        
        /// <summary>
        /// Estado en el que se encuentra un usuario al enviar el nombre de la publicación a la que se le quiere agregar un material.
        /// </summary>
        AddingMaterial
    }
}