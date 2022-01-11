using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IUserJwtAuthService
    {
        string TryGetToken(string email, string password);

        // for google authentication / trust frontend for now
        string TryGetTokenWithoutPassword(string email);

        User CreateNewUser(string password, string email, string firstName, string lastName,
            string profilePictureUrl, string defaultProfilePictureHex);

        AccountConfirmationRequest CreateNewConfirmationRequest(string email);
    }
}