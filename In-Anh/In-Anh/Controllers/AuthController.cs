using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using In_Anh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static Google.Rpc.Context.AttributeContext.Types;

namespace In_Anh.Controllers
{
    public class AuthController : BaseController
    {
        private IConfiguration _config;
        public AuthController(IConfiguration config) : base(config)
        {
            _config= config;
        }



        // GET: AuthController
        public async Task<ActionResult> IndexAsync(string token)
        {
            FirebaseHelper.SetEnviromentVar();
            FirebaseToken decodedToken = await FirebaseAuth.GetAuth(FirebaseApp.GetInstance("jin-nie"))
    .VerifyIdTokenAsync(token);
            string uid = decodedToken.Uid;
            var user = await FirebaseAuth.GetAuth(FirebaseApp.GetInstance("jin-nie")).GetUserAsync(uid);
            var us = new UserModel()
            {
                UserName = user.DisplayName ?? user.ProviderData[0].DisplayName ,
                Email = user.Email ?? user.ProviderData[0].Email,
                PhoneNumber = user.PhoneNumber ?? user.ProviderData[0].PhoneNumber ,
                ImageUrlUser = user.PhotoUrl ?? user.ProviderData[0].PhotoUrl,
                Uid = uid,
                
            };

            SetCookies("userToken", GenerateJSONWebToken(us), 3600);
          var  partialViewHtml = await this.RenderViewAsync("UserRender", us, true);
            return Content(partialViewHtml);
        }

        public ActionResult InitLogin()
        {
            var token = Request.Cookies["userToken"];


            var validuser = GetUserValid(token);
            if (string.IsNullOrEmpty(validuser?.Email))
            {
                RemoveCookies("userToken");
                
                return new JsonResult(new
                {
                    Code=400
                });
            }

            return new JsonResult(new
            {
                Code = 200,
                Data = JsonConvert.SerializeObject(validuser)
            });
        }
        // GET: AuthController/Details/5

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim("email", userInfo.Email ?? "" ),
        new Claim("name", userInfo.UserName ?? ""),
        new Claim("website", userInfo.ImageUrlUser ?? "" ),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        

    }
}
