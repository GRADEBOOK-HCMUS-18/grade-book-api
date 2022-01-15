using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SharedKernel
{
    public static class PasswordHelper
    {
        public static string GenerateJwtToken(int userId, string emailOrUsername)
        {
            var hourToRefresher = 48;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3aacfb02-b67b-4923-8a2d-21a103902b91"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID", userId.ToString()),
                    new Claim(ClaimTypes.Email, emailOrUsername)
                }),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddHours(hourToRefresher)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        public static string GenerateRandomLetterString()
        {
            var resultGuid = string.Concat(Guid.NewGuid().ToString().Select(c => (char) (c + 17)));
            return resultGuid.Substring(resultGuid.Length - 12);
        }
        public static void HashPassword(string inputPassword, out byte[] salt, out byte[] hash)
        {
            if (inputPassword is null)
                throw new ArgumentException("Password is undefied");

            using (var randomhash = new HMACSHA512())
            {
                salt = randomhash.Key;
                hash = randomhash.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
            }
        }

        public static bool CheckPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
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
    }
}