using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utility
{
    public static class PasswordHashing
    {
        private const int SALT_SIZE = 64; // size in bytes
        private const int HASH_SIZE = 64; // size in bytes
        private const int ITERATIONS = 100000; // number of pbkdf2 iterations

        public static (byte[] hash, Byte[] salt) CreateHash(string input)
        {
            // Generate a salt
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_SIZE];
            provider.GetBytes(salt);

            // Generate and return the hash
            return (Hash(input, salt), salt);
        }

        public static byte[] Hash(string input, byte[] salt)
        {
            // SHA512 is chosen along with a sizeable amount of iterations to ensure protection against brute force attacks
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, ITERATIONS, HashAlgorithmName.SHA512);
            return pbkdf2.GetBytes(HASH_SIZE);
        }

    }
}
