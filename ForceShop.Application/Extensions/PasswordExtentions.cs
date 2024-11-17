using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sodium;
using Isopoh.Cryptography.Argon2;
using Konscious.Security.Cryptography;





namespace ForceShop.Application.Extensions
{
    public static class PasswordExtensions
    {
        public static string ToSha512(this string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // تبدیل هر بایت به هگزادسیمال
                }
                return sb.ToString();
            }
        }

        public static string ToMd5(this string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // تبدیل هر بایت به هگزادسیمال
                }
                return sb.ToString();
            }
        }
    }
}


