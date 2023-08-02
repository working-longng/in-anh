using In_Anh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace In_Anh.Controllers
{

    public  class BaseController : Controller
    {
        public string GetUserCookies()
        {
            var userRaw = Request.Cookies["userLogin"];
            if (userRaw == null || userRaw == "")
            {
                return "";
            }
            else
            {
                return userRaw;


            }

        }
        public bool isLogin()
        {
            if (string.IsNullOrEmpty(GetUserCookies()))
            {
                return false;
            }
            return true;
        }


        public void SetCookies(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }
        public void RemoveCookies(string key)
        {
            Response.Cookies.Delete(key);
        }
        //protected ISomeType SomeMember { get; set; }
    }
    


}

