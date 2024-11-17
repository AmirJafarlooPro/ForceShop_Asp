using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Domian.ViewModels.User
{
    public class LoginViewModel
    {
        #region Email

        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست ")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserEmail { get; set; }

        #endregion

        #region Password

        [DataType(DataType.Password)]
        [Display(Name = "گذرواژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        #endregion

        public bool? Error { get; set; }

        public bool IsPersistent { get; set; }

    }
    public enum ResultLogin
    {
        Succses,
        UserNotFound,
        Failed,
    }
}
