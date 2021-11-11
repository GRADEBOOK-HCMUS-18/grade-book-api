using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
   
        public interface IUserJwtAuthService
        {
            string TryGetToken(string usernameOrEmail, string password);

            User CreateNewUser(string username, string password, string email, string firstName, string lastName); 
        
        } 
}