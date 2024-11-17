
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ForceShop.Domian.Models;

namespace ForceShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region Home

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("search")]
        public async Task<IActionResult> Search()
        {
            TempData["PageName"] = "جستجوی پیشرفته";

            return View();
        }

        #endregion

        #region Support

        [Route("contact-us")]
        public async Task<IActionResult> ContactUs()
        {
            TempData["PageName"] = "ارتباط با ما";

            return View();
        }

        [Route("about-us")]
        public async Task<IActionResult> AboutUs()
        {
            TempData["PageName"] = "درباره ی ما";

            return View();
        }

        [Route("faq")]
        public async Task<IActionResult> FAQ()
        {
            TempData["PageName"] = "سوالات متداول";

            return View();
        }


        #endregion

        #region PrivacyAndSecurity

        [Route("Privacy")]
        public async Task<IActionResult> Privacy()
        {
            TempData["PageName"] = "حریم خصوصی";

            return View();
        }

        [Route("Rules")]
        public async Task<IActionResult> Rules()
        {
            TempData["PageName"] = "قوانین و مقررات";

            return View();
        }

        #endregion

        #region Services

        [Route("HelpBuy")]
        public async Task<IActionResult> HelpBuy()
        {
            TempData["PageName"] = "راهنمای خرید";

            return View();
        }

        [Route("RefundProduct")]
        public async Task<IActionResult> RefundProduct()
        {
            TempData["PageName"] = "شرایط بازگشت کالا";

            return View();
        }

        [Route("warranty")]
        public async Task<IActionResult> Warranty()
        {
            TempData["PageName"] = "گارانتی کالا";

            return View();
        }

        #endregion

        #region Reports

        [Route("reportbug")]
        public async Task<IActionResult> ReportBug()
        {
            TempData["PageName"] = "گزارش خرابی";

            return View();
        }

        [Route("suggestions")]
        public async Task<IActionResult> suggestions_Criticisms()
        {
            TempData["PageName"] = "پیشنهادات و انتقادات";

            return View();
        }

        #endregion

        #region Staffs

        [Route("sponsoring")]
        public async Task<IActionResult> sponsoring()
        {
            TempData["PageName"] = "اسپانسرینگ";

            return View();
        }

        [Route("JoinStaff")]
        public async Task<IActionResult> JoinStaff()
        {
            TempData["PageName"] = "عضویت مجموعه";

            return View();
        }

        #endregion

        #region Seller

        [Route("SellerRules")]
        public async Task<IActionResult> SellerRules()
        {
            TempData["PageName"] = "شرایط فروشندگی";

            return View();
        }

        [Route("verifySeller")]
        public async Task<IActionResult> verifySeller()
        {
            TempData["PageName"] = "تایید صلاحیت فروشندگی";

            return View();
        }

        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
