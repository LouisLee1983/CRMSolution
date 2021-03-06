﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CrmWebApp.Models;
using System.Collections.Generic;
using static CrmWebApp.Controllers.ManageController;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;

namespace CrmWebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult QunarLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public string GetRandomPassword(int len)
        {
            char[] constant = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            Random rd = new Random();
            for (int i = 0; i < len; i++)
            {
                newRandom.Append(constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }

        public string GetAndSaveMobilePassword(string mobileNum)
        {
            string password = GetRandomPassword(6);
            OtaCrmModel db = new OtaCrmModel();
            MobilePassword item = new MobilePassword();
            item.CreateTime = DateTime.Now;
            item.MobileNum = mobileNum;
            item.Password = password;
            db.MobilePassword.Add(item);
            db.SaveChanges();

            return password;
        }

        public bool IsMobilePasswordValid(string mobileNum, string password)
        {
            bool result = false;
            //取最近的，3分钟有效的密码
            DateTime lastCreateTime = DateTime.Now.AddMinutes(-3);
            OtaCrmModel db = new OtaCrmModel();
            var q = (from p in db.MobilePassword
                     where p.MobileNum == mobileNum && p.CreateTime > lastCreateTime
                     orderby p.CreateTime descending
                     select p.Password).FirstOrDefault();
            if (!string.IsNullOrEmpty(q))
            {
                result = password == q;
            }
            return result;
        }

        public string FindUserNameByMobileNum(string mobileNum)
        {
            OtaCrmModel db = new OtaCrmModel();
            var q = (from p in db.AspNetUsers
                     where p.PhoneNumber == mobileNum
                     select p.UserName).FirstOrDefault();
            return q;
        }

        public string GetRealName(string userName)
        {
            OtaCrmModel db = new OtaCrmModel();
            var q = (from p in db.AspNetUsers
                     where p.UserName == userName
                     select p.TrueName).FirstOrDefault();
            return q;
        }

        public List<string> GetAllRealName()
        {
            OtaCrmModel db = new OtaCrmModel();
            var q = (from p in db.AspNetUsers
                     select p.TrueName).ToList();
            return q;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QunarLogin(QunarLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //通过用户名获取到手机号，然后用手机号去验证验证码
            ApplicationUser user = await UserManager.FindByNameAsync(model.UserName);
            string mobileNum = user.PhoneNumber;
            //通过验证验证码是否正确,阻止用户多次登录尝试，屏蔽登录攻击。。
            if (IsMobilePasswordValid(mobileNum, model.Vcode))
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
                switch (result)
                {
                    case SignInStatus.Success:
                        if (returnUrl == null)
                        {
                            return RedirectToAction("Index", "Charts");
                        }
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "无效的登录尝试。");
                        return View(model);
                }
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult GetQunarSMS(string userName)
        {
            //            type 申请的 发送类型，这里使用qunar_xx
            //date 发送时 间，为null则立即发送，定时发送则使用 yyyy/ MM / dd HH: mm: ss格式
            //     prenums   国家区号 比如  86   多个用,分隔 与 所传 mobiles 一一对应
            //  mobiles 发 送的手机号码数组，发送到多个手机请使用,分隔
            //  message 发 送消息的内容，长度请确保在60个字符以内，否则将拆分多条发送，计费多倍
            //  groupid 可 为null，发送的消息类型名，建议输入，可供统计使用。如：酒店团购
            //  //由欧阳泉于2014年3月7日添加 支持国际短信（目前支持244个国家或地区），不传递该参数时只支持国内， 
            //  inter    国际手机号 传字符串 true
            ////关于手机号有效性及归属地的判断 详见http://wiki.corp.qunar.com/pages/viewpage.action?pageId=52070424
            //            编码格式：UTF - 8
            //返回结果：String 型, 为0则发送成功
            //    public final static int OK = 0;//成功
            //public final static int ERR_TYPE = 101;// 主账户不存在
            //public final static int ERR_MESSAGE = 102;//发送短信内容为空
            //public final static int ERR_MOBILE = 103; // 号码错误
            //public final static int ERR_SERVICE_URL = 104;//配置短信网关地址错误
            //public final static int ERR_INTER_SUPPORT = 107; //该主账户不支持国际短信
            //public final static int ERR_FORBIDDEN_REQUEST = 201;//IP不在白名单中
            //public final static int ERR_RESPONSE = 202;//http response解析错误
            //public final static int ERR_BLACKLIST = 302;//号码在黑名单中
            //public final static int ERR_UNKNOWN = 401;//调用者本地错误
            //public final static int ERR_MAINACCOUNT_NOTACTIVE = 600;// 主账户未激活
            //public final static int ERR_SUBACCOUNT_NOTAVAILABLE = 601;// 没有可用的子账户
            //public final static int ERR_LOW_BALANCE = 602;// 余额不足
            //public final static int ERR_MMS_BIG = 603;// 彩信内容过大
            //public final static int ERR_CONTEXT_PHONE = 604;// 相同号码、内容已经发送

            //根据userName获取手机号
            ApplicationUser user = UserManager.FindByName(userName);
            string mobileNum = user.PhoneNumber;
            string result = "";
            if (!string.IsNullOrEmpty(mobileNum))
            {
                //限制使用的频率。1分钟只能一个验证码。不能多次发送
                DateTime lastCreateTime = DateTime.Now.AddMinutes(-1);
                OtaCrmModel db = new OtaCrmModel();
                var todayCount = (from p in db.MobilePassword
                                  where p.MobileNum == mobileNum && p.CreateTime > DateTime.Today
                                  select p).Count();
                if (todayCount > 30)
                {
                    result = "今天发送短信超过30次，已经屏蔽.";
                }
                else
                {
                    var q = (from p in db.MobilePassword
                             where p.MobileNum == mobileNum && p.CreateTime > lastCreateTime
                             orderby p.CreateTime descending
                             select p.Password).FirstOrDefault();

                    result = "一分钟只能发一次";
                    if (string.IsNullOrEmpty(q))
                    {
                        string password = GetAndSaveMobilePassword(mobileNum);  //保存到数据库，然后验证的时候取最近的一条
                        string url = "http://sms1.f.cn1.qunar.com/mon/req";
                        string type = "qs_fland_sstm";
                        string postData = "type=" + type + "&date=&prenums=86&mobiles=" + mobileNum + "&message=" + password + "-OtaCrmPassword&groupid=otacrm&inter=false";
                        string response = PostDataToUrl(postData, url);

                        switch (response)
                        {
                            case "0":
                                result = "成功";
                                break;
                            case "101":
                                result = "主账户不存在";
                                break;
                            case "102":
                                result = "发送短信内容为空";
                                break;
                            case "103":
                                result = "号码错误";
                                break;
                            case "104":
                                result = "配置短信网关地址错误";
                                break;
                            case "107":
                                result = "该主账户不支持国际短信";
                                break;
                            case "201":
                                result = "IP不在白名单中";
                                break;
                            case "202":
                                result = "http response解析错误";
                                break;
                            case "302":
                                result = "号码在黑名单中";
                                break;
                            case "401":
                                result = "调用者本地错误";
                                break;
                            case "600":
                                result = "主账户未激活";
                                break;
                            case "601":
                                result = "没有可用的子账户";
                                break;
                            case "602":
                                result = "余额不足";
                                break;
                            case "603":
                                result = "彩信内容过大";
                                break;
                            case "604":
                                result = "相同号码、内容已经发送";
                                break;
                            default:
                                result = response;
                                break;
                        }
                    }
                }
            }
            else
            {
                result = "该账户不存在手机号";
            }

            return Content(result);
        }

        public string PostDataToUrl(string data, string url)
        {
            byte[] bytesToPost = Encoding.UTF8.GetBytes(data);
            #region 创建httpWebRequest对象
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest == null)
            {
                throw new ApplicationException(string.Format("Invalid url string: {0}", url));
            }
            #endregion

            #region 填充httpWebRequest的基本信息
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Method = "POST";
            httpRequest.Referer = url;              //来源地址,避免有些站是防盗链的
            httpRequest.AllowAutoRedirect = true;  //这里不允许再继续跳转.否则取不到了
            #endregion

            #region 填充要post的内容
            httpRequest.ContentLength = data.Length;
            Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(bytesToPost, 0, bytesToPost.Length);
            requestStream.Close();
            #endregion

            #region 发送post请求到服务器并读取服务器返回信息
            Stream responseStream;
            try
            {
                WebResponse response = httpRequest.GetResponse();
                responseStream = response.GetResponseStream();
            }
            catch (Exception e)
            {
                // log error
                Console.WriteLine(string.Format("POST操作发生异常：{0}", e.Message));
                throw e;
            }
            #endregion

            #region 读取服务器返回信息
            string stringResponse = string.Empty;
            using (StreamReader responseReader =
                new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
            {
                stringResponse = responseReader.ReadToEnd();
            }
            responseStream.Close();
            #endregion
            return stringResponse;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "无效的登录尝试。");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // 要求用户已通过使用用户名/密码或外部登录名登录
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 以下代码可以防范双重身份验证代码遭到暴力破解攻击。
            // 如果用户输入错误代码的次数达到指定的次数，则会将
            // 该用户帐户锁定指定的时间。
            // 可以在 IdentityConfig 中配置帐户锁定设置
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "代码无效。");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            ViewData["ServeAreaList"] = GetServeAreaList("");
            return View();
        }

        public List<SelectListItem> GetServeAreaList(string defaultValue)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            SelectListItem defaultItem = new SelectListItem();
            defaultItem.Text = "";
            defaultItem.Value = "";
            if (string.IsNullOrEmpty(defaultValue))
            {
                defaultItem.Selected = true;
            }
            result.Add(defaultItem);

            OtaCrmModel db = new OtaCrmModel();
            var q = from p in db.ParamDict
                    where p.ParamName == "销售区域"
                    select p;

            foreach (var item in q)
            {
                SelectListItem newItem = new SelectListItem();
                newItem.Text = item.SubItemName;
                newItem.Value = item.SubItemName;
                if (item.SubItemName == defaultValue)
                {
                    newItem.Selected = true;
                }
                result.Add(newItem);
            }

            return result;
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, TrueName = model.TrueName, QQ = model.QQ, PhoneNumber = model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
                    // 发送包含此链接的电子邮件
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "确认你的帐户", "请通过单击 <a href=\"" + callbackUrl + "\">這裏</a>来确认你的帐户");

                    var roleName = "Guest";
                    if (RoleManager.RoleExists(roleName) == false)
                    {
                        var role = new ApplicationRole(roleName, "访客", "");
                        await RoleManager.CreateAsync(role);
                    }
                    await UserManager.AddToRoleAsync(user.Id, roleName);

                    //加入区域
                    if (string.IsNullOrEmpty(model.ServeAreaName))
                    {
                        ServeArea serveArea = new ServeArea();
                        serveArea.UserName = model.UserName;
                        serveArea.ServeAreaName = model.ServeAreaName;
                        OtaCrmModel db = new OtaCrmModel();
                        db.ServeArea.Add(serveArea);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(model.UserName);
                // || !(await UserManager.IsEmailConfirmedAsync(user.Id))
                if (user == null)
                {
                    // 请不要显示该用户不存在或者未经确认
                    return View("ForgotPasswordConfirmation");
                }

                // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
                // 发送包含此链接的电子邮件
                string code = UserManager.GeneratePasswordResetToken(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                string mailBody = "请通过单击 " + callbackUrl + " 来重置你的密码";

                string mailTitle = "重置密码";
                //直接调用发送方法
                #region 发送邮件
                //填写电子邮件地址，和显示名称
                System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress("425451886@qq.com", "jinyuanli");
                //填写邮件的收件人地址和名称
                System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(user.Email, user.TrueName);
                //设置好发送地址，和接收地址，接收地址可以是多个

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.Priority = MailPriority.Normal;
                mail.From = from;
                mail.To.Add(to);
                mail.Subject = mailTitle;
                mail.SubjectEncoding = Encoding.GetEncoding(936); //这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。  
                mail.Body = mailBody;
                mail.IsBodyHtml = true;//设置显示htmls
                mail.BodyEncoding = Encoding.GetEncoding(936);    //邮件正文的编码， 设置不正确， 接收者会收到乱码  
                                                                  //设置好发送邮件服务地址
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = "smtp.qq.com"; //这里发邮件用的是126，所以为"smtp.126.com"
                client.Port = 25;
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;

                //填写服务器地址相关的用户名和密码信息
                client.Credentials = new System.Net.NetworkCredential("425451886@qq.com", "cpkehayuptjibgbc");
                //发送邮件
                client.Send(mail);

                #endregion
                //await UserManager.SendEmailAsync(user.Id, "重置密码", "请通过单击 <a href=\"" + callbackUrl + "\">此处</a>来重置你的密码");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // 请不要显示该用户不存在
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                //设置注销
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("QunarLogin", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 请求重定向到外部登录提供程序
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // 生成令牌并发送该令牌
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // 如果用户已具有登录名，则使用此外部登录提供程序将该用户登录
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // 如果用户没有帐户，则提示该用户创建帐户
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // 从外部登录提供程序获取有关用户的信息
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserList()
        {
            var Db = new ApplicationDbContext();
            var users = Db.Users;
            var model = new List<EditUserViewModel>();
            foreach (var user in users)
            {
                var u = new EditUserViewModel(user);
                u.ServeAreaName = GetServeAreaName(user.UserName);

                model.Add(u);
            }
            return View(model);
        }

        public string GetServeAreaName(string userName)
        {
            string result = "";
            OtaCrmModel db = new OtaCrmModel();
            var item = db.ServeArea.FirstOrDefault(p => p.UserName == userName);
            if (item != null)
            {
                result = item.ServeAreaName;
            }

            return result;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string id, ManageMessageId? Message = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = User.Identity.Name;
            }
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            //设置区域
            model.ServeAreaName = GetServeAreaName(id);

            ViewBag.MessageId = Message;

            ViewData["ServeAreaList"] = GetServeAreaList("");

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);
                user.TrueName = model.TrueName;
                user.QQ = model.QQ;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();

                //保存区域
                OtaCrmModel crmdb = new OtaCrmModel();
                var serveArea = crmdb.ServeArea.FirstOrDefault(p => p.UserName == model.UserName);
                if (serveArea == null)
                {
                    ServeArea newItem = new ServeArea();
                    newItem.UserName = model.UserName;
                    newItem.ServeAreaName = model.ServeAreaName;
                    crmdb.ServeArea.Add(newItem);
                }
                else
                {
                    serveArea.ServeAreaName = model.ServeAreaName;
                }
                crmdb.SaveChanges();

                return RedirectToAction("UserList");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(string id = null)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            return View(model);
        }
        //
        // POST: /Account/Delete
        [HttpPost, ActionName("DeleteUser")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
            return RedirectToAction("UserList");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditUserRoles(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = User.Identity.GetUserName();
            }
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new SelectUserRolesViewModel(user);
            return View(model);
        }


        //
        // POST: /Account/UserRoles
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserRoles(SelectUserRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var idManager = new IdentityManager();
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);
                idManager.ClearUserRoles(user.Id);
                foreach (var role in model.RoleEditorViews)
                {
                    if (role.Selected)
                    {
                        idManager.AddUserToRole(user.Id, role.RoleName);
                    }
                }
                return RedirectToAction("UserList");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public ActionResult CreateRole()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole(model.RoleName, model.Description, model.ParentRole);
                var result = await RoleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    ViewBag.Message = model.RoleName + ",新增成功.";
                }
                AddErrors(result);
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RoleList()
        {
            var Db = new ApplicationDbContext();
            var roles = Db.Roles;
            var model = new List<EditRoleViewModel>();
            foreach (var role in roles)
            {
                var u = new EditRoleViewModel(role);
                model.Add(u);
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditRole(string id, ManageMessageId? Message = null)
        {
            var Db = new ApplicationDbContext();
            var role = Db.Roles.First(r => r.Id == id);
            var model = new EditRoleViewModel(role);
            ViewBag.MessageId = Message;
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApplicationDbContext();
                var role = Db.Roles.First(r => r.Id == model.RoleId);
                role.Name = model.RoleName;
                role.Description = model.Description;
                role.ParentRole = model.ParentRole;
                Db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("RoleList");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRole(string id = null)
        {
            var Db = new ApplicationDbContext();
            var item = Db.Roles.First(r => r.Id == id);
            var model = new EditRoleViewModel(item);
            return View(model);
        }
        //
        // POST: /Account/Delete
        [HttpPost, ActionName("DeleteRole")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleConfirmed(string id)
        {
            var Db = new ApplicationDbContext();
            var item = Db.Roles.First(r => r.Id == id);
            Db.Roles.Remove(item);
            Db.SaveChanges();
            return RedirectToAction("RoleList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region 帮助程序
        // 用于在添加外部登录名时提供 XSRF 保护
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}