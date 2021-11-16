using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IUserJwtAuthService
    {
        string TryGetToken(string email, string password);

        User CreateNewUser( string password, string email, string firstName, string lastName,
            string profilePictureUrl, string defaultProfilePictureHex);
    }
}