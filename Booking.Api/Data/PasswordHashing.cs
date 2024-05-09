using System;
using System.Security.Cryptography;
using System.Text;

namespace Booking.Api.Data
{
    public class PasswordHashing
    {
        public static (string hashedPassword, byte[] salt) HashPassword(string password)
        {
            byte[] salt = GenerateSalt();

            string hashedPassword = ComputeHash(password, salt);

            return (hashedPassword, salt);
        }

        public static bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            string computedHash = ComputeHash(password, salt);

            return string.Equals(computedHash, hashedPassword);
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static string ComputeHash(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
