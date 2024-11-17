using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Domian.ViewModels.User
{
    public class OTPVerifyViewModel
    {
        [Required]
        public string Email { get; set; }

        #region VerifyCode

        [Required(ErrorMessage = "لطفا کد تایید را وارد کنید")]
        public string VerifyCode { get; set; }

        #endregion


    }
    public enum ResultOTPEmail
    {
        Succses,
        InavildCode
    }
}
