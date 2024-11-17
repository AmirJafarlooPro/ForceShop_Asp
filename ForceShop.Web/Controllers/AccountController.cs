using System.Security.Claims;
using ForceShop.Application.Services.Interfaces;
using ForceShop.Domian.Models.User;
using ForceShop.Domian.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ForceShop.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Dependency Injection

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        //--------------------------------------------

        #region Login 

        [Route("Login")]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            TempData["PageName"] = "ورود";
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel UserViewModel)
        {
            TempData["PageName"] = "ورود";

            if (!ModelState.IsValid)
            {
                return View(UserViewModel);
            }
            else
            {
                var Result = await _userService.LoginUserAsync(UserViewModel);
                switch (Result)
                {
                    case ResultLogin.Failed:
                        ModelState.AddModelError("Error", "متاسفانه  عملیات با خطا مواجه شد");
                        return View(UserViewModel);
                        break;

                    case ResultLogin.UserNotFound:
                        ModelState.AddModelError("Error", "اطلاعات وارد شده صحیح نمیباشد");
                        return View(UserViewModel);
                        break;

                    case ResultLogin.Succses:

                        var user = await _userService.GetUserByEmailAsync(UserViewModel.UserEmail);

                        if (user != null)
                        {
                            List<Claim> claimtypes = new List<Claim>()
                            {
                                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
                                new Claim(ClaimTypes.Email,user.UserEmail),
                                new Claim("UserName",user.UserName),
                            };

                            var claimIdentity = new ClaimsIdentity(claimtypes, CookieAuthenticationDefaults.AuthenticationScheme);

                            var ClaimsPrincipal = new ClaimsPrincipal(claimIdentity);

                            var authentcationProperties = new AuthenticationProperties()
                            {
                                IsPersistent = UserViewModel.IsPersistent,
                            };

                            await HttpContext.SignInAsync(ClaimsPrincipal, authentcationProperties);

                            return Redirect("/");
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "متاسفانه  عملیات با خطا مواجه شد");
                            return View(UserViewModel);
                        }

                        break;

                    default:
                        return View(UserViewModel);
                        break;
                }
            }
            return View();
        }

        #endregion

        #region Logout

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Register

        [Route("Register")]
        [HttpGet]
        public async Task<IActionResult> Registrer()
        {
            TempData["PageName"] = "ثبت نام";
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Registrer(RegisterViewModel UserViewModel)
        {
            TempData["PageName"] = "ثبت نام";

            if (UserViewModel.AcceptRoles != false)
            {
                if (!ModelState.IsValid)
                {
                    return View(UserViewModel);
                }
                else
                {
                    var result = await _userService.RegisterUserAsync(UserViewModel);

                    switch (result)
                    {
                        case ResultRegister.Succses:
                            return View("SuccsesRegister", UserViewModel);
                            break;

                        case ResultRegister.EmailInvalid:
                            ModelState.AddModelError("UserEmail", "ایمیل وارد شده از قبل موجود است");
                            return View(UserViewModel);
                            break;

                        case ResultRegister.Failed:
                            return View(UserViewModel);
                            break;

                        default:
                            return View(UserViewModel);
                            break;

                    }
                }
            }
            else
            {
                ModelState.AddModelError("AcceptRoles", "برای ثبت نام در سایت شما میبایسد تمامی شرایط و قوانین را پذیرفته باشید");
                return View(UserViewModel);
            }
        }

        #endregion

        #region ForgotPassword

        [Route("/forgot-Password")]
        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            TempData["PageName"] = "فراموشی رمز عبور";


            return View();
        }

        [Route("/forgot-Password")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            TempData["PageName"] = "فراموشی رمز عبور";

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = await _userService.ForgotPassword(model);

                switch (result)
                {
                    case ResultForgotPassword.Succses:
                        return RedirectToAction(nameof(OTPEmail), new { Email = model.UserEmail });

                    case ResultForgotPassword.UserNotFound:
                        ModelState.AddModelError(nameof(ForgotPasswordViewModel.UserEmail), "مشخصات نادرست است و حسابی بااین ایمیل یافت نشد !");

                        break;
                }


            }
            return View();
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public async Task<IActionResult> Resetpassword(string email)
        {
            TempData["PageName"] = "بازیابی رمز عبور";

            if (!email.IsNullOrEmpty())
            {
                ResetPasswordViewModel model = new ResetPasswordViewModel()
                {
                    Email = email,
                };
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(ForgotPassword));
            }

        }


        [HttpPost]
        public async Task<IActionResult> Resetpassword(ResetPasswordViewModel model)
        {
            TempData["PageName"] = "بازیابی رمز عبور";

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = await _userService.ResetPassword(model);

                switch (result)
                {
                    case ResultResetPassword.Succses:
                        return View("SucsessResetPassword");
                        break;

                    case ResultResetPassword.Failed:
                        return View("SomethingWentWrong");
                        break;

                    default:
                        return View(model);
                        break;

                }
            }
        }

        #endregion

        #region ChangePassword

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            string emailUser = User.FindFirstValue(ClaimTypes.Email);

            TempData["PageName"] = "تغیر رمز عبور";

            ChangePasswordViewModel model = new ChangePasswordViewModel()
            {
                Email = emailUser,
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            TempData["PageName"] = "تغیر رمز عبور";

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = await _userService.ChangePassword(model);
                switch (result)
                {
                    case ResultChnagePassword.Succses:
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return View("SucsessChangePassword");
                        break;

                    case ResultChnagePassword.Failed:
                        return View("SomethingWentWrong");
                        break;

                    case ResultChnagePassword.oldpassInvalid:
                        ModelState.AddModelError("OldPassword","رمز عبور فعلی صحیح نیست");
                        return View(model);
                        break;

                    default:
                        return View(model);
                        break;

                }
            }

        }

        #endregion

        #region OTPEmail


        [HttpGet]
        public async Task<IActionResult> OTPEmail(string Email)
        {
            TempData["PageName"] = "کد تایید برای ایمیل";

            if (!Email.IsNullOrEmpty())
            {
                OTPVerifyViewModel model = new OTPVerifyViewModel()
                {
                    Email = Email
                };

                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(ForgotPassword));
            }
        }

        [HttpPost]
        public async Task<IActionResult> OTPEmail(OTPVerifyViewModel model)
        {
            TempData["PageName"] = "کد تایید برای ایمیل";

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = await _userService.ValidateConfirmCode(model);

                switch (result)
                {
                    case ResultOTPEmail.Succses:
                        return RedirectToAction(nameof(Resetpassword), new { email = model.Email });
                        break;

                    case ResultOTPEmail.InavildCode:
                        ModelState.AddModelError("VerifyCode", "کد نامعتبر است");
                        return View(model);
                        break;

                    default:
                        return View(model);
                        break;

                }
            }
        }

        #endregion

        #region Resend Code 

        //[HttpPost]
        //public async Task<IActionResult> ResendCode(string  email)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _userService.ResendCode(email);

        //    switch (result)
        //    {
        //        case ResultResendCode.Succses:
        //            return Ok(); // ارسال مجدد موفقیت‌آمیز
        //        case ResultResendCode.failed:
        //            return BadRequest("ارسال مجدد ناموفق بود.");
        //        default:
        //            return StatusCode(500, "خطای داخلی سرور.");
        //    }
        //}

        #endregion

        #region ViewAccount

        [Route("AccountPanel")]
        public async Task<IActionResult> AccountPanel()
        {
            TempData["PageName"] = "پنل کاربری";
            return View();
        }

        #endregion
    }
}
