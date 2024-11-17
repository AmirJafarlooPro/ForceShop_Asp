
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Domian.ViewModels.User
{
    public class RegisterViewModel
    {
        #region UserName

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        #endregion

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

        #region RePassword

        [DataType(DataType.Password)]
        [Display(Name = "تکرار گذرواژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Compare("Password", ErrorMessage = "گذرواژه ها باهم مغایرت دارند")]
        public string RePassword { get; set; }

        #endregion

        public bool AcceptRoles { get; set; }
    }

    public enum ResultRegister
    {
        Succses,
        Failed,
        EmailInvalid
    }
}
