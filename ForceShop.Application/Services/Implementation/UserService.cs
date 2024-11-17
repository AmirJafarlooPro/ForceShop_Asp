using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Application.EmailHtml;
using ForceShop.Application.Extensions;
using ForceShop.Application.Generators;
using ForceShop.Application.Services.Interfaces;
using ForceShop.Domian.Interfaces.User;
using ForceShop.Domian.Models.User;
using ForceShop.Domian.ViewModels.User;
using static System.Net.Mime.MediaTypeNames;

namespace ForceShop.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        #region DependecyInjection

        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;

        public UserService(IUserRepository userRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        #endregion

        //---------------------------------------------------

        #region Register

        public async Task<ResultRegister> RegisterUserAsync(RegisterViewModel UserViewModel)
        {
            bool CheckEmail = await _userRepository.IsExistByEmailAsync(UserViewModel.UserEmail.Trim().ToLower());

            if (!CheckEmail)
            {
                User User = new User()
                {
                    IsAdmin = false,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    UserName = UserViewModel.UserName,
                    UserEmail = UserViewModel.UserEmail,
                    Password = UserViewModel.Password.ToMd5(),
                };

                bool Result = await _userRepository.AddUserAsync(User);

                if (Result == true)
                {
                    await _userRepository.SaveAsync();
                    return ResultRegister.Succses;
                }
                else
                {
                    return ResultRegister.Failed;
                }

            }
            else
            {
                return ResultRegister.EmailInvalid;
            }
        }


        #endregion

        #region login

        public async Task<ResultLogin> LoginUserAsync(LoginViewModel userViewModel)
        {
            if (userViewModel != null)
            {
                var user = await _userRepository.GetActiveUserByEmailAsync(userViewModel.UserEmail.ToLower().Trim());

                if (user != null)
                {
                    // بررسی رمز عبور با استفاده از VerifyPassword
                    if (user.Password == userViewModel.Password.ToMd5())
                    {
                        return ResultLogin.Succses; // ورود موفق
                    }
                    else
                    {
                        return ResultLogin.UserNotFound; // رمز عبور نادرست
                    }
                }
                else
                {
                    return ResultLogin.UserNotFound; // کاربر پیدا نشد
                }
            }
            else
            {
                return ResultLogin.Failed; // ورودی نامعتبر
            }
        }


        #endregion

        #region ForgotPassword

        public async Task<ResultForgotPassword> ForgotPassword(ForgotPasswordViewModel model)
        {
            var User = await _userRepository.GetActiveUserByEmailAsync(model.UserEmail.ToLower().Trim());
            
            if (User == null)
            {
                return ResultForgotPassword.UserNotFound;
            }
            else
            {
                var code = CodeGenerators.GetUnigueCode();

                User.ConfirmCode = code;

                _userRepository.UpdateUser(User);

                await _userRepository.SaveAsync();


                string body = SenderHtml.RenderSendVerifyCodeForResetPass(User.UserName, User.ConfirmCode);

                await _emailSender.SendEmailAsync(User.UserEmail, "کد تایید", body);

                return ResultForgotPassword.Succses;
            }
        }

        #endregion

        #region ResendCode

        //public async Task<ResultResendCode> ResendCode(string email)
        //{
        //    var User = await _userRepository.GetUserByEmailAsync(email.ToLower().Trim());

        //    if (User == null)
        //    {
        //        return ResultResendCode.failed;
        //    }
        //    else
        //    {
        //        var code = CodeGenerators.GetUnigueCode();

        //        User.ConfirmCode = code;

        //        _userRepository.UpdateUser(User);

        //        await _userRepository.SaveAsync();


        //        string body = SenderHtml.RenderSendVerifyCodeForResetPass(User.UserName, User.ConfirmCode);

        //        await _emailSender.SendEmailAsync(User.UserEmail, "کد تایید", body);

        //        return ResultResendCode.Succses;

        //    }
        //}

        #endregion

        #region ResetPassword
        public async Task<ResultResetPassword> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userRepository.GetActiveUserByEmailAsync(model.Email);

            if (user != null)
            {
                user.Password = model.NewPassword.ToMd5();

                user.ConfirmCode = "";

                var result = _userRepository.UpdateUser(user);

                if (result)
                {
                    await _userRepository.SaveAsync();
                    return ResultResetPassword.Succses;
                }
                else
                {
                    return ResultResetPassword.Failed;
                }
            }
            else
            {
                return ResultResetPassword.Failed;
            }

        }

        public async Task<ResultChnagePassword> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userRepository.GetActiveUserByEmailAsync(model.Email);

            if (user != null)
            {
                if (user.Password == model.OldPassword.ToMd5())
                {
                    user.Password = model.NewPassword.ToMd5();

                    user.ConfirmCode = "";

                    var result = _userRepository.UpdateUser(user);

                    if (result)
                    {
                        await _userRepository.SaveAsync();
                        return ResultChnagePassword.Succses;
                    }
                    else
                    {
                        return ResultChnagePassword.Failed;
                    }
                }
                else
                {
                    return ResultChnagePassword.oldpassInvalid;
                }
            }
            else
            {
                return ResultChnagePassword.Failed;
            }
        }
        #endregion


        #region ValidateConfirmCode

        public async Task<ResultOTPEmail> ValidateConfirmCode(OTPVerifyViewModel model)
        {
            var result = await _userRepository.IsSucsessConfirmCodeByEmailAsync(model.Email.ToLower().Trim(), model.VerifyCode);

            if (result)
            {
                return ResultOTPEmail.Succses;
            }
            else
            {
                return ResultOTPEmail.InavildCode;
            }
        }

        #endregion

        public async Task<User> GetUserByEmailAsync(string UserEmail)
        {
            return await _userRepository.GetUserByEmailAsync(UserEmail.ToLower().Trim());
        }

        public int GetUserIDByEmail(string email)
        {
            return _userRepository.GetUserIDByEmail(email);
        }


    }
}
