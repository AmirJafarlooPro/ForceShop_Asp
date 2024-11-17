using ForceShop.Domian.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Application.EmailHtml
{
    public class SenderHtml
    {
        public static string RenderSendVerifyCodeForResetPass(string UserName, string ConfirmCode)
        {
            return $@"<div style=""font-family: 'IRANSans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol'; background-color: #1a1a1d; color: #efefef; padding: 40px 20px; border-radius: 15px; box-shadow: 0 4px 30px rgba(0, 0, 0, 0.5); font-size: 16px; line-height: 1.6;"">
    <div style=""text-align: center; margin-bottom: 20px;"">
        <h1 style=""font-size: 45px; margin: 0; text-shadow: 2px 2px 5px rgba(0, 0, 0, 0.7);"">
            <span style=""color: #ffffff;"">FORCE </span><span style=""color: green;"">SHOP</span>
        </h1>
    </div>
  
    <h2 style=""color:#ffffff;text-align:center;margin-bottom:30px; font-size: 24px; text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.5);"">بازیابی رمز عبور حساب کاربری شما در فورس شاپ</h2>
  
    <p style=""color:#dddddd;text-align:center;margin-bottom:10px;font-size:18px"">
        سلام {UserName}،
    </p>
  
    <p style=""color:#dddddd;text-align:center;margin-bottom:30px;font-size:18px"">
        برای بازیابی رمز عبور خود، لطفاً از کد زیر استفاده کنید:
    </p>
  
    <div style=""text-align:center;margin-bottom:40px; transition: transform 0.3s;"">
        <p style=""background-color:#efefef;color:#000;padding:20px 30px;border-radius:8px;font-size:24px;letter-spacing:5px;font-weight:bold;display:inline-block; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.3);"">
            {ConfirmCode}
        </p>
    </div>
  
    <p style=""color:#bbbbbb;text-align:center;font-size:16px; margin-bottom: 20px;"">
        اگر این درخواست توسط شما نبوده، این ایمیل را نادیده بگیرید.
    </p>

    <footer style=""color:#888;text-align:center;margin-top:50px;font-size:12px; padding-top: 20px; border-top: 1px solid #444;"">
        شما این ایمیل را به عنوان کاربر فورس شاپ دریافت کرده‌اید.
        <br>
        برای تغییر تنظیمات ایمیل، لطفاً به صفحه <a href=""https://forceshop.ir/settings/notifications"" style=""color:#888;text-decoration:none; font-weight: bold;"" target=""_blank"">تنظیمات اعلانات</a> مراجعه کنید.
    </footer>
</div>";
        }
    }
}
