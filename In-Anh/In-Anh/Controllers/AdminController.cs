using In_Anh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Drawing.Printing;

namespace In_Anh.Controllers
{
    public class AdminController : BaseController
    {
        private const int PAGING = 2;
        public AdminController(IConfiguration config, IImageMgDatabase setting) : base(config, setting)
        {
        }

        // GET: AdminController
        public async Task<ActionResult> Index(string keyw = "")
        {
            var token = Request.Cookies["userToken"];


            var validuser = GetUserValid(token);
            string phone = validuser?.PhoneNumber;
            if (phone?.Trim().CompareTo("+84398417370") !=0)
            {
                return BadRequest();
            }
            var userGetOrder = _ordersCollection.Find(_ => keyw == "" ? true : _.Phone.Contains(keyw)).ToList();
            var count = _ordersCollection.CountDocumentsAsync(_ => keyw == "" ? true : _.Phone.Contains(keyw)).Result;
            ViewBag.IsLogin = true;
            ViewBag.TotalPage = (int)count / PAGING;
            return View(userGetOrder);
        }


        public async Task<ActionResult> GetData(int pageIndex =1 , string keyw="")
        {
            var token = Request.Cookies["userToken"];


            var validuser = GetUserValid(token);
            string phone = validuser?.PhoneNumber;
            if (phone?.Trim().CompareTo("+84398417370") != 0)
            {
                return BadRequest();
            }
            var userGetOrder = _ordersCollection.Find(_ => keyw ==""? true: _.Phone.Contains(keyw)).ToList().Select(x => x.ListDetail).OrderByDescending(x => x.Max(y => y.DayOrder)).Take(PAGING).Skip(pageIndex*PAGING).ToList();
            ViewBag.IsLogin = true;
            ViewBag.Page = pageIndex;
            var partialViewHtml = await this.RenderViewAsync("_Render", userGetOrder, true);
            return Content(partialViewHtml);
            
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
