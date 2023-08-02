using In_Anh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace In_Anh.Controllers
{
    public class HomeController : BaseController
    {
       

        public IActionResult Index()
        {
            Languages lang = new Languages().LanguageVN();
            ViewBag.Language = lang;
            ViewBag.IsLogin = isLogin();
            if (isLogin())
            {
                ViewBag.UserLogin = JsonConvert.DeserializeObject<UserModel>(GetUserCookies());
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}