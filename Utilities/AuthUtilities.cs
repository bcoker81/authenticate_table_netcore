using System;
using System.Security.Cryptography;
using System.Text;

namespace testmysql.Utilities
{
    public class AuthUtilities
    {
        public static string HashPassword(string password){
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-","").ToLower();
                return hash;
            }
        }
    }
}