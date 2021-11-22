namespace Bot
{
    /// <summary>
    /// Indica los diferentes estados que puede tener el comando RegisterHandler.
    /// - Start: El estado inicial del comando. En este estado el comando pide el token de registro
    /// - ConfirmingToken: Luego de pedir el token. En este estado el comando valida si el token ingresado existe y vuelve al estado Start.
    /// </summary>
    public enum State
    {
        /// Estado antes de mandar el token
        Start,
        /// Estado mientras el bot espera y confirma un token
        ConfirmingToken,
        ConfirmingHeadingEntrepreneur,
        ConfirmingCityEntrepreneur,
        ConfirmingAdressEntrepreneur,
        ConfirmingCompanyName,
        ConfirmingCompanyHeader,
        ConfirmingCompanyAddress,
        ConfirmingCompanyCity,
        ConfirmingCompanyContact,
        AskingCompanyName,
        AskingDataNumber,
        ChangingUserUbication,
        ChangingUserHeader,
        ChangingUserSpecializations,
        ChangingUserCertifications,
        ChangingUserAddress,
        ChangingUserCity,
        AddingUserSpecializations,
        DeletingUserSpecializations,
        AddingUserCertification,
        DeletingUserCertification,
        Searching,
        SearchingByMaterial,
        SearchingByLocation,
        InterestedInPublication,
        AskingPublicationName,
        AskingCompanyLocation,
        AskingMaterialName,
        AskingMaterialQuantity,
        AskingMaterialPrice,
        AskingMaterialNameToAdd,
        AskingMaterialQuantityToAdd,
        AskingMaterialPriceToAdd,
        AddingMaterial
        
    }
}