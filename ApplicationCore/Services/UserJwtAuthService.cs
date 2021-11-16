using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SharedKernel;

namespace ApplicationCore.Services
{
    public class UserJwtAuthService : IUserJwtAuthService
    {
        private readonly ILogger<UserJwtAuthService> _logger;
        private readonly IBaseRepository<User> _repository;

        public UserJwtAuthService(
            ILogger<UserJwtAuthService> logger,
            IBaseRepository<User> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public string TryGetToken(string email, string password)
        {
            var foundUser =
                _repository.GetFirst(user => user.Email == email);
            if (foundUser is null)
                return null;
            var success = CheckPasswordHash(password, foundUser.PasswordHash, foundUser.PasswordSalt);

            if (!success)
                return null;

            return GenerateJwtToken(foundUser);
        }

        public string TryGetTokenWithoutPassword(string email)
        {
            var foundUser =
                _repository.GetFirst(user => user.Email == email);
            if (foundUser is null)
                return null; 
            return GenerateJwtToken(foundUser);
        }

        public User CreateNewUser(string password, string email, string firstName, string lastName,
            string profilePictureUrl, string defaultProfilePictureHex)
        {
            var foundUser = _repository.GetFirst(user => user.Email == email);

            if (foundUser is not null) throw new ApplicationException("Existed user");
            var userToAdd = new User();
            userToAdd.Email = email;
            userToAdd.FirstName = firstName;
            userToAdd.LastName = lastName;
            userToAdd.ProfilePictureUrl = profilePictureUrl;
            userToAdd.DefaultProfilePictureHex = defaultProfilePictureHex;
            PasswordHelper.HashPassword(password, out var newPasswordSalt, out var newPasswordHash);

            userToAdd.PasswordSalt = newPasswordSalt;
            userToAdd.PasswordHash = newPasswordHash;

            _repository.Insert(userToAdd);

            return userToAdd;
        }


        private bool CheckPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Input password is empty");
            using (var hmacsha512 = new HMACSHA512(storedSalt))
            {
                var passwordHashToCheck = hmacsha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (var i = 0; i < passwordHashToCheck.Length; i++)
                    if (passwordHashToCheck[i] != storedHash[i])
                    {
                        Console.WriteLine("Wrong password");
                        return false;
                    }
            }

            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var hourToRefresher = 48;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3aacfb02-b67b-4923-8a2d-21a103902b91"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddHours(hourToRefresher)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}