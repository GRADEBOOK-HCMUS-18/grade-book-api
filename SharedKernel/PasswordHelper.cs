using System;
using System.Security.Cryptography;
using System.Text;

namespace SharedKernel
{
    public static class PasswordHelper
    {
        
        public static void HashPassword(string inputPassword, out byte[] salt, out byte[] hash)
        {
            if (inputPassword == null || string.IsNullOrWhiteSpace(inputPassword))
                throw new ArgumentException("Password is undefied");

            using (var randomhash = new HMACSHA512())
            {
                salt = randomhash.Key;
                hash = randomhash.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
            }
        }
    }
}