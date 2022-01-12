using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedKernel
{
    public static class PasswordHelper
    {
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