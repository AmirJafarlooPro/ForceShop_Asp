using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Domian.ViewModels.User
{
    public class ResendCodeViewModel
    {
        public string UserEmail { get; set; }
    }
    public enum ResultResendCode
    {
        Succses,
        failed
    }
}
