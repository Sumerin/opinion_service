using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Web;
using System.Text;

namespace opinion_Service.Services
{
    public static class Security
    {
        public static string Encrypte(String passwd, out String salt)
        {
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            salt = Convert.ToBase64String(saltBytes);

            var sha = SHA256.Create();
            var saltedPassword = passwd + salt;
            var saltedHashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));

            return saltedHashedPassword;

        }
        public static bool DecrypteAndCheck(String passwd, String salt, String saltedHashedPassword)
        {
            var sha = SHA256.Create();
            var saltedPassword = passwd + salt;
            var saltedHashedPasswordDataBase = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));

            return saltedHashedPassword.Equals(saltedHashedPasswordDataBase);
        }
    }
}