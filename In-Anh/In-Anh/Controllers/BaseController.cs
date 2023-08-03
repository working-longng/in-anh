using In_Anh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Xml.Linq;

namespace In_Anh.Controllers
{

    public  class BaseController : Controller
    {
        private IConfiguration _config;

        public BaseController(IConfiguration config)
        {
            _config = config;
        }
        public string GetUserCookies()
        {
            var userRaw = Request.Cookies["userToken"];
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
        public UserModel GetUserValid(string token)
        {
                        
            var user = new UserModel();
            var mySecret = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Issuer"],
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);

                var tokenS = tokenHandler?.ReadToken(token) as JwtSecurityToken;
                user.Email = tokenS?.Claims?.First(claim => claim.Type == "email")?.Value;
                user.UserName = tokenS?.Claims?.First(claim => claim.Type == "name")?.Value;
                user.ImageUrlUser = tokenS?.Claims?.First(claim => claim.Type == "website")?.Value;
            }
            catch
            {
                return new UserModel();
            }
            return user;
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

