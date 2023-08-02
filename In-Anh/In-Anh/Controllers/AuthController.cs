using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using In_Anh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using static Google.Rpc.Context.AttributeContext.Types;

namespace In_Anh.Controllers
{
    public class AuthController : BaseController
    {
        


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
                UserName = user.DisplayName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ImageUrlUser = user.PhotoUrl,
                Uid = uid,
                
            };
            RemoveCookies("userLogin");
            SetCookies("userLogin", JsonConvert.SerializeObject(us),3600);
          var  partialViewHtml = await this.RenderViewAsync("UserRender", us, true);
            return Content(partialViewHtml);
        }

        public async Task<ActionResult> InitLogin()
        {

            var raw = GetUserCookies();
            var data = JsonConvert.DeserializeObject<UserModel>(raw);
            var partialViewHtml = await this.RenderViewAsync("UserRender", data, true);
            return Content(partialViewHtml);
        }
        // GET: AuthController/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
