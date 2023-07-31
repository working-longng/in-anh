using In_Anh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace In_Anh.Controllers
{

    public class BaseController : Controller
    {
        public UserModel GetUserSession()
        {
           var userRaw =  HttpContext.Session.GetString("User");
            if(userRaw==null || userRaw == "")
            {
                return new UserModel();
            }
            else
            {
                var user = JsonConvert.DeserializeObject<UserModel>(userRaw);
                if (user == null)
                {
                    return new UserModel();
                }
                return user;
            }
            
        }
        public bool isLogin()
        {
            if (string.IsNullOrEmpty(GetUserSession().UserName))
            {
                return false;
            }
            return true;
        }
        
        //protected ISomeType SomeMember { get; set; }

        public BaseController(IServiceProvider serviceProvider)
        {
            //Init all properties you need
            //SomeMember =  serviceProvider.GetRequiredService<ISomeMember>();
        }
    }
}
