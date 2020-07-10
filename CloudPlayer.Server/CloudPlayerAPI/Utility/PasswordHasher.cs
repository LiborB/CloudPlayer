using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CloudPlayerAPI.Utility
{
    public class PasswordAndSalt
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
    }
    public static class PasswordHasher
    {
        public static byte[] GeneratePasswordSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
        public static string GeneratePasswordHash(string password, byte[] salt)
        {
 
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                256 / 8));
            return hashed;
        }
        public static PasswordAndSalt GeneratePasswordHashAndSalt(string password)
        {
            byte[] salt = GeneratePasswordSalt();
 
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                256 / 8));
            return new PasswordAndSalt()
            {
                Hash = hashed,
                Salt = salt
            };
        }

        public static bool isPasswordCorrect(string enteredPassword, string hashedPassword, byte[] salt)
        {
            var passwordHash = GeneratePasswordHash(enteredPassword, salt);
            return passwordHash == enteredPassword;

        }
    }
}