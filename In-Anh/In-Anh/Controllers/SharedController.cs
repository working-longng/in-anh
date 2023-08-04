using Microsoft.AspNetCore.Mvc;

namespace In_Anh.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RenderTemplate()
        {
            return View();
        }
    }
}
