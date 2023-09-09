using In_Anh.Models;
using In_Anh.RabitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq;

namespace In_Anh.Controllers
{
    public class HistoryController : BaseController
    {
        private static readonly int pageSize = 2;

        public HistoryController(IConfiguration config, IImageMgDatabase setting, IRabitMQProducer rabitMQProducer) : base(config, setting, rabitMQProducer)
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
            var cdnpath = _config["Cdn:UrlCdn"];
            ViewBag.Cdn = cdnpath;
            var phone = GetPhoneUserCookies();
            var orderLst = new List<OrderDetail>() { };
            if (string.IsNullOrEmpty(phone) || lstId == null || lstId.Length ==0)
            {
                return View(orderLst);
            }
            var orderqr = _ordersCollection?.AsQueryable()?.Where(x => x.Phone == phone).FirstOrDefault()?.ListDetail.Where(x => lstId.Contains(x.OrderId)).ToList();
            
            var data = orderqr?.OrderByDescending(y=>y.DayOrder).Take(pageSize).ToList();
            
            
            if (data ==null || !data.Any())
            {
                return View(orderLst);
            }
            foreach (var item in data)
            {
                var port = 0;
                if(item.Port == "192.168.1.4")
                {
                    port = 444;
                }
                else
                {
                    port = 446;
                }
                if (item.Images != null)
                {
                    foreach (var item1 in item.Images)
                    {
                        var lstTemp = new List<string>();
                        foreach (var item2 in item1.OrginUrl)
                        {
                            lstTemp.Add(cdnpath + ":" + port + "\\" + item2);
                        }
                        item1.OrginUrl = lstTemp;
                    }
                }
            }
            return View(data);
        }
        public async Task<ActionResult> GetData(int page)
        {
            Languages lang = new Languages().LanguageVN();
            ViewBag.Language = lang;
            ViewBag.IsLogin = false;
            var cdnpath = _config["Cdn:UrlCdn"];
            ViewBag.Cdn = cdnpath;
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
            var orderqr = _ordersCollection?.AsQueryable()?.Where(x => x.Phone == phone).FirstOrDefault()?.ListDetail.Where(x => lstId.Contains(x.OrderId)).ToList();
            

            var data = orderqr?.OrderByDescending(y => y.DayOrder).Skip(page * pageSize).Take(pageSize).ToList();


            if (data == null || !data.Any())
            {
                return new JsonResult(new
                {
                    Code = 201,
                    Data = new { },
                    Message = "File Not Valid"
                });
            }

            foreach (var item in data)
            {
                var port = 0;
                if (item.Port == "192.168.1.4")
                {
                    port = 444;
                }
                else
                {
                    port = 446;
                }
                if (item.Images != null)
                {
                    foreach (var item1 in item.Images)
                    {
                        var lstTemp = new List<string>();
                        foreach (var item2 in item1.OrginUrl)
                        {
                            lstTemp.Add(cdnpath + ":" + port + "\\" + item2);
                        }
                        item1.OrginUrl = lstTemp;
                    }
                }
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
