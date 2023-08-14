using In_Anh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace In_Anh.Controllers
{
    public class HistoryController : BaseController
    {
        private static readonly int pageSize = 2;
        public HistoryController(IConfiguration config, IImageMgDatabase setting) : base(config, setting)
        {
        }

        // GET: HistoryController
        public ActionResult Index()
        {
            Languages lang = new Languages().LanguageVN();
            ViewBag.Language = lang;
            ViewBag.Page = 0;
            ViewBag.PageSize = pageSize;
            ViewBag.IsLogin = false;
            var lstId = GetOrderIdsUserCookies();
            var phone = GetPhoneUserCookies();
            var orderLst = new List<OrderDetail>() { };
            if (string.IsNullOrEmpty(phone) || lstId == null || lstId.Length ==0)
            {
                return View(orderLst);
            }
            var orderqr = _ordersCollection.AsQueryable().Where(x => x.ListDetail.Any(x => lstId.Contains(x.OrderId)) && x.Phone == phone).Select(x => x.ListDetail);
            var count = orderqr.Count();
            TempData["Count"] = count;
            var data = orderqr.Take(pageSize).FirstOrDefault();
            
            
            if (data ==null || !data.Any())
            {
                return View(orderLst);
            }

            return View(data);
        }

        // GET: HistoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HistoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistoryController/Create
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

        // GET: HistoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HistoryController/Edit/5
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

        // GET: HistoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HistoryController/Delete/5
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
