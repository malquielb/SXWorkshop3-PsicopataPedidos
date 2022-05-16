using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Identity.Services
{
    public static class EncryptionService
    {
        public static EncryptedPassword EncryptPassword(string password)
        {
            var encryptedPassword = new EncryptedPassword();

            using(var hmac = new HMACSHA512())
            {
                encryptedPassword.PasswordSalt = hmac.Key;
                encryptedPassword.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return encryptedPassword;
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeddHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeddHash.SequenceEqual(passwordHash);
            }
        }
    }

    public struct EncryptedPassword
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
