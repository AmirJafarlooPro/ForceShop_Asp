using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Application.Generators
{
    public  class CodeGenerators
    {
        public static string GetUnigueCode()
        {
            Random rnd = new Random();

            int Code = rnd.Next(100_000, 999_999);

            return Code.ToString();
        }
    }
}
