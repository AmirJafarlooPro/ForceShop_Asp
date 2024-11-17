using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.User;
using ForceShop.Domian.ViewModels.User;

namespace ForceShop.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ResultRegister> RegisterUserAsync(RegisterViewModel UserViewModel);

        public Task<ResultLogin> LoginUserAsync(LoginViewModel UserViewModel);

        Task<User> GetUserByEmailAsync(string UserEmail);
        public Task<ResultForgotPassword> ForgotPassword(ForgotPasswordViewModel model);

        public Task<ResultResetPassword> ResetPassword(ResetPasswordViewModel model);

        public Task<ResultOTPEmail> ValidateConfirmCode(OTPVerifyViewModel model);
        public Task<ResultChnagePassword> ChangePassword(ChangePasswordViewModel model);

        public int GetUserIDByEmail(string email);

        //public Task<ResultResendCode> ResendCode(string email);
    }
}
