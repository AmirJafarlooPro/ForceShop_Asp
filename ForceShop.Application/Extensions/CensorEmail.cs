using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Application.Extensions
{
    public static class CensorEmail
    {
        public static string ToCensorEmail(this string email, int charsToCensor)
        {
            if (charsToCensor < 0 || charsToCensor > email.Length)
                throw new ArgumentOutOfRangeException(nameof(charsToCensor), "Invalid number of characters to censor.");

            // سانسور کاراکترها
            string censoredPart = new string('*', charsToCensor);
            string remainingPart = email.Substring(charsToCensor);

            return censoredPart + remainingPart;
        }
    }
}
