using In_Anh.Models;
using In_Anh.RabitMQ;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace In_Anh.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IConfiguration config, IImageMgDatabase setting, IRabitMQProducer rabitMQProducer) : base(config, setting, rabitMQProducer)
        {
        }

        public IActionResult Index()
        {
            Languages lang = new Languages().LanguageVN();
            ViewBag.Language = lang;
            ViewBag.IsLogin = isLogin();
            if (isLogin())
            {
                var token = Request.Cookies["userToken"];


                var validuser = GetUserValid(token);
                ViewBag.UserLogin = validuser;

                
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