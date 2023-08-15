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
            var data = orderqr.FirstOrDefault().OrderByDescending(y=>y.DayOrder).Take(pageSize).ToList();
            
            
            if (data ==null || !data.Any())
            {
                return View(orderLst);
            }

            return View(data);
        }
        public async Task<ActionResult> GetData(int page)
        {
            Languages lang = new Languages().LanguageVN();
            ViewBag.Language = lang;
            ViewBag.Page = 0;
            ViewBag.PageSize = pageSize;
            ViewBag.IsLogin = false;


            var lstId = GetOrderIdsUserCookies();
            var phone = GetPhoneUserCookies();
            var orderLst = new List<OrderDetail>() { };
            if (string.IsNullOrEmpty(phone) || lstId == null || lstId.Length == 0)
            {
                return new JsonResult(new
                {
                    Code = 201,
                    Data = new { },
                    Message = "File Not Valid"
                });
            }
            var orderqr = _ordersCollection.AsQueryable().Where(x => x.ListDetail.Any(x => lstId.Contains(x.OrderId)) && x.Phone == phone).Select(x => x.ListDetail);
            
           
            var data = orderqr.FirstOrDefault().OrderByDescending(y => y.DayOrder).Skip(page * pageSize).Take(pageSize).ToList();


            if (data == null || !data.Any())
            {
                return new JsonResult(new
                {
                    Code = 201,
                    Data = new { },
                    Message = "File Not Valid"
                });
            }
            var partialViewHtml = await this.RenderViewAsync("_RenderItem", data, true);
            return new JsonResult(new
            {
                Code = 200,
                Data = new { html=partialViewHtml },
                Message = ""
            });

        }
        // GET: HistoryController/Details/5

    }
}
