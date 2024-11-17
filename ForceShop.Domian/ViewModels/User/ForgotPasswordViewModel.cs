using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Domian.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست ")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserEmail { get; set; }


    }
    public enum ResultForgotPassword
    {
        Succses,
        UserNotFound
    }
}
