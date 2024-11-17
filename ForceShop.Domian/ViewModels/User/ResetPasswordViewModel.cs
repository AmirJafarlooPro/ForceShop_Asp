using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Domian.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        #region Password

        [DataType(DataType.Password)]
        [Display(Name = "گذرواژه جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NewPassword { get; set; }

        #endregion

        #region RePassword

        [DataType(DataType.Password)]
        [Display(Name = "تکرار گذرواژه جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Compare("NewPassword", ErrorMessage = "گذرواژه ها باهم مغایرت دارند")]
        public string ReNewPassword { get; set; }


        #endregion

        public string Email { get; set; }
    }
    public class ChangePasswordViewModel
    {
        #region OldPassword

        [DataType(DataType.Password)]
        [Display(Name = "گذرواژه فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string OldPassword { get; set; }

        #endregion

        #region Password

        [DataType(DataType.Password)]
        [Display(Name = "گذرواژه جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NewPassword { get; set; }

        #endregion

        #region RePassword

        [DataType(DataType.Password)]
        [Display(Name = "تکرار گذرواژه جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Compare("NewPassword", ErrorMessage = "گذرواژه ها باهم مغایرت دارند")]
        public string ReNewPassword { get; set; }


        #endregion

        public string Email { get; set; }
    }
    public enum ResultResetPassword
    {
        Succses,
        Failed,
    }
    public enum ResultChnagePassword
    {
        Succses,
        Failed,
        oldpassInvalid,
    }
}
