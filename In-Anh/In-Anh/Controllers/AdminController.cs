using In_Anh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Drawing.Printing;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace In_Anh.Controllers
{
    public class AdminController : BaseController
    {
        private const int PAGING = 43;
        public AdminController(IConfiguration config, IImageMgDatabase setting) : base(config, setting)
        {
        }

        // GET: AdminController
        public ActionResult Index(string keyw = "")
        {

            Languages lang = new Languages().LanguageVN();
            ViewBag.Language = lang;
            ViewBag.IsLogin = isLogin();

       
            //var token = Request.Cookies["userToken"];


            //var validuser = GetUserValid(token);
            //string phone = validuser?.PhoneNumber;
            //if (phone?.Trim().CompareTo("+84398417370") !=0)
            //{
            //    return BadRequest();
            //}
            try
            {
                var a = GetListImage("0762414222", "31236", "2023-8-12");
                var userGetOrder = _ordersCollection.Find(_ => keyw == "" ? true : _.Phone.Contains(keyw)).ToList().SelectMany(y => y.ListDetail).OrderByDescending(x => x.DayOrder).Skip(0 * PAGING).Take(PAGING).ToList();
                var count = _ordersCollection.Find(_ => keyw == "" ? true : _.Phone.Contains(keyw)).ToList().SelectMany(y => y.ListDetail).Count();
                ViewBag.IsLogin = true;
                var ttp = Math.Ceiling((decimal)count / (decimal)PAGING);
                ViewBag.TotalPage = ttp;
                TempData["TotalPage"] = (int)ttp;
                return View(userGetOrder);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<ActionResult> GetData(int pageIndex =1 , string keyw="")
        {
            var token = Request.Cookies["userToken"];


            //var validuser = GetUserValid(token);
            //string phone = validuser?.PhoneNumber;
            //if (phone?.Trim().CompareTo("+84398417370") != 0)
            //{
            //    return BadRequest();
            //}
            var userGetOrder = _ordersCollection.Find(_ => keyw == "" ? true : _.Phone.Contains(keyw)).ToList().SelectMany(y => y.ListDetail).OrderByDescending(x => x.DayOrder).Skip(pageIndex * PAGING).Take(PAGING).ToList();
            ViewBag.IsLogin = true;
            ViewBag.Page = pageIndex;
            ViewBag.TotalPage = Convert.ToInt32(TempData["TotalPage"]);
            TempData["TotalPage"] =(int) TempData["TotalPage"];
            var partialViewHtml = await this.RenderViewAsync("_Render", userGetOrder, true);
            return Content(partialViewHtml);
            
        }

      
        public async Task<ActionResult> ChangeStatusAsync(string phone,string orderID,int newStatus)
        {
            var filter = Builders<OrderModel>.Filter.And(Builders<OrderModel>.Filter.Eq(x=>x.Phone, phone), Builders<OrderModel>.Filter.Where(x=>x.ListDetail.Any(y=>y.OrderId == orderID)));
          var  a =  _ordersCollection.Find(filter).FirstOrDefault();

            var oldData = _ordersCollection.FindAsync(filter).Result.FirstOrDefault();
            if (oldData != null)
            {
                var dataDetails = oldData.ListDetail.ToList();
                if(dataDetails.Find(x => x.OrderId == orderID) != null)
                {
                    dataDetails.Find(x => x.OrderId == orderID).Active = (Active) newStatus;
                    var update = Builders<OrderModel>.Update
                     .Set(x => x.ListDetail, dataDetails);

                    var data = await _ordersCollection.FindOneAndUpdateAsync(filter, update);
                    return Ok();
                }



            }
           
                   
       
            //await _ordersCollection.UpdateOneAsync
            //    (filter, update);
            return BadRequest();
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
